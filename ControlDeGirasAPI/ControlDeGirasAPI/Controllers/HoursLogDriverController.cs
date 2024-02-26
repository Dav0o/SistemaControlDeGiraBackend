using DataAccess.Data;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Repository.Internal.IGeneric;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControlDeGirasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoursLogDriverController : ControllerBase
    {
        private readonly IGenericRepository<HoursLogDriver> _repository;
        private readonly MyDbContext _dbContext;

        public HoursLogDriverController(IGenericRepository<HoursLogDriver> repository, MyDbContext myDbContext)
        {
            _repository = repository;
            _dbContext = myDbContext;
        }

        
        [HttpGet]
        public List<HoursLogDriver> Get()
        {
            return _repository.GetAll();
        }

        

        // POST api/<HoursLogDriverController>
        [HttpPost]
        public async Task<HoursLogDriver> Post([FromBody] DtoMapping.DtoHoursLog dto)
        {
            var driver = await _dbContext.DriverLogs.FirstOrDefaultAsync(d => d.Id == dto.DriverLogId);

            TimeSpan workedHours = (dto.FinishHour - dto.InitialHour);


            if(dto.CategoryHours == "Regulares")
            {
                driver.OrdinaryHours = workedHours.Hours;
            }

            if (dto.CategoryHours == "Extra")
            {
                driver.ExtraHours = workedHours.Hours;
            }

            if (dto.CategoryHours == "Sobresueldo")
            {
                driver.BonusHours = workedHours.Hours;
            }


            _dbContext.DriverLogs.Attach(driver);
            _dbContext.Entry(driver).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return _repository.Add(dto.ToHoursLog());
        }

    
       
    }
}
