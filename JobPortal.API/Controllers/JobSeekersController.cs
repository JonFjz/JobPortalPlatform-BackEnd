using JobPortal.API.Controllers.Base;
using JobPortal.Application.Contracts.Infrastructure;
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
        private readonly IBlobStorageService _blobService;

        public JobSeekersController(IMediator mediator, IBlobStorageService blobService)
        {
                _blobService = blobService;
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
