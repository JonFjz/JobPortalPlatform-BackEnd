using JobPortal.API.Controllers.Base;
using JobPortal.Application.Features.Resumes.Commands.DeleteResume;
using JobPortal.Application.Features.Resumes.Commands.UploadResume;
using JobPortal.Application.Features.Resumes.Dtos;
using JobPortal.Application.Features.Resumes.Queries.DownloadResume;
using JobPortal.Application.Features.Resumes.Queries.GetResume;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    [Authorize]
    public class ResumesController : BaseApiController
    {
        private readonly IMediator _mediator;

        public ResumesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] UploadResumeCommand command)
        {
            var resume = await _mediator.Send(command);
            return Ok(resume);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            await _mediator.Send(new DeleteResumeCommand());
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resume = await _mediator.Send(new GetResumeQuery());
            return Ok(resume);
        }

        [HttpGet("download")]
        public async Task<IActionResult> DownloadResume()
        {
            var result = await _mediator.Send(new DownloadResumeQuery());
            return File(result.FileData, "application/octet-stream", result.FileName);
        }
    }
}
