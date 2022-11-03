using Microsoft.AspNetCore.Identity;
using Mullayon.Core.Entities;
using Mullayon.Core.Repositories;

namespace Mullayon.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationUser?> GetAsync(Guid id)
    {
        return await _userManager.FindByIdAsync(id.ToString());
    }

    public async Task<ApplicationUser?> GetAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<ApplicationUser> AddAsync(ApplicationUser user)
    {
        await _userManager.CreateAsync(user);
        return user;
    }

    public async Task UpdateAsync(ApplicationUser user)
    {
        await _userManager.UpdateAsync(user);
    }

    public async Task DeleteAsync(ApplicationUser user)
    {
        await _userManager.DeleteAsync(user);
    }
}