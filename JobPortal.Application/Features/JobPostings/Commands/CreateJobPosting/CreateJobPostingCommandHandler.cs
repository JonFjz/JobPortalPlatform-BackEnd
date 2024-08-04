using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.JobPostings.Commands.CreateJobPosting;
using JobPortal.Application.Features.JobPostings.Dtos;
using JobPortal.Domain.Entities;
using MediatR;
using Newtonsoft.Json;

public class CreateJobPostingCommandHandler : IRequestHandler<CreateJobPostingCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
    private readonly IMapper _mapper;
    private readonly ISearchService _searchService;

    public CreateJobPostingCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsPrincipalAccessor, ISearchService searchService)
    {
        _unitOfWork = unitOfWork;
        _claimsPrincipalAccessor = claimsPrincipalAccessor;
        _mapper = mapper;
        _searchService = searchService;
    }

    public async Task<int> Handle(CreateJobPostingCommand request, CancellationToken cancellationToken)
    {
        var employer = await _claimsPrincipalAccessor.GetCurrentEmployerAsync();
        var jobPostingToCreate = _mapper.Map<JobPosting>(request);

        jobPostingToCreate.Employer = employer;

        await _unitOfWork.Repository<JobPosting>().CreateAsync(jobPostingToCreate);
        _unitOfWork.Complete();

        var jobPostingDto = _mapper.Map<JobPostingDto>(jobPostingToCreate);

        jobPostingDto.CompanyName = employer.CompanyName;

        Console.WriteLine($"JobPostingDto: {JsonConvert.SerializeObject(jobPostingDto)}");

        var indexingSuccess = await _searchService.Index(jobPostingDto);

        if (!indexingSuccess)
        {
            Console.WriteLine("Failed to index job posting.");
        }

        return jobPostingToCreate.Id;
    }

}
