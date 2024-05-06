using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Internal.IGeneric;
using Repository.IRepository;
using static Repository.Extensions.DtoMapping;

namespace ControlDeGirasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, AdminTecnico")]
    public class VehiclesController : ControllerBase
    {

        public readonly IGenericRepository<Vehicle> _vehicleRepository;
        public readonly IVehicleRepository _repository;

        public VehiclesController(IGenericRepository<Vehicle> vehicleRepository, IVehicleRepository repository)

        {

            _vehicleRepository = vehicleRepository;
            _repository = repository;
        }


        // GET: api/<VehiclesController>
        [HttpGet]
       
        public List<Vehicle> Get()
        {
            return _vehicleRepository.GetAll().ToList();

        }

        
        [HttpGet("{id}")]
     
        public Vehicle GetById(int id)
        {
            return _vehicleRepository.GetByCondition(vehicle => vehicle.Id == id, "maintenances");

        }



        // POST api/<VehiclesController>
        [HttpPost]
      
        public ActionResult<Vehicle> Post([FromBody] DtoVehicle vehicle)
        {
            if (!_repository.IsUniquePlate(vehicle.Plate_Number))
            {
                return Conflict("Plate Number already exists");
            }
            return _vehicleRepository.Add(vehicle.ToVehicle());
        }


        // PUT api/<VehiclesController>
        [HttpPut("{id}")]
    
        public IActionResult Update(DtoVehicle request)
        {
            _vehicleRepository.Update(request.ToVehicle()); 
            return NoContent();
        }

        // DESHABILITAR api/<VehiclesController>
        [HttpPatch("{id}/disable")]
      
        public IActionResult Disable(int id)
        {
            var vehicle = _vehicleRepository.GetByCondition(vehicle => vehicle.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            vehicle.Status = !(vehicle.Status);
            _vehicleRepository.Update(vehicle);

            return NoContent();
        }

    }

}

