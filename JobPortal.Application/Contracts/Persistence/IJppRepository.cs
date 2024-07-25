using System.Linq.Expressions;

namespace JobPortal.Application.Interfaces
{
    public interface IJppRepository<Tentity> where Tentity : class
    {
        Task<List<Tentity>> GetByConditionAsync(Expression<Func<Tentity, bool>> expression);
        Task<Tentity> GetByIdAsync(Expression<Func<Tentity, bool>> expression);
        Task<List<Tentity>> GetPagedByConditionAsync(Expression<Func<Tentity, bool>> expression, int skip, int take);
        Task<int> CountByConditionAsync(Expression<Func<Tentity, bool>> filter);
        Task<IReadOnlyList<Tentity>> GetAllAsync();
        Task CreateAsync(Tentity entity);
        void CreateRange(List<Tentity> entity);
        Task UpdateAsync(Tentity entity);
        void UpdateRange(List<Tentity> entity);
        void Delete(Tentity entity);
        void DeleteRange(List<Tentity> entity);
        Task SaveChangesAsync();
        IQueryable<Tentity> GetByCondition(Expression<Func<Tentity, bool>> expression);

    }
}
