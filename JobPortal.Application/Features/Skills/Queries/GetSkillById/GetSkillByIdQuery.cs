using JobPortal.Application.Features.Skills.Dtos;
using MediatR;

namespace JobPortal.Application.Features.Skills.Queries.GetSkillById
{
    public class GetSkillByIdQuery : IRequest<SkillDto>
    {
        public int Id { get; set; }

        public GetSkillByIdQuery(int id)
        {
            Id = id;
        }
    }
}