using JobPortal.Application.Features.Resumes.Dtos;
using MediatR;

namespace JobPortal.Application.Features.Resumes.Queries.GetResume
{
    public class GetResumeQuery : IRequest<ResumeDto>
    {
    }

}
