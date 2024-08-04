using JobPortal.API.Controllers.Base;
using JobPortal.Application.Features.Skills.Commands.CreateSkill;
using JobPortal.Application.Features.Skills.Commands.DeleteSkill;
using JobPortal.Application.Features.Skills.Commands.UpdateSkill;
using JobPortal.Application.Features.Skills.Queries.GetAllSkills;
using JobPortal.Application.Features.Skills.Queries.GetSkillById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace JobPortal.API.Controllers
{
    [Authorize(Policy ="JobSeeker")]
    public class SkillsController : BaseApiController
    {
        private readonly IMediator _mediator;

        public SkillsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSkillCommand command)
        {
            var skillId = await _mediator.Send(command);
            return CreatedAtAction(nameof(Create), new { id = skillId });
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateSkillCommand command)
        {

            await _mediator.Send(command);
            return NoContent();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var skill = await _mediator.Send(new GetSkillByIdQuery(id));
            return Ok(skill);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var skills = await _mediator.Send(new GetAllSkillsQuery());
            return Ok(skills);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteSkillCommand(id));
            return NoContent();
        }
    }
}