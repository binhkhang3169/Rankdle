using APIRanked.DTO;
using APIRanked.Models;
using APIRanked.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIRanked.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyController : ControllerBase
    {
        private readonly IDailyService _dailyService;
        private readonly ITypeQuizService _typeQuizService;

        public DailyController(IDailyService dailyService, ITypeQuizService typeQuizService)
        {
            _dailyService = dailyService;
            _typeQuizService = typeQuizService;
        }

        // GET: api/daily
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Daily>>> GetAllDailies()
        {
            var dailies = await _dailyService.GetAllAsync();
            return Ok(dailies);
        }

        // GET: api/daily/{typeId}/{date}
        [HttpGet("{typeId}/{date}")]
        public async Task<ActionResult<Daily>> GetDaily(int typeId, DateOnly date)
        {
            var daily = await _dailyService.GetByIdAsync(typeId, date);
            if (daily == null)
            {
                return NotFound(new { message = $"Daily record for TypeId {typeId} and Date {date} not found." });
            }
            return Ok(daily);
        }

        // POST: api/daily
        [HttpPost]
        public async Task<ActionResult> AddDaily([FromBody] DailyDto dailyDto)
        {

            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var daily = new Daily();
            daily.Quiz1 = dailyDto.Quiz1;
            daily.Quiz2 = dailyDto.Quiz2;
            daily.Quiz3 = dailyDto.Quiz3;
            daily.Date = dailyDto.Date;
            daily.TypeId = dailyDto.TypeId;

            await _dailyService.AddAsync(daily);
            return CreatedAtAction(nameof(GetDaily), new { typeId = daily.TypeId, date = daily.Date }, daily);
        }

        // PUT: api/daily/{typeId}/{date}
        [HttpPut("{typeId}/{date}")]
        public async Task<ActionResult> UpdateDaily(int typeId, DateOnly date, [FromBody] Daily daily)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingDaily = await _dailyService.GetByIdAsync(typeId, date);
            if (existingDaily == null)
            {
                return NotFound(new { message = $"Daily record for TypeId {typeId} and Date {date} not found." });
            }

            // Ensure the TypeId and Date are consistent
            daily.TypeId = typeId;
            daily.Date = date;

            await _dailyService.UpdateAsync(daily);
            return NoContent();
        }

        // DELETE: api/daily/{typeId}/{date}
        [HttpDelete("{typeId}/{date}")]
        public async Task<ActionResult> DeleteDaily(int typeId, DateOnly date)
        {
            var existingDaily = await _dailyService.GetByIdAsync(typeId, date);
            if (existingDaily == null)
            {
                return NotFound(new { message = $"Daily record for TypeId {typeId} and Date {date} not found." });
            }

            await _dailyService.DeleteAsync(typeId, date);
            return NoContent();
        }
    }
}
