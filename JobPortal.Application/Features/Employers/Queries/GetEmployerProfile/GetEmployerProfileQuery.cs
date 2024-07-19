using JobPortal.Application.Features.Employers.Dtos;
using MediatR;

namespace JobPortal.Application.Features.Employers.Queries.GetEmployerProfile
{
    public class GetEmployerProfileQuery : IRequest<EmployerDto>{}
}
