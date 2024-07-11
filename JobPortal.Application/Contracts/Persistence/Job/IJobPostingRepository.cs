using JobPortal.Application.Interfaces;
using JobPortal.Domain.Entities;

namespace JobPortal.Application.Contracts.Persistence.Job
{
    public interface IJobPostingRepository : IJppRepository<JobPosting>
    {
    }
}
