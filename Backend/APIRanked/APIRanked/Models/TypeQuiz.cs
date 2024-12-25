using System.ComponentModel.DataAnnotations;

namespace APIRanked.Models
{
    public class TypeQuiz
    {
        [Key]
        public int TypeId { get; set; }

        [Required]
        [StringLength(255)]
        public string NameType { get; set; }

        public string Image { get; set; }
        public string URL { get; set; }

        public virtual ICollection<Quiz> Quizzes { get; set; }
        public virtual ICollection<Daily> Dailies { get; set; }

        public TypeQuiz()
        {
            Quizzes = new List<Quiz>();
            Dailies = new List<Daily>();
        }
    }

}
