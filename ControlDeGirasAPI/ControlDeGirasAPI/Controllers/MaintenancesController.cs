using Microsoft.AspNetCore.Mvc;
using Repository.Internal.IGeneric;
using DataAccess.Models;
using static Repository.Extensions.DtoMapping;
using Microsoft.AspNetCore.Authorization;

namespace ControlDeGirasAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, AdminTecnico")]
    public class MaintenancesController : ControllerBase
    {

        public readonly IGenericRepository<Maintenance> _maintenanceRepository;

        public MaintenancesController(IGenericRepository<Maintenance> maintenanceRepository)

        {

            _maintenanceRepository = maintenanceRepository;
        }


        // GET: api/<MaintenanceController>
        [HttpGet]
        public List<Maintenance> Get()
        {
            return _maintenanceRepository.GetAll();

        }

        // GET api/<MaintenanceController>
        [HttpGet("{id}")]
        public Maintenance GetById(int id)
        {
            return _maintenanceRepository.GetByCondition(maintenance => maintenance.Id == id);

        }


        // POST api/<MaintenanceController>
        [HttpPost]
        public Maintenance Post([FromBody] DtoMaintenance maintenance)
        {
            return _maintenanceRepository.Add(maintenance.ToMaintenance());
        }


        // PUT api/<MaintenanceController>
        [HttpPut("{id}")]
        public IActionResult Update(DtoMaintenance request)
        {
            _maintenanceRepository.Update(request.ToMaintenance());
            return NoContent();
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _maintenanceRepository.Delete(id);
        }






    }

}

