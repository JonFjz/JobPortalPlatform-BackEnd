using JobPortal.Application.Features.JobApplications.Command.ApplyToJob;
using JobPortal.Application.Features.JobApplications.Command.UpdateJobApplicationStatus;
using JobPortal.Application.Features.JobApplications.Dtos;
using JobPortal.Application.Features.JobApplications.Queries.GetJobApplicationsForJobPosting;
using JobPortal.Application.Features.JobApplications.Queries.GetJobPostingByJobSeeker;
using JobPortal.Application.Helpers.Models.Cashe;
using JobPortal.Application.Helpers.Models.Pagination;
using JobPortal.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    public class JobApplicationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobApplicationsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [Authorize(Policy ="JobSeeker")]
        [HttpPost("apply")]
        public async Task<IActionResult> Apply([FromForm] ApplyToJobCommand command)
        {
            var applicationId = await _mediator.Send(command);
            return Ok(applicationId);
        }


        [Authorize(Policy = "Employer")]
        [HttpGet("{jobPostingId}/applications")]
       // [Cached(600)]
        public async Task<ActionResult<PagedResult<JobApplicationDto>>> GetJobApplications(int jobPostingId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var jobApplications = await _mediator.Send(new GetJobApplicationsQuery(jobPostingId, pageNumber, pageSize));
            return Ok(jobApplications);
        }


        [Authorize(Policy = "Employer")]
        [HttpPut("{id}/update-status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] JobApplicationStatus newStatus)
        {
            try
            {
                await _mediator.Send(new UpdateJobApplicationStatusCommand { JobApplicationId = id, NewStatus = newStatus });
                return NoContent();

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


        [Authorize(Policy = "JobSeeker")]
        [HttpGet("by-jobseeker")]
      //  [Cached(600)]
        public async Task<ActionResult<PagedResult<JobApplicatinForJobSeekerDto>>> GetJobApplicationsByJobSeeker([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var jobApplications = await _mediator.Send(new GetJobApplicationByJobSeekerQuery(pageNumber, pageSize));
            return Ok(jobApplications);
        }
    }
}
