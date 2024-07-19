using JobPortal.Application.Features.Educations.Dtos;
using MediatR;

namespace JobPortal.Application.Features.Educations.Queries.GetEducationById
{
    public class GetEducationByIdQuery : IRequest<EducationDto>
    {
        public int Id { get; set; }

        public GetEducationByIdQuery(int id)
        {
            Id = id;
        }
    }
}
