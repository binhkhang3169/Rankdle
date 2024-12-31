using APIRanked.DTO;
using APIRanked.Models;
using APIRanked.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIRanked.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAnswerController : ControllerBase
    {
        private readonly IUserAnswerService _userAnswerService;
        private readonly IAuthService _authService;

        public UserAnswerController(IUserAnswerService userAnswerService, IAuthService authService)
        {
            _userAnswerService = userAnswerService;
            _authService = authService;
        }

        // GET: api/useranswer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAnswer>>> GetAllUserAnswers()
        {
            var userAnswers = await _userAnswerService.GetAllAsync();
            return Ok(userAnswers);
        }

        // GET: api/useranswer/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAnswer>> GetUserAnswerById(int id)
        {
            var userAnswers = await _userAnswerService.GetAllByQuizIdAsync(id);
            return Ok(userAnswers);
        }

        // POST: api/useranswer
        [HttpPost]
        public async Task<ActionResult> AddUserAnswer([FromBody] UserAnswerDto userAnswerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userAnswer = new UserAnswer
            {
                Answer = userAnswerDto.Answer,
                QuizId = userAnswerDto.QuizId
            };
            if (userAnswerDto.Token != null)
            {

                var userId = await _authService.GetUserByTokenAsync(userAnswerDto.Token);
                if (userId != null)
                {
                    userAnswer.UserId = Int32.Parse(userId);
                }
            }

            await _userAnswerService.AddAsync(userAnswer);
            return CreatedAtAction(nameof(GetUserAnswerById), new { id = userAnswer.UserAnswerId }, userAnswer);
        }

        // PUT: api/useranswer/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserAnswer(int id, [FromBody] UserAnswer userAnswer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUserAnswer = await _userAnswerService.GetByIdAsync(id);
            if (existingUserAnswer == null)
            {
                return NotFound(new { message = $"UserAnswer with ID {id} not found." });
            }

            userAnswer.UserAnswerId = id; // Ensure ID is consistent
            await _userAnswerService.UpdateAsync(userAnswer);
            return NoContent();
        }

        // DELETE: api/useranswer/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAnswer(int id)
        {
            var existingUserAnswer = await _userAnswerService.GetByIdAsync(id);
            if (existingUserAnswer == null)
            {
                return NotFound(new { message = $"UserAnswer with ID {id} not found." });
            }

            await _userAnswerService.DeleteAsync(id);
            return NoContent();
        }
    }
}
