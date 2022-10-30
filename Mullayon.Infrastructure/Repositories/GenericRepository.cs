using Microsoft.EntityFrameworkCore;
using Mullayon.Core.Repositories;
using Mullayon.Infrastructure.Data;

namespace Mullayon.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class

{
    protected readonly ApplicationDbContext Context;

    public GenericRepository(ApplicationDbContext context)
    {
        Context = context;
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await Context.Set<T>().FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        return entity;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        Context.Set<T>().Update(entity);
    }

    public virtual async Task DeleteAsync(T entity)
    {
        Context.Set<T>().Remove(entity);
    }
}