using APIRanked.Models;
using APIRanked.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIRanked.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeQuizController : ControllerBase
    {
        private readonly ITypeQuizService _typeQuizService;

        public TypeQuizController(ITypeQuizService typeQuizService)
        {
            _typeQuizService = typeQuizService;
        }

        // GET: api/typequiz
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeQuiz>>> GetAllTypeQuizzes()
        {
            var typeQuizzes = await _typeQuizService.GetAllAsync();
            return Ok(typeQuizzes);
        }

        // GET: api/typequiz/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeQuiz>> GetTypeQuizById(int id)
        {
            var typeQuiz = await _typeQuizService.GetByIdAsync(id);
            if (typeQuiz == null)
            {
                return NotFound(new { message = $"TypeQuiz with ID {id} not found." });
            }
            return Ok(typeQuiz);
        }

        // POST: api/typequiz
        [HttpPost]
        public async Task<ActionResult> AddTypeQuiz([FromBody] TypeQuiz typeQuiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _typeQuizService.AddAsync(typeQuiz);
            return CreatedAtAction(nameof(GetTypeQuizById), new { id = typeQuiz.TypeId }, typeQuiz);
        }

        // PUT: api/typequiz/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTypeQuiz(int id, [FromBody] TypeQuiz typeQuiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTypeQuiz = await _typeQuizService.GetByIdAsync(id);
            if (existingTypeQuiz == null)
            {
                return NotFound(new { message = $"TypeQuiz with ID {id} not found." });
            }

            typeQuiz.TypeId = id; // Ensure the ID is consistent
            await _typeQuizService.UpdateAsync(typeQuiz);
            return NoContent();
        }

        // DELETE: api/typequiz/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTypeQuiz(int id)
        {
            var existingTypeQuiz = await _typeQuizService.GetByIdAsync(id);
            if (existingTypeQuiz == null)
            {
                return NotFound(new { message = $"TypeQuiz with ID {id} not found." });
            }

            await _typeQuizService.DeleteAsync(id);
            return NoContent();
        }
    }
}
