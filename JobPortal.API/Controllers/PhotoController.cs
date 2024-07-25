using JobPortal.API.Controllers.Base;
using JobPortal.Application.Features.Photos.Commands.DeletePhoto;
using JobPortal.Application.Features.Photos.Commands.UploadPhoto;
using JobPortal.Application.Features.Photos.Queries.DownloadPhoto;
using JobPortal.Application.Features.Photos.Queries.GetPhoto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    [Authorize]
    public class PhotoController : BaseApiController
    {
        private readonly IMediator _mediator;

        public PhotoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] UploadPhotoCommand command)
        {
            var photo = await _mediator.Send(command);
            return Ok(photo);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            await _mediator.Send(new DeletePhotoCommand());
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var photo = await _mediator.Send(new GetPhotoQuery());
            return Ok(photo);
        }

        [HttpGet("download")]
        public async Task<IActionResult> Download()
        {
            var result = await _mediator.Send(new DownloadPhotoQuery());
            return File(result.FileData, "application/octet-stream", result.FileName);
        }
    }
}

