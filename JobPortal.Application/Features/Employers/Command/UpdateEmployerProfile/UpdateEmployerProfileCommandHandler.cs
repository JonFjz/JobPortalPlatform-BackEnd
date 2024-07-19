using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using MediatR;

namespace JobPortal.Application.Features.Employers.Commands.UpdateEmployerProfile
{
    public class UpdateEmployerProfileCommandHandler : IRequestHandler<UpdateEmployerProfileCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;

        public UpdateEmployerProfileCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
        }

        public async Task Handle(UpdateEmployerProfileCommand request, CancellationToken cancellationToken)
        {
            var employerToUpdate = await _claimsPrincipalAccessor.GetCurrentEmployerAsync();

            _mapper.Map(request, employerToUpdate);

            await _unitOfWork.Repository<Domain.Entities.Employer>().UpdateAsync(employerToUpdate);
            _unitOfWork.Complete();

        }
    }
}
