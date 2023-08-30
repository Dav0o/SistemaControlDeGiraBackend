using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Internal.IGeneric;
using static Repository.Extensions.DtoMapping;

namespace ControlDeGirasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {

        public readonly IGenericRepository<Vehicle> _vehicleRepository;

        public VehiclesController(IGenericRepository<Vehicle> vehicleRepository)

        {

            _vehicleRepository = vehicleRepository;
        }


        // GET: api/<VehiclesController>
        [HttpGet]
        public List<Vehicle> Get()
        {
            return _vehicleRepository.GetAll().Where(vehicle => vehicle.Status).ToList();

        }

        // GET api/<VehiclesController>
        [HttpGet("{id}")]
        public Vehicle GetById(int id)
        {
            return _vehicleRepository.GetByCondition(vehicle => vehicle.Id == id);

        }


        // POST api/<VehiclesController>
        [HttpPost]
        public Vehicle Post([FromBody] DtoVehicle vehicle)
        {
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

            vehicle.Status = false;
            _vehicleRepository.Update(vehicle);

            return NoContent();
        }

    }

}

