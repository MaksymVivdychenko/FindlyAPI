using System.Linq.Expressions;

namespace FindlyDAL.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task<IEnumerable<TEntity>> GetAllAsync(int pageSize, int pageNumber);
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> FindSingleAsync(Expression<Func<TEntity, bool>> predicate);
}