using APIRanked.DTO;
using APIRanked.Models;
using APIRanked.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Quic;

namespace APIRanked.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;
        private readonly IAuthService _authService;

        public QuizController(IQuizService quizService, IAuthService authService)
        {
            _quizService = quizService;
            _authService = authService;
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
        public async Task<ActionResult> AddQuiz([FromBody] CreateQuizDto quizDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Quiz quiz = new Quiz();

            quiz.Region = quizDto.Region;
            var userId = await _authService.GetUserByTokenAsync(quizDto.TokenJWT);
            if (userId == null)
            {
                return BadRequest(userId);
            }

            quiz.UserId = Int32.Parse(userId);
            quiz.CreatedAt = DateTime.Now;
            quiz.CorrectValue = quizDto.CorrectValue;
            quiz.TypeId = quizDto.TypeId;
            quiz.URL = quizDto.Url;

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
