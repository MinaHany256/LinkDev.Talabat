using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;
using System.Security.Claims;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services.Auth
{
    public interface IAuthService
    {
        Task<UserDto> LoginAsync(LoginDto model);

        Task<UserDto> RegisterAsync(RegisterDto model);

        Task<UserDto> GetCurrentUser(ClaimsPrincipal claimsPrincipal);
    }
}
