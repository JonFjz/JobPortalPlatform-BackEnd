using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Features.Users.Dtos;
using JobPortal.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers.User
{

    public class AccountController : BaseApiController
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginDto loginDto)
        {
            return Ok(await _authService.Login(loginDto));
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(RegisterDto registerDto)
        {
            return Ok(await _authService.Register(registerDto));
        }
    }
}
