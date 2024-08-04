using JobPortal.API.Controllers.Base;
using JobPortal.Application.Features.Educations.Commands.CreateEducation;
using JobPortal.Application.Features.Educations.Commands.DeleteEducation;
using JobPortal.Application.Features.Educations.Commands.UpdateEducation;
using JobPortal.Application.Features.Educations.Queries.GetAllEducations;
using JobPortal.Application.Features.Educations.Queries.GetEducationById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    [Authorize(Policy ="JobSeeker")]
    public class EducationsController : BaseApiController
    {
        private readonly IMediator _mediator;

        public EducationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEducationCommand command)
        {
            var educationId = await _mediator.Send(command);
            return CreatedAtAction(nameof(Create), new { id = educationId });
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateEducationCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var education = await _mediator.Send(new GetEducationByIdQuery(id));
            return Ok(education);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var educations = await _mediator.Send(new GetAllEducationsQuery());
            return Ok(educations);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteEducationCommand(id));
            return NoContent();
        }
    }
}