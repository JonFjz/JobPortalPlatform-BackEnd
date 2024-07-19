using JobPortal.Application.Features.WorkExperiences.Dtos;
using MediatR;

namespace JobPortal.Application.Features.WorkExperiences.Queries.GetAllWorkExperiences
{
    public class GetAllWorkExperiencesQuery : IRequest<List<WorkExperienceDto>>
    {
    }
}