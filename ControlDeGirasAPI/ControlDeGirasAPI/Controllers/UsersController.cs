using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.IRepository;
using static Repository.Extensions.DtoMapping;

namespace ControlDeGirasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    

    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        

        [HttpPost]
        [Authorize(Roles = "Admin, AdminTecnico")]
        public async Task<ActionResult<User>> Create([FromBody] DtoCreateUser user)
        {
            if (!_userRepository.IsUniqueDNI(user.DNI))
            {
                return Conflict("DNI already exists");
            }

           var newUser = await _userRepository.Create(user);

            return Ok(newUser);
        }

        [HttpPost("login")]

        public async Task<Object> Login(DtoUser request)
        {
            var user = await _userRepository.Login(request);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            return Ok(user);
        }


        [HttpPost("forgot")]
        public async Task<IActionResult> ForgotPassword(DtoForgotPassword request)
        {
            try
            {
                var resetToken = await _userRepository.ForgotPassword(request);
                return Ok(new { message = "Token generado correctamente. Usuario encontrado.", resetToken });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new { message = "Error al generar el token de restablecimiento de contraseña. Error de base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al generar el token de restablecimiento de contraseña.", error = ex.Message });
            }
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword(DtoResetPassword request)
        {
            await _userRepository.ResetPassword(request);
            return NoContent();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, AdminTecnico")]
        public async Task<List<User>> GetAll()
        {
            List<User> users = await _userRepository.GetAll();

            return users;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Chofer, Funcionario")]
        public async Task<User> GetById(int id)
        {
            var user = await _userRepository.GetById(id);

            return user;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, AdminTecnico, Funcionario, Chofer")]
        public async Task<IActionResult> Update(DtoUpdateUser request)
        {
            await _userRepository.Update(request);
            return NoContent();
        }


        [HttpPatch]
        public async Task<IActionResult> ChangePassword(DtoChangePassword request)
        {
            await _userRepository.ChangePassword(request);
            return NoContent();
        }

        [HttpPatch("{id}/disable")]
        [Authorize(Roles = "Admin, AdminTecnico")]
        public IActionResult Disable(int id)
        {
            var user = _userRepository.UpdateStatus(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpGet("usersbyrole/{roleName}")]
        [Authorize(Roles = "Admin, AdminTecnico")]
        public async Task<IActionResult> GetUsersByRole(string roleName)
        {
            var users = await _userRepository.GetByRole(roleName);
            return Ok(users);
        }
    }
}
