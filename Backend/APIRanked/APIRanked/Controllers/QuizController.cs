using APIRanked.Models;
using APIRanked.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIRanked.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        // GET: api/quiz
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetAllQuizzes()
        {
            var quizzes = await _quizService.GetAllAsync();
            return Ok(quizzes);
        }

        // GET: api/quiz/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Quiz>> GetQuizById(int id)
        {
            var quiz = await _quizService.GetByIdAsync(id);
            if (quiz == null)
            {
                return NotFound(new { message = $"Quiz with ID {id} not found." });
            }
            return Ok(quiz);
        }

        // POST: api/quiz
        [HttpPost]
        public async Task<ActionResult> AddQuiz([FromBody] Quiz quiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _quizService.AddAsync(quiz);
            return CreatedAtAction(nameof(GetQuizById), new { id = quiz.QuizId }, quiz);
        }

        // PUT: api/quiz/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateQuiz(int id, [FromBody] Quiz quiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingQuiz = await _quizService.GetByIdAsync(id);
            if (existingQuiz == null)
            {
                return NotFound(new { message = $"Quiz with ID {id} not found." });
            }

            quiz.QuizId = id; // Ensure the ID is consistent
            await _quizService.UpdateAsync(quiz);
            return NoContent();
        }

        // DELETE: api/quiz/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteQuiz(int id)
        {
            var existingQuiz = await _quizService.GetByIdAsync(id);
            if (existingQuiz == null)
            {
                return NotFound(new { message = $"Quiz with ID {id} not found." });
            }

            await _quizService.DeleteAsync(id);
            return NoContent();
        }
    }
}
