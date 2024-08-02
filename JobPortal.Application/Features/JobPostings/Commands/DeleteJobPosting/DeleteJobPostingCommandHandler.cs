using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.JobPostings.Commands.DeleteJobPosting;
using JobPortal.Domain.Entities;
using MediatR;

public class DeleteJobPostingCommandHandler : IRequestHandler<DeleteJobPostingCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
    private readonly ISearchService _searchService;

    public DeleteJobPostingCommandHandler(IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsPrincipalAccessor, ISearchService searchService)
    {
        _unitOfWork = unitOfWork;
        _claimsPrincipalAccessor = claimsPrincipalAccessor;
        _searchService = searchService;
    }

    public async Task<Unit> Handle(DeleteJobPostingCommand request, CancellationToken cancellationToken)
    {
        var employer = await _claimsPrincipalAccessor.GetCurrentEmployerAsync();

        var jobPosting = await _unitOfWork.Repository<JobPosting>().GetByIdAsync(e => e.Id == request.Id);

        if (jobPosting == null || jobPosting.EmployerId != employer.Id)
            throw new KeyNotFoundException("Job Posting not found.");

        _unitOfWork.Repository<JobPosting>().Delete(jobPosting);
        _unitOfWork.Complete();

        var deleteSuccess = await _searchService.DeleteEntry(request.Id);
        if (!deleteSuccess)
        {
            Console.WriteLine("Failed to delete job posting from Elasticsearch.");
        }

        return Unit.Value;
    }
}
