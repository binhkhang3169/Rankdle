using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APIRanked.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Salt { get; set; } // Dùng cho bảo mật mật khẩu


        public string? ResetToken { get; set; } // Token cho quên mật khẩu

        [ForeignKey("Role")]
        public int RoleId { get; set; } // Khóa ngoại đến Role

        public virtual Role? Role { get; set; } // Quan hệ 1:N từ Role -> Users

        public DateTime? ResetTokenExpiry { get; set; } // Thời hạn token
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
        public User()
        {
            Quizzes = new List<Quiz>();
            UserAnswers = new List<UserAnswer>();
        }
    }

}
