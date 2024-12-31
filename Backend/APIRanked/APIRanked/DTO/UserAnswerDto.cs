using APIRanked.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APIRanked.DTO
{
    public class UserAnswerDto
    {
        public string Answer { get; set; }
        public string? Token { get; set; }
        public int QuizId { get; set; }
    }
}
