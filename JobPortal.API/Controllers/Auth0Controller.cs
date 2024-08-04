using JobPortal.API.Controllers.Base;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Features.Employers.Dtos;
using JobPortal.Application.Features.JobSeeker.Dtos;
using JobPortal.Application.Helpers.Models.Auth0;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    public class Auth0Controller : BaseApiController
    {
        private readonly IAuth0Service _auth0Service;


        public Auth0Controller(IAuth0Service auth0Service)
        {
            _auth0Service = auth0Service;
        }


        [HttpPost("register-as-a-jobSeeker")]
        public async Task<IActionResult> RegisterAsAJobSeeker([FromBody] JobSeekerRegistrationDto request)
        {
            var result = await _auth0Service.SignupJobSeekerAsync(request, "JobSeeker");
            return Ok(result);
        }

        [HttpPost("register-as-a-employer")]
        public async Task<IActionResult> RegisterAsAEmployer([FromBody] EmployerRegistrationDto request)
        {
            var result = await _auth0Service.SignupEmployerAsync(request, "Employer");
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Auth0TokenRequest tokenRequest)
        {
            var result = await _auth0Service.GetTokenAsync(tokenRequest);
            return Ok(result);
        }
    }
}