using APIRanked.DTO;
using APIRanked.Models;
using APIRanked.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace APIRanked.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AuthController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        // Đăng ký
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (await _userService.IsUsernameTaken(registerDto.Username))
            {
                return BadRequest(new { message = "Username đã được sử dụng." });
            }

            if (await _userService.IsEmailTaken(registerDto.Email))
            {
                return BadRequest(new { message = "Email đã được sử dụng." });
            }

            // Tạo mật khẩu hash với salt
            var salt = GenerateSalt();
            var passwordHash = HashPassword(registerDto.Password, salt);

            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                Salt = salt,
                RoleId = 1
            };

            await _userService.AddAsync(user);
            return Ok(new { message = "Đăng ký thành công." });
        }

        // Đăng nhập
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userService.GetUserByUsernameAsync(loginDto.Username);

            if (user == null || user.PasswordHash != HashPassword(loginDto.Password, user.Salt))
            {
                return Unauthorized(new { message = "Sai thông tin đăng nhập." });
            }

            // Tạo JWT Token
            var token = await _authService.GenerateJwtTokenAsync(user);
            return Ok(new { token });
        }

        
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] string token)
        {
            var success = await _authService.RevokeTokenAsync(token);
            if (!success)
            {
                return BadRequest(new { message = "Không thể thực hiện logout, token không hợp lệ." });
            }

            return Ok(new { message = "Đăng xuất thành công." });
        }

        // Quên mật khẩu
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userService.GetUserByEmailAsync(forgotPasswordDto.Email);

            if (user == null)
            {
                return BadRequest(new { message = "Email không tồn tại." });
            }

            // Tạo Reset Token
            var resetToken = Guid.NewGuid().ToString();
            user.ResetToken = resetToken;
            user.ResetTokenExpiry = DateTime.UtcNow.AddHours(1); // Token hết hạn sau 1 giờ

            await _userService.UpdateAsync(user);

            // Gửi email reset mật khẩu
            await _userService.SendPasswordResetEmail(user.Email, resetToken);

            return Ok(new { message = "Đã gửi email đặt lại mật khẩu." });
        }

        // Xác minh Reset Token
        [HttpPost("verify-reset-token")]
        public async Task<IActionResult> VerifyResetToken([FromBody] VerifyTokenDto verifyTokenDto)
        {
            var user = await _userService.GetUserByResetTokenAsync(verifyTokenDto.ResetToken);

            if (user == null || user.ResetTokenExpiry < DateTime.UtcNow)
            {
                return BadRequest(new { message = "Token không hợp lệ hoặc đã hết hạn." });
            }

            return Ok(new { message = "Token hợp lệ." });
        }

        // Đổi mật khẩu
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var user = await _userService.GetUserByResetTokenAsync(resetPasswordDto.ResetToken);

            if (user == null || user.ResetTokenExpiry < DateTime.UtcNow)
            {
                return BadRequest(new { message = "Token không hợp lệ hoặc đã hết hạn." });
            }

            // Đặt lại mật khẩu
            var salt = GenerateSalt();
            user.PasswordHash = HashPassword(resetPasswordDto.NewPassword, salt);
            user.Salt = salt;
            user.ResetToken = null;
            user.ResetTokenExpiry = null;

            await _userService.UpdateAsync(user);
            return Ok(new { message = "Đổi mật khẩu thành công." });
        }

        // Tạo mật khẩu hash
        private string HashPassword(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            var saltedPassword = password + salt;
            return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword)));
        }

        // Tạo salt ngẫu nhiên
        private string GenerateSalt()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
