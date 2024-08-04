using JobPortal.API.Controllers.Base;
using JobPortal.Application.Features.WorkExperiences.Commands.CreateWorkExperience;
using JobPortal.Application.Features.WorkExperiences.Commands.DeleteWorkExperience;
using JobPortal.Application.Features.WorkExperiences.Commands.UpdateWorkExperience;
using JobPortal.Application.Features.WorkExperiences.Queries.GetAllWorkExperiences;
using JobPortal.Application.Features.WorkExperiences.Queries.GetWorkExperienceById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    [Authorize(Policy ="JobSeeker")]
    public class WorkExperiencesController : BaseApiController
    {
        private readonly IMediator _mediator;

        public WorkExperiencesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWorkExperienceCommand command)
        {
            var workExperienceId = await _mediator.Send(command);
            return CreatedAtAction(nameof(Create), new { id = workExperienceId });
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateWorkExperienceCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var workExperience = await _mediator.Send(new GetWorkExperienceByIdQuery(id));
            return Ok(workExperience);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var workExperiences = await _mediator.Send(new GetAllWorkExperiencesQuery());
            return Ok(workExperiences);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteWorkExperienceCommand(id));
            return NoContent();
        }
    }
}