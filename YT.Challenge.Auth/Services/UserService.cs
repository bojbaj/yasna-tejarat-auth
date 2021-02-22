using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using YT.Challenge.Auth.i18n;
using YT.Challenge.Auth.Models;
using YT.Challenge.Domain.DTOs.User;
using YT.Challenge.Domain.Models;

namespace YT.Challenge.Auth.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMessageRepo _messageRepo;

        public UserService(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IMessageRepo messageRepo)
        {
            _userManager = userManager;
            _configuration = configuration;
            _messageRepo = messageRepo;
        }

        public async Task<TypedResult<UserLoginResponseDto>> LoginAsync(UserLoginRequestDto userLoginRequestDto)
        {
            var user = await _userManager.FindByNameAsync(userLoginRequestDto.Username);
            if (user is null)
                return new TypedResult<UserLoginResponseDto>(false, _messageRepo.Get(MessageKey.INVALID_USER_PASS), null);

            var passwordCheck = await _userManager.CheckPasswordAsync(user, userLoginRequestDto.Password);
            if (!passwordCheck)
                return new TypedResult<UserLoginResponseDto>(false, _messageRepo.Get(MessageKey.INVALID_USER_PASS), null);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName)
                };

            byte[] secretKeyInBytes = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);
            var authSigningKey = new SymmetricSecurityKey(secretKeyInBytes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            var loginResponse = new UserLoginResponseDto()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
            return new TypedResult<UserLoginResponseDto>(loginResponse);
        }

        public Task<TypedResult<UserRegisterResponseDto>> RegisterAsync(UserRegisterRequestDto userRegisterRequestDto)
        {
            throw new System.NotImplementedException();
        }
    }
}