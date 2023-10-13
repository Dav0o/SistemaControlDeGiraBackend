using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Internal.IGeneric;
using static Repository.Extensions.DtoMapping;

namespace ControlDeGirasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestDaysController : ControllerBase
    {
        public readonly IGenericRepository<RequestDays> _daysRepository;

        public RequestDaysController(IGenericRepository<RequestDays> daysRepository)
        {
            _daysRepository = daysRepository;
        }

        [HttpGet]

        public List<RequestDays> Get()
        {
            return _daysRepository.GetAll().ToList();

        }

        [HttpGet("{id}")]

        public RequestDays GetById(int id)
        {
            return _daysRepository.GetByCondition(days => days.Id == id);

        }

        [HttpPost]

        public RequestDays Post([FromBody] DtoRequestDays request)
        {
            return _daysRepository.Add(request.ToRequestDays());
        }

        [HttpPut("{id}")]

        public IActionResult Update(DtoRequestDays request)
        {
            _daysRepository.Update(request.ToRequestDays());
            return NoContent();
        }
    }
}
