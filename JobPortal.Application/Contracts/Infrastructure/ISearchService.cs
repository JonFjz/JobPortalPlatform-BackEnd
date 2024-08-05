using JobPortal.Application.Features.JobPostings.Dtos;
using JobPortal.Domain.Entities;

namespace JobPortal.Application.Contracts.Infrastructure
{
    public interface ISearchService
    {
        Task<List<JobPostingDto>> Search(string searchTerm);
        Task<bool> Index(JobPostingDto jobPosting);
        Task<bool> UpdateEntry(JobPosting jobPosting);
        Task<bool> DeleteEntry(int id);
        Task<List<JobPostingDto>> RecommendJobsAsync(JobSeeker jobSeeker);

    }
}