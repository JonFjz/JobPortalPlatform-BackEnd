using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.Employers.Dtos;
using MediatR;

namespace JobPortal.Application.Features.Employers.Queries.GetEmployerProfile
{
    public class GetEmployerProfileQueryHandler : IRequestHandler<GetEmployerProfileQuery, EmployerDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        private readonly IMapper _mapper;

        public GetEmployerProfileQueryHandler(IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsPrincipalAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
            _mapper = mapper;
        }

        public async Task<EmployerDto> Handle(GetEmployerProfileQuery request, CancellationToken cancellationToken)
        {

            var employerToReturn = await _claimsPrincipalAccessor.GetCurrentEmployerAsync();

            var userDto = _mapper.Map<EmployerDto>(employerToReturn);
            return userDto;

        }
    }
}
