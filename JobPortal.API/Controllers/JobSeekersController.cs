using JobPortal.API.Controllers.Base;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Features.JobSeeker.Commands.UpdateJobSeeker;
using JobPortal.Application.Features.JobSeeker.Queries.GetJobSeekerDetail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    [Authorize(Policy = "JobSeeker")]
    public class JobSeekersController : BaseApiController
    {
        private readonly IMediator _mediator;

        public JobSeekersController(IMediator mediator)
        {
                _mediator = mediator;
        }


        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userDetails = await _mediator.Send(new GetJobEmployerProfileQuery());
            return Ok(userDetails);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateJobSeekerProfileCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

    }
}
