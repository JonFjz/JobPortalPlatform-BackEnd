using MediatR;
using Microsoft.AspNetCore.Http;

namespace JobPortal.Application.Features.Resumes.Commands.UploadResume
{
    public class UploadResumeCommand : IRequest<int>
    {
        public IFormFile File { get; set; }
    }
}
