using JobPortal.API.Controllers.Base;
using JobPortal.Application.Features.Photos.Commands.DeletePhoto;
using JobPortal.Application.Features.Photos.Commands.UploadPhoto;
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

        [HttpPost("photo")]
        public async Task<IActionResult> UploadPhoto([FromForm] UploadPhotoCommand command)
        {
            var photoId = await _mediator.Send(command);
            return Ok(new { PhotoId = photoId });
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
            if (photo == null)
            {
                return NotFound();
            }

            return photo;
        }

    }
}

