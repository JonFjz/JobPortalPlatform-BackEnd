using JobPortal.API.Controllers.Base;
using JobPortal.Application.Features.ScheduleInterview.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers;
[Authorize]
public class InterviewController:BaseApiController
{
    private readonly IMediator _mediator;

    public InterviewController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("Schedule-Interview")]
    public async Task<IActionResult> ScheduleInterview([FromBody] ScheduleInterviewCommand command)
    {
        var result = await _mediator.Send(command);
        if (result)
            return Ok("Interview scheduled successfully.");
        return BadRequest("Failed to schedule interview.");
    }
}