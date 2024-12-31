using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APIRanked.Models
{
    public class UserAnswer
    {
        [Key]
        public int UserAnswerId { get; set; }

        [Required]
        public string Answer { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }

        [Required]
        [ForeignKey("Quiz")]
        public int QuizId { get; set; }

        public virtual User? User { get; set; }
        public virtual Quiz Quiz { get; set; }
    }

}
