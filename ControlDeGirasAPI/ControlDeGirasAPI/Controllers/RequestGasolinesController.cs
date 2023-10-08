using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Internal.IGeneric;
using static Repository.Extensions.DtoMapping;

namespace ControlDeGirasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestGasolinesController : ControllerBase
    {
        public readonly IGenericRepository<RequestGasoline> _gasRepository;

        public RequestGasolinesController(IGenericRepository<RequestGasoline> gasRepository)

        {
            _gasRepository = gasRepository;
        }

        [HttpGet]

        public List<RequestGasoline> Get()
        {
            return _gasRepository.GetAll().ToList();

        }

        [HttpGet("{id}")]

        public RequestGasoline GetById(int id)
        {
            return _gasRepository.GetByCondition(gas => gas.Id == id);

        }

        [HttpPost]

        public RequestGasoline Post([FromBody] DtoRequestGasoline request)
        {
            return _gasRepository.Add(request.ToRequestGasoline());
        }

        [HttpPut("{id}")]

        public IActionResult Update(DtoRequestGasoline request)
        {
            _gasRepository.Update(request.ToRequestGasoline());
            return NoContent();
        }
    }
}
