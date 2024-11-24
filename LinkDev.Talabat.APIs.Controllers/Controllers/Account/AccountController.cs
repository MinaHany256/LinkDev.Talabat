using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction.Models._Common;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Account
{
    public class AccountController(IServiceManager serviceManager) : BaseApiController
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
            {
            var response  = await serviceManager.AuthService.LoginAsync(model);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var response = await serviceManager.AuthService.RegisterAsync(model);   
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public async Task <ActionResult<UserDto>> GetCurrentUser()
        {
            var result = await serviceManager.AuthService.GetCurrentUser(User);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var result = await serviceManager.AuthService.GetUserAddress(User);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAdress(AddressDto address)
        {
            var result = await serviceManager.AuthService.UpdateUserAddress(User, address);
            return Ok(result);
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return Ok(await serviceManager.AuthService.EmailExists(email!));
        }
    }
}
