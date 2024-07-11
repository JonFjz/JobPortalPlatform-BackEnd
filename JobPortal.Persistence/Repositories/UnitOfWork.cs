using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Interfaces;
using System.Collections;

namespace JobPortal.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dbContext;

        private Hashtable _repositories;


        public UnitOfWork(DataContext dbContext)
        {
            _dbContext = dbContext;
        }


        public bool Complete()
        {
            var numberOfAffectedRows = _dbContext.SaveChanges();
            return numberOfAffectedRows > 0;
        }

        public IJppRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.Contains(type))
            {
                var repositoryType = typeof(JppRepository<>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dbContext);

                _repositories.Add(type, repositoryInstance);
            }

            return (IJppRepository<TEntity>)_repositories[type];
        }
    }
}
