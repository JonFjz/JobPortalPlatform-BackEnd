using JobPortal.API.Controllers.Base;
using JobPortal.Application.Features.Employers.Commands.UpdateEmployerProfile;
using JobPortal.Application.Features.Employers.Queries.GetEmployerById;
using JobPortal.Application.Features.Employers.Queries.GetEmployerProfile;
using JobPortal.Application.Features.Employers.Queries.GetEmployersByIndustry;
using JobPortal.Application.Helpers.Models.Cashe;
using JobPortal.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    public class EmployersController : BaseApiController
    {

        private readonly IMediator _mediator;


        public EmployersController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("by-industry")]
       // [Cached(600)]
        public async Task<IActionResult> GetEmployersByIndustry([FromQuery] Industry industry, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var employersByIndustry = await _mediator.Send(new GetEmployersByIndustryQuery(industry, pageNumber, pageSize));
            return Ok(employersByIndustry);
        }

        [HttpGet("industries")]
        public IActionResult GetIndustries()
        {
            var industries = Enum.GetValues(typeof(Industry)).Cast<Industry>().Select(i => i.ToString()).ToList();
            return Ok(industries);
        }

        [Authorize(Policy = "Employer")]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userDetails = await _mediator.Send(new GetEmployerProfileQuery());
            return Ok(userDetails);
        }

        [Authorize(Policy = "JobSeeker")]
        [HttpGet("{employerId}")]
      //  [Cached(600)]
        public async Task<IActionResult> GetEmployerById(int employerId)
        {
            try
            {
                var employer = await _mediator.Send(new GetEmployerByIdQuery(employerId));
                return Ok(employer);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [Authorize(Policy = "Employer")]
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateEmployerProfileCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

    }
}
