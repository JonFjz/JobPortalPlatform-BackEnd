using JobPortal.Application.Features.WorkExperiences.Dtos;
using MediatR;

namespace JobPortal.Application.Features.WorkExperiences.Queries.GetWorkExperienceById
{
    public class GetWorkExperienceByIdQuery : IRequest<WorkExperienceDto>
    {
        public int Id { get; set; }

        public GetWorkExperienceByIdQuery(int id)
        {
            Id = id;
        }
    }
}
