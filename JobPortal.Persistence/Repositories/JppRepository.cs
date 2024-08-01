using JobPortal.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JobPortal.Persistence.Repositories
{
    public class JppRepository<Tentity> : IJppRepository<Tentity> where Tentity : class
    {
        private readonly DbContext _dbContext;

        public JppRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Tentity entity)
        {
            await _dbContext.Set<Tentity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public void CreateRange(List<Tentity> entities)
        {
            _dbContext.Set<Tentity>().AddRange(entities);
        }

        public void Delete(Tentity entity)
        {
            _dbContext.Set<Tentity>().Remove(entity);
        }

        public void DeleteRange(List<Tentity> entities)
        {
            _dbContext.Set<Tentity>().RemoveRange(entities);
        }

        public async Task<IReadOnlyList<Tentity>> GetAllAsync()
        {
            return await _dbContext.Set<Tentity>().ToListAsync();
        }


        public async Task<List<Tentity>> GetPagedByConditionAsync(Expression<Func<Tentity, bool>> filter, int skip, int take)
        {
            return await _dbContext.Set<Tentity>()
                .Where(filter)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> CountByConditionAsync(Expression<Func<Tentity, bool>> expression)
        {
            return await _dbContext.Set<Tentity>()
                .CountAsync(expression);
        }
        public async Task<List<Tentity>> GetByConditionAsync(Expression<Func<Tentity, bool>> expression)
        {
            return await _dbContext.Set<Tentity>()
                .Where(expression)
                .ToListAsync();
        }
        public IQueryable<Tentity> GetByCondition(Expression<Func<Tentity, bool>> expression)
        {
            return _dbContext.Set<Tentity>().Where(expression);
        }
        public async Task<Tentity> GetByIdAsync(Expression<Func<Tentity, bool>> expression)
        {
            return await _dbContext.Set<Tentity>().FirstOrDefaultAsync(expression);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tentity entity)
        {
            _dbContext.Set<Tentity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }


        public void UpdateRange(List<Tentity> entities)
        {
            _dbContext.Set<Tentity>().UpdateRange(entities);
        }
    }
}
