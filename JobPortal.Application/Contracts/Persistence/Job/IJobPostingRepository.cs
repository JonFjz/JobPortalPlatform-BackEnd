using JobPortal.Application.Interfaces;
using JobPortal.Domain;

namespace JobPortal.Application.Contracts.Persistence.Job
{
    public interface IJobPostingRepository : IJppRepository<JobPosting>
    {
    }
}
