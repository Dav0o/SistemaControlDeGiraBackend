using DataAccess.Models;
using DataAccess.Models.Relations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Internal.IGeneric;
using static Repository.Extensions.DtoMapping;

namespace ControlDeGirasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, AdminTecnico")]
    public class User_RolesController : ControllerBase
    {
        public readonly IGenericRepository<User_Role> _userRoleRepository;
        public User_RolesController(IGenericRepository<User_Role> userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        [HttpGet]
        public List<User_Role> Get()
        {
            return _userRoleRepository.GetAll();
        }

    


        [HttpPost]
        public IActionResult Post([FromBody] DtoUserRole request) 
        {
            try
            {
                var newUserRole = request.ToUserRole();
                _userRoleRepository.Add(newUserRole);
                return Ok(newUserRole);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al agregar la relación: {ex.Message}");
            }
        }

        [HttpDelete("{userId}/{roleId}")]
        public IActionResult Delete(int userId, int roleId)
        {
            
            
                _userRoleRepository.Delete(userId, roleId);
                return Ok(); 
            }


    }
}
