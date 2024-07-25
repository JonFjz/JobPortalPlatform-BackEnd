using MediatR;
using Microsoft.AspNetCore.Http;

namespace JobPortal.Application.Features.Photos.Commands.UploadPhoto
{
    public class UploadPhotoCommand : IRequest<int>
    {
        public IFormFile File { get; set; }
    }
}

