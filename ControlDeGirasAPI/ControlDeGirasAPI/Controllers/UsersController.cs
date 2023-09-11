using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<User>> Create(DtoCreateUser request)
        {
            User newUser = await _userRepository.Create(request);

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

        [HttpGet]
        public async Task<List<User>> GetAll()
        {
            List<User> users = await _userRepository.GetAll();

            return users;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(DtoCreateUser request)
        {
            await _userRepository.Update(request);
            return NoContent();
        }

        [HttpPatch("{id}/disable")]
        public IActionResult Disable(int id)
        {
            var user = _userRepository.UpdateStatus(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
