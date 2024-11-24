using LinkDev.Talabat.Core.Application.Abstraction.Models._Common;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;
using System.Security.Claims;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services.Auth
{
    public interface IAuthService
    {
        Task<UserDto> LoginAsync(LoginDto model);

        Task<UserDto> RegisterAsync(RegisterDto model);

        Task<UserDto> GetCurrentUser(ClaimsPrincipal claimsPrincipal);

        Task<AddressDto?> GetUserAddress(ClaimsPrincipal claimsPrincipal);

        Task<AddressDto> UpdateUserAddress(ClaimsPrincipal claimsPrincipal, AddressDto model);

    }
}
