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
            return _vehicleRepository.GetAll();

        }

        // GET api/<VehiclesController>
        [HttpGet("{id}")]
        public Vehicle GetById(int id)
        {
            return _vehicleRepository.GetByCondition(vehicle => vehicle.Id == id);

        }


        // POST api/<VehiclesController>
        [HttpPost]
        public Vehicle Post([FromBody] Vehicle vehicle)
        {
            return _vehicleRepository.Add(vehicle);
        }


        // PUT api/<VehiclesController>
        [HttpPut("{id}")]
        public IActionResult Update(Vehicle request)
        {
            _vehicleRepository.Update(request); 
            return NoContent();
        }

    }

}

