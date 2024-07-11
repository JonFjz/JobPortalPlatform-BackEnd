using System.Linq.Expressions;

namespace JobPortal.Application.Interfaces
{
    public interface IJppRepository<Tentity> where Tentity : class
    {
        IQueryable<Tentity> GetByCondition(Expression<Func<Tentity, bool>> expression);
        IQueryable<Tentity> GetById(Expression<Func<Tentity, bool>> expression);
        Task<IReadOnlyList<Tentity>> GetAllAsync();
        Task CreateAsync(Tentity entity);
        void CreateRange(List<Tentity> entity);
        void Update(Tentity entity);
        void UpdateRange(List<Tentity> entity);
        void Delete(Tentity entity);
        void DeleteRange(List<Tentity> entity);
        Task SaveChangesAsync();
    }
}
