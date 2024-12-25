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

        public DailyController(IDailyService dailyService)
        {
            _dailyService = dailyService;
        }

        // GET: api/daily
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Daily>>> GetAllDailies()
        {
            var dailies = await _dailyService.GetAllAsync();
            return Ok(dailies);
        }

        // GET: api/daily/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Daily>> GetDailyById(int id)
        {
            var daily = await _dailyService.GetByIdAsync(id);
            if (daily == null)
            {
                return NotFound(new { message = $"Daily with ID {id} not found." });
            }
            return Ok(daily);
        }

        // POST: api/daily
        [HttpPost]
        public async Task<ActionResult> AddDaily([FromBody] Daily daily)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _dailyService.AddAsync(daily);
            return CreatedAtAction(nameof(GetDailyById), new { id = daily.DailyId }, daily);
        }

        // PUT: api/daily/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDaily(int id, [FromBody] Daily daily)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingDaily = await _dailyService.GetByIdAsync(id);
            if (existingDaily == null)
            {
                return NotFound(new { message = $"Daily with ID {id} not found." });
            }

            daily.DailyId = id; // Ensure the ID is consistent
            await _dailyService.UpdateAsync(daily);
            return NoContent();
        }

        // DELETE: api/daily/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDaily(int id)
        {
            var existingDaily = await _dailyService.GetByIdAsync(id);
            if (existingDaily == null)
            {
                return NotFound(new { message = $"Daily with ID {id} not found." });
            }

            await _dailyService.DeleteAsync(id);
            return NoContent();
        }
    }
}
