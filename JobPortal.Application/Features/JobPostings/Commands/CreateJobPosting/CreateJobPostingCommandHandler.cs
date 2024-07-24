using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.JobPostings.Commands.CreateJobPosting
{
    public class CreateJobPostingCommandHandler: IRequestHandler<CreateJobPostingCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        private readonly IMapper _mapper;

        public CreateJobPostingCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _unitOfWork = unitOfWork;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateJobPostingCommand request, CancellationToken cancellationToken)
        {

            var employer = await _claimsPrincipalAccessor.GetCurrentEmployerAsync();

            var jobPostingToCreate = _mapper.Map<JobPosting>(request);
            jobPostingToCreate.EmployerId = employer.Id;

            await _unitOfWork.Repository<JobPosting>().CreateAsync(jobPostingToCreate);
            _unitOfWork.Complete();

            return jobPostingToCreate.Id;
        }
    }
}
