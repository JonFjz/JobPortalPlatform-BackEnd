using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Features.JobPostings.Commands.SearchJobPosting;
using JobPortal.Application.Features.JobPostings.Dtos;
using MediatR;

public class SearchJobPostingCommandHandler : IRequestHandler<SearchJobPostingCommand, List<JobPostingDto>>
{
    private readonly ISearchService _searchService;
    private readonly IMapper _mapper;

    public SearchJobPostingCommandHandler(ISearchService searchService, IMapper mapper)
    {
        _searchService = searchService;
        _mapper = mapper;
    }

    public async Task<List<JobPostingDto>> Handle(SearchJobPostingCommand request, CancellationToken cancellationToken)
    {
        var jobPostings = await _searchService.Search(request.SearchTerm);
        var jobPostingDtos = _mapper.Map<List<JobPostingDto>>(jobPostings);
        return jobPostingDtos;
    }
}
