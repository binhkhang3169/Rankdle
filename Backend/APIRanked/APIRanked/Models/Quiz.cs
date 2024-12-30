using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIRanked.Models
{
    public class Quiz
    {
        [Key]
        public int QuizId { get; set; }

        [Required]
        [StringLength(255)]
        public string Region { get; set; }

        [Required]
        [StringLength(255)]
        public string URL { get; set; }

        [Required]
        public string CorrectValue { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId {  get; set; }
        public virtual User User { get; set; }


        [Required]
        [ForeignKey("TypeQuiz")]
        public int TypeId { get; set; }

        public virtual TypeQuiz TypeQuiz { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }

        public Quiz()
        {
            UserAnswers = new List<UserAnswer>();
        }
    }

}
