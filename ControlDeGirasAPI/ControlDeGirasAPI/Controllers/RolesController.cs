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
    //[Authorize(Roles = "Admin, AdminTecnico")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RolesController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public List<Role> Get()
        {
            return _roleRepository.GetAll();

        }

        [HttpGet("{id}")]
        public Role GetById(int id)
        {
            return _roleRepository.GetByCondition(role => role.Id == id);

        }

        [HttpPost]
        public Role Post([FromBody] DtoRole request)
        {
            return _roleRepository.Add(request.ToRole());
        }

        [HttpPut("{id}")]
        public IActionResult Update(DtoRole request)
        {
            _roleRepository.Update(request.ToRole());
            return NoContent();
        }
    }
}
