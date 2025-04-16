using System.Net;
using LibraryEcom.Application.Common.Response;
using LibraryEcom.Application.DTOs.Identity;
using LibraryEcom.Application.Interfaces.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryCom.Controllers.Base;

namespace LibraryCom.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController(IAuthenticationService authenticationService): BaseController<AuthenticationController>
{
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginRequest)
    {
        var result = await authenticationService.Login(loginRequest);

        return Ok(new ResponseDto<UserLoginResponseDto>()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Successfully authenticated.",
            Result = result,
        });
    }

    // [HttpPost("user-registration")]
    // public async Task<IActionResult> Register([FromBody] RegisterDto registerRequest)
    // {
    //     var result = await authenticationService.
    // }
}