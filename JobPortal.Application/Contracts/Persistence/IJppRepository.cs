using System.Linq.Expressions;

namespace JobPortal.Application.Interfaces
{
    public interface IJppRepository<Tentity> where Tentity : class
    {
        Task<List<Tentity>> GetByConditionAsync(Expression<Func<Tentity, bool>> expression);
        Task<Tentity> GetByIdAsync(Expression<Func<Tentity, bool>> expression);
        Task<IReadOnlyList<Tentity>> GetAllAsync();
        Task CreateAsync(Tentity entity);
        void CreateRange(List<Tentity> entity);
        Task UpdateAsync(Tentity entity);
        void UpdateRange(List<Tentity> entity);
        void Delete(Tentity entity);
        void DeleteRange(List<Tentity> entity);
        Task SaveChangesAsync();
    }
}
