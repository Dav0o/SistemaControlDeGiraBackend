
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
    public class NoticesController : ControllerBase
    {

        public readonly IGenericRepository<Notice> _noticeRepository;
        public readonly INoticeRepository _repository;

        public NoticesController(IGenericRepository<Notice> noticeRepository)

        {

            _noticeRepository = noticeRepository;
        }



        [HttpGet]

        public List<Notice> Get()
        {
            return _noticeRepository.GetAll().ToList();

        }

        [HttpGet("{id}")]
        public Notice GetById(int id)
        {
            return _noticeRepository.GetByCondition(notice => notice.Id == id);

        }


        [HttpPost]

        public Notice Post([FromBody] DtoNotice notice)
        {
            return _noticeRepository.Add(notice.ToNotice());
        }


        [HttpPut("{id}")]

        public IActionResult Update(DtoNotice request)
        {
            _noticeRepository.Update(request.ToNotice());
            return NoContent();
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _noticeRepository.Delete(id);
        }



        [HttpPatch("{id}/disable")]

        public IActionResult Disable(int id)
        {
            var notice = _noticeRepository.GetByCondition(notice => notice.Id == id);

            if (notice == null)
            {
                return NotFound();
            }

            notice.Status = !(notice.Status);
            _noticeRepository.Update(notice);

            return NoContent();
        }

    }

}



