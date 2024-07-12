using JobPortal.API.Controllers.Base;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Helpers.Models.Auth0;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    public class Auth0Controller : BaseApiController
    {
        private readonly IAuth0Service _auth0Service;
        private readonly IConfiguration _configuration;


        public Auth0Controller(IAuth0Service auth0Service, IConfiguration configuration)
        {
            _auth0Service = auth0Service;
            _configuration = configuration;
        }


        [HttpPost("register-as-a-jobSeeker")]
        public async Task<IActionResult> RegisterAsAJobSeeker([FromBody] Auth0SignupRequest request)
        {
            var result = await _auth0Service.SignupUserAsync(request, "JobSeeker");
            return Ok(result);
        }

        [HttpPost("register-as-a-employer")]
        public async Task<IActionResult> RegisterAsAEmployer([FromBody] Auth0SignupRequest request)
        {
            var result = await _auth0Service.SignupUserAsync(request, "Employer");
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> GetToken([FromBody] Auth0TokenRequest tokenRequest)
        {
            var result = await _auth0Service.GetTokenAsync(tokenRequest);
            return Ok(result);
        }

    }
}
