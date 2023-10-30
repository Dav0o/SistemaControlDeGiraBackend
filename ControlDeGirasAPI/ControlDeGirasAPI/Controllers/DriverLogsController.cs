using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Internal.IGeneric;
using Repository.IRepository;
using static Repository.Extensions.DtoMapping;

namespace ControlDeGirasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverLogsController : ControllerBase
    {
        public readonly IGenericRepository<DriverLog> _logRepo;
        

        public DriverLogsController(IGenericRepository<DriverLog> logRepo)
        {
            _logRepo = logRepo;
            
        }

        
        [HttpGet]

        public List<DriverLog> Get()
        {
            return _logRepo.GetAllByCondition(x => x.Id != null,"User");

        }

        [HttpGet("{id}")]

        public DriverLog GetById(int id)
        {
            return _logRepo.GetByCondition(log => log.Id == id);

        }

        [HttpPost]

        public DriverLog Post([FromBody] DtoDriverLog log)
        {
            return _logRepo.Add(log.ToDriverLog());
        }

        [HttpPut("{id}")]

        public IActionResult Update(DtoDriverLog request)
        {
            _logRepo.Update(request.ToDriverLog());
            return NoContent();
        }
    }
}
