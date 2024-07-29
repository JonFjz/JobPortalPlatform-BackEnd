using JobPortal.Application.Features.Resumes.Dtos;
using MediatR;

namespace JobPortal.Application.Features.Resumes.Queries.DownloadResumeForEmployers
{
    public class DownloadResumeForEmployerQuery : IRequest<DownloadResumeResult>
    {
        public int JobApplicationId { get; set; }
    }
}
