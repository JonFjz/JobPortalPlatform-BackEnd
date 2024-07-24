using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.Educations.Commands.CreateEducation;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.
    s.Commands.CreateEducation
{
    public class CreateEducationCommandHandler : IRequestHandler<CreateEducationCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        private readonly IMapper _mapper;

        public CreateEducationCommandHandler(IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsPrincipalAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateEducationCommand request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _claimsPrincipalAccessor.GetCurrentJobSeekerAsync();

            var educationToCreate = _mapper.Map<Education>(request);
            educationToCreate.JobSeekerId = jobSeeker.Id;  

            await _unitOfWork.Repository<Education>().CreateAsync(educationToCreate);
            _unitOfWork.Complete();

            return educationToCreate.Id;
        }


    }
}
