using JobPortal.API.Controllers.Base;
using JobPortal.Application.Features.JobPostings.Commands.CreateJobPosting;
using JobPortal.Application.Features.JobPostings.Commands.DeleteJobPosting;
using JobPortal.Application.Features.JobPostings.Commands.UpdateJobPosting;
using JobPortal.Application.Features.JobPostings.Queries.GetAllJobPostings;
using JobPortal.Application.Features.JobPostings.Queries.GetJobPostingById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{

    public class JobPostingsController : BaseApiController
    {
        private readonly IMediator _mediator;
        public JobPostingsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [Authorize(Policy = "Employer")]
        [HttpPost]
        public async Task<ActionResult> Create(CreateJobPostingCommand command)
        {
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(Create), new { id = response });
        }


        [Authorize(Policy = "Employer")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateJobPostingCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var jobPosting = await _mediator.Send(new GetJobPostingByIdQuery(id));
            return Ok(jobPosting);
        }


        [HttpGet("by-employer/{employerId}")]
        public async Task<IActionResult> GetJobPostingsByEmployer(int employerId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var jobPostingsByEmployer = await _mediator.Send(new GetJobPostingsByEmployerQuery(employerId, pageNumber, pageSize));
            return Ok(jobPostingsByEmployer);
        }


        [Authorize(Policy = "Employer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteJobPostingCommand(id));
            return NoContent();
        }
    }
}
