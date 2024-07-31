using JobPortal.Application.Features.Employers.Dtos;
using MediatR;

namespace JobPortal.Application.Features.Employers.Queries.GetEmployerById;

public class GetEmployerByIdQuery:IRequest<EmployerDto>
{
    public int Id { get; set; }

    public GetEmployerByIdQuery(int id)
    {
        Id = id;
    }
}