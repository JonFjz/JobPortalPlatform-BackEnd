using AutoMapper;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.Employers.Dtos;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.Employers.Queries.GetEmployerById;

public class GetEmployerByIdQueryHandler:IRequestHandler<GetEmployerByIdQuery,EmployerDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetEmployerByIdQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<EmployerDto> Handle(GetEmployerByIdQuery request, CancellationToken cancellationToken)
    {
        var employer = await _unitOfWork.Repository<Employer>().GetByIdAsync(x => x.Id == request.Id);
        if (employer == null)
        {
            throw new Exception("Employer not found!");
        }

        var employerToReturn = _mapper.Map<EmployerDto>(employer);
        return employerToReturn;
    }
}