using JobPortal.API.Controllers.Base;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Features.JobPostings.Commands.CreateJobPosting;
using JobPortal.Application.Features.JobPostings.Commands.DeleteJobPosting;
using JobPortal.Application.Features.JobPostings.Commands.SearchJobPosting;
using JobPortal.Application.Features.JobPostings.Commands.UpdateJobPosting;
using JobPortal.Application.Features.JobPostings.Queries.GetJobPostingById;
using JobPortal.Application.Features.JobPostings.Queries.GetJobPostingsByEmployerId;
using JobPortal.Application.Features.JobPostings.Queries.GetMyJobPosting;
using JobPortal.Application.Features.JobPostings.Dtos;
using JobPortal.Application.Features.JobPostings.Queries.GetRecommendJob;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JobPortal.Application.Helpers.Models.Cashe;


namespace JobPortal.API.Controllers
{
    public class JobPostingsController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly ISearchService _searchService;

        public JobPostingsController(IMediator mediator, ISearchService searchService)
        {
            _mediator = mediator;
            _searchService = searchService;
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
       // [Cached(600)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var jobPosting = await _mediator.Send(new GetJobPostingByIdQuery(id));
            return Ok(jobPosting);
        }


        [HttpGet("by-employer/{employerId}")]
       // [Cached(300)]
        public async Task<IActionResult> GetJobPostingsByEmployer(int employerId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var jobPostingsByEmployer = await _mediator.Send(new GetJobPostingsByEmployerIdQuery(employerId, pageNumber, pageSize));
            return Ok(jobPostingsByEmployer);
        }


        [Authorize(Policy = "Employer")]
        [HttpGet("my-premium-job-postings")]
       // [Cached(300)]
        public async Task<IActionResult> GetMyPremiumJobPostings([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {

            var jobPostings = await _mediator.Send(new GetMyPremiumJobPostingQuery(pageNumber, pageSize));
            return Ok(jobPostings);
        }


        [Authorize(Policy = "Employer")]
        [HttpGet("my-job-postings")]
        //[Cached(300)]
        public async Task<IActionResult> GetMyJobPostings([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var jobPostings = await _mediator.Send(new GetMyJobPostingQuery(pageNumber, pageSize));
            return Ok(jobPostings);
        }


        [Authorize(Policy = "Employer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteJobPostingCommand(id));
            return NoContent();
        }


        [HttpGet("search")]
        public async Task<ActionResult<List<JobPostingDto>>> Search(string searchTerm)
        {
            var result = await _mediator.Send(new SearchJobPostingCommand(searchTerm));
            return Ok(result);
        }
        
        [Authorize(Policy = "JobSeeker")]
        [HttpPost("recommendations")]
        public async Task<IActionResult> RecommendJobs()
        {
            var recommendedJobs = await _mediator.Send(new GetRecommendJobQuery());
            return Ok(recommendedJobs);
        }

    }
}