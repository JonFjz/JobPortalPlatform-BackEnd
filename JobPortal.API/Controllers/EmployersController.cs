using JobPortal.API.Controllers.Base;
using JobPortal.Application.Features.Employers.Commands.UpdateEmployerProfile;
using JobPortal.Application.Features.Employers.Queries.GetEmployerProfile;
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
