using System.ComponentModel.DataAnnotations;

namespace YT.Challenge.Domain.DTOs.User
{
    public class UserLoginRequestDto
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}