using Mullayon.Core.Entities;

namespace Mullayon.Core.Repositories;

public interface IGenericRepository<T> where T:BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}