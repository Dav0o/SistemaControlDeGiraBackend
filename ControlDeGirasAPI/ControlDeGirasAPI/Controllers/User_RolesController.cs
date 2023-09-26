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
        public User_Role Post([FromBody] DtoUserRole request) 
        {
            return _userRoleRepository.Add(request.ToUserRole());
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _userRoleRepository.Delete(id);
        }


    }
}
