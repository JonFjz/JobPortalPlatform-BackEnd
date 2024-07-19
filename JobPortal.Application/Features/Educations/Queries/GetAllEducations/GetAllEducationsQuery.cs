using JobPortal.Application.Features.Educations.Dtos;
using MediatR;

namespace JobPortal.Application.Features.Educations.Queries.GetAllEducations
{
    public class GetAllEducationsQuery  : IRequest<List<EducationDto>>
    {
    }
}