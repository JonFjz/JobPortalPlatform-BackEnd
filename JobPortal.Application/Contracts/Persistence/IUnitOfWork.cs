using JobPortal.Application.Contracts.Persistence.Job;
using JobPortal.Application.Interfaces;

namespace JobPortal.Application.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        bool Complete();

        public IJppRepository<TEntity> Repository<TEntity>() where TEntity : class;
      
    }
}
