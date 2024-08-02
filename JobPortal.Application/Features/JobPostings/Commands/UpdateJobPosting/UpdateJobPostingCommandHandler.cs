using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.JobPostings.Commands.UpdateJobPosting
{
    public class UpdateJobPostingCommandHandler : IRequestHandler<UpdateJobPostingCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        private readonly IMapper _mapper;
        private readonly ISearchService _searchService;

        public UpdateJobPostingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimsPrincipalAccessor claimsPrincipalAccessor, ISearchService searchService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
            _searchService = searchService;
        }

        public async Task<Unit> Handle(UpdateJobPostingCommand request, CancellationToken cancellationToken)
        {
            var employer = await _claimsPrincipalAccessor.GetCurrentEmployerAsync();

            var jobPosting = await _unitOfWork.Repository<JobPosting>().GetByIdAsync(e => e.Id == request.Id);

            if (jobPosting == null || jobPosting.EmployerId != employer.Id)
            {
                throw new KeyNotFoundException("Job Posting not found.");
            }

            _mapper.Map(request, jobPosting);

            await _unitOfWork.Repository<JobPosting>().UpdateAsync(jobPosting);
            _unitOfWork.Complete();

            var updateSuccess = await _searchService.UpdateEntry(jobPosting);
            if (!updateSuccess)
            {
                Console.WriteLine("Failed to update job posting in Elasticsearch.");
            }

            return Unit.Value;
        }
    }
}