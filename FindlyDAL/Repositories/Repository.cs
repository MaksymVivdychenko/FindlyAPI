using System.Linq.Expressions;
using FindlyDAL.DB;
using FindlyDAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindlyDAL.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly FindlyDbContext DbContext;

    public Repository(FindlyDbContext dbContext)
    {
        this.DbContext = dbContext;
    }
    public async Task AddAsync(TEntity entity)
    {
        await DbContext.Set<TEntity>().AddAsync(entity);
        await DbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);
        await DbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);
        await DbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(int pageSize, int pageNumber)
    {
        return await DbContext.Set<TEntity>().Skip((pageNumber - 1)*pageSize).Take(pageSize).AsNoTracking().ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await DbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbContext.Set<TEntity>().Where(predicate).AsNoTracking().ToListAsync();
    }

    public async Task<TEntity?> FindSingleAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbContext.Set<TEntity>().SingleOrDefaultAsync(predicate);
    }
}