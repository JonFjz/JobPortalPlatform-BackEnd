﻿using JobPortal.API.Controllers.Base;
using JobPortal.Application.Features.JobPostings.Commands.CreateJobPosting;
using JobPortal.Application.Features.JobPostings.Dtos;
using JobPortal.Application.Features.JobPostings.Queries.GetAllJobPostings;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    [Authorize]
    public class JobPostingsController : BaseApiController
    {
        private readonly IMediator _mediator;
        public JobPostingsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [Authorize(Policy = "JobSeeker")]
        [HttpPost]
        public async Task<ActionResult> Post(CreateJobPostingCommand command)
        {
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = response });
        }


        [Authorize(Policy = "Employer")]
        [HttpGet]
        public async Task<ActionResult<List<JobPostingDto>>> Get()
        {
            var jobPostings = await _mediator.Send(new GetAllJobPostingsQuery());
            return Ok(jobPostings);
        }
    }
}
