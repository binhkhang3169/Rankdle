using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIRanked.Models
{
    public class Daily
    {
        [Key]
        public int DailyId { get; set; }

        [Required]
        public int Quiz1 { get; set; }

        [Required]
        public int Quiz2 { get; set; }

        [Required]
        public int Quiz3 { get; set; }

        [Required]
        [ForeignKey("TypeQuiz")]
        public int TypeId { get; set; }

        public virtual TypeQuiz TypeQuiz { get; set; }
    }

}
