using System.Threading.Tasks;
using YT.Challenge.Domain.DTOs.User;
using YT.Challenge.Domain.Models;

namespace YT.Challenge.Auth.Services
{
    public class UserService : IUserService
    {
        public Task<TypedResult<UserLoginResponseDto>> LoginAsync(UserLoginRequestDto userLoginRequestDto)
        {
            throw new System.NotImplementedException();
        }

        public Task<TypedResult<UserRegisterResponseDto>> RegisterAsync(UserRegisterRequestDto userRegisterRequestDto)
        {
            throw new System.NotImplementedException();
        }
    }
}