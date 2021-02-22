using System.Threading.Tasks;
using YT.Challenge.Domain.DTOs.User;
using YT.Challenge.Domain.Models;

namespace YT.Challenge.Auth.Services
{
    public interface IUserService
    {
        Task<TypedResult<UserLoginResponseDto>> LoginAsync(UserLoginRequestDto userLoginRequestDto);
        Task<TypedResult<UserRegisterResponseDto>> RegisterAsync(UserRegisterRequestDto userRegisterRequestDto);
    }
}