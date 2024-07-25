using JobPortal.API.Controllers.Base;
using JobPortal.Application.Features.Employers.Commands.UpdateEmployerProfile;
using JobPortal.Application.Features.Employers.Queries.GetEmployerProfile;
using JobPortal.Application.Features.Employers.Queries.GetEmployersByIndustry;
using JobPortal.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    [Authorize]
    public class EmployersController : BaseApiController
    {

        private readonly IMediator _mediator;


        public EmployersController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("by-industry")]
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

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var query = new GetEmployerProfileQuery();
            var userDetails = await _mediator.Send(query);
            return Ok(userDetails);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateEmployerProfileCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

    }
}
