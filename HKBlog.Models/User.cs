
namespace HKBlog.Models
{
    using System.ComponentModel.DataAnnotations;
    public class User
    {
        public string Id { get; set; } = "";
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$",
            ErrorMessage = "Password must have at least 8 characters, including a letter, number, and special character.")]
        public string Password { get; set; } = "";
        [Required]
        [Compare("Password", ErrorMessage = "Password fields do not match, ensure both fields are the same.")]
        public string Password2 { get; set; } = string.Empty;
        public string Filepath { get; set; } = string.Empty;
        [Required]
        public string Username { get; set; } = "";
        public bool IsAdmin { get; set; }
        public bool AllowAcess { get; set; }
        [Required]
        public int OTP { get; set; }
    }
}