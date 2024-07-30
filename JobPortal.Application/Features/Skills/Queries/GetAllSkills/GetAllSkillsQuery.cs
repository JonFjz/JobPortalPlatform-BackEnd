using JobPortal.Application.Features.Skills.Dtos;
using MediatR;

namespace JobPortal.Application.Features.Skills.Queries.GetAllSkills
{
    public class GetAllSkillsQuery : IRequest<List<SkillDto>>
    {
    }
}