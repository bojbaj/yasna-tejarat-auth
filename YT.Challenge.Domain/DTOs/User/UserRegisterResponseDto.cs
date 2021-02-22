using System;

namespace YT.Challenge.Domain.DTOs.User
{
    public class UserRegisterResponseDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}