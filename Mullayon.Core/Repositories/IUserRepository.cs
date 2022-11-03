using Mullayon.Core.Entities;

namespace Mullayon.Core.Repositories;

public interface IUserRepository
{
    Task<ApplicationUser?> GetAsync(Guid id);
    Task<ApplicationUser?> GetAsync(string email);
    Task<ApplicationUser> AddAsync(ApplicationUser user);
    Task UpdateAsync(ApplicationUser user);
    Task DeleteAsync(ApplicationUser user);
}