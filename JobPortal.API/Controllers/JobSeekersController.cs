using JobPortal.API.Controllers.Base;
using JobPortal.Application.Features.JobSeeker.Commands.UpdateJobSeeker;
using JobPortal.Application.Features.JobSeeker.Queries.GetJobSeekerDetail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    [Authorize]
    public class JobSeekersController : BaseApiController
    {
        private readonly IMediator _mediator;


        public JobSeekersController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("details")]
        public async Task<IActionResult> GetProfile()
        {
            var query = new GetJobSeekerProfileQuery();
            var userDetails = await _mediator.Send(query);
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
