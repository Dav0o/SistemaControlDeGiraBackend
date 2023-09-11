using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;
using static Repository.Extensions.DtoMapping;

namespace ControlDeGirasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestRepository _requestRepository;

        public RequestsController(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Request>> Create(DtoRequestByUser request)
        {
            Request newRequest = await _requestRepository.Create(request);

            return Ok(newRequest);
        }

        [HttpPut("endorse")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Endorse(DtoEndorseRequest request)
        {
            await _requestRepository.Endorse(request);
            return NoContent();
        }

        [HttpPut("approve")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approve(DtoApproveRequest request)
        {
            await _requestRepository.Approve(request);
            return NoContent();
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _requestRepository.Delete(id);
        }

        [HttpGet]
        public async Task<List<Request>> GetAll()
        {
            List<Request> list = await _requestRepository.GetAll();

            return list;
        }

        [HttpGet("{id}")]
        public async Task<Request> GetById(int id)
        {
            var request = await _requestRepository.GetById(id);

            return request;
        }

        [HttpPut]
        public async Task<IActionResult> Update(DtoRequest request)
        {
            await _requestRepository.Update(request);
            return NoContent();
        }
    }
}
