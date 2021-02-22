using System.ComponentModel.DataAnnotations;

namespace YT.Challenge.Domain.DTOs.User
{
    public class UserRegisterRequestDto
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}