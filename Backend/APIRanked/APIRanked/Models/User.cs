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

        public DateTime? ResetTokenExpiry { get; set; } // Thời hạn token
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }

        public User()
        {
            UserAnswers = new List<UserAnswer>();
        }
    }

}
