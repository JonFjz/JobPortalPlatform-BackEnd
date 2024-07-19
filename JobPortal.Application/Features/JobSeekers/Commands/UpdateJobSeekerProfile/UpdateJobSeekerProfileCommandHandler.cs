using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using MediatR;

namespace JobPortal.Application.Features.JobSeeker.Commands.UpdateJobSeeker
{
    public class UpdateJobSeekerProfileCommandHandler : IRequestHandler<UpdateJobSeekerProfileCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;

        public UpdateJobSeekerProfileCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
        }

        public async Task Handle(UpdateJobSeekerProfileCommand request, CancellationToken cancellationToken)
        {
            var jobSeekerToUpdate = await _claimsPrincipalAccessor.GetCurrentJobSeekerAsync();

            _mapper.Map(request, jobSeekerToUpdate);

            await _unitOfWork.Repository<Domain.Entities.JobSeeker>().UpdateAsync(jobSeekerToUpdate);
            _unitOfWork.Complete();

        }
    }
}
