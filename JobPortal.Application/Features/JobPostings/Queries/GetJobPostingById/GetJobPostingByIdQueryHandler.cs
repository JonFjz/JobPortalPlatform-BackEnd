using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.JobPostings.Dtos;
using JobPortal.Application.Features.JobPostings.Queries.GetJobPostingById;
using JobPortal.Application.Helpers.Models.Cashe;
using JobPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

internal class GetJobPostingByIdQueryHandler : IRequestHandler<GetJobPostingByIdQuery, JobPostingDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    public GetJobPostingByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<JobPostingDto> Handle(GetJobPostingByIdQuery request, CancellationToken cancellationToken)
    {
        var jobPosting = await _unitOfWork.Repository<JobPosting>()
            .GetByCondition(e => e.Id == request.Id)
            .Include(x => x.Employer)
            .FirstOrDefaultAsync(cancellationToken);

        if (jobPosting == null)
        {
            throw new KeyNotFoundException("Job Posting not found.");
        }

        var jobPostingDto = _mapper.Map<JobPostingDto>(jobPosting);


        return jobPostingDto;
    }
}
