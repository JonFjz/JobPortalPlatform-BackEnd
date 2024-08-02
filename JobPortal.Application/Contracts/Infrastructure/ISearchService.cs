using System;
using JobPortal.Domain.Entities;

namespace JobPortal.Application.Contracts.Infrastructure
{
    public interface ISearchService
    {
        Task<List<JobPosting>> Search(string searchTerm);
        Task<bool> Index(JobPosting jobPosting);
        Task<bool> UpdateEntry(JobPosting jobPosting);
        Task<bool> DeleteEntry(int id);

    }
}

