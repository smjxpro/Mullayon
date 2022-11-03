using Microsoft.EntityFrameworkCore;
using Mullayon.Core.Entities;
using Mullayon.Core.Repositories;
using Mullayon.Infrastructure.Data;

namespace Mullayon.Infrastructure.Repositories;

public class CategoryRepository: GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await Context.Categories
            .Include(c => c.Image)
            .ToListAsync();
    }

    public override Task<Category?> GetByIdAsync(Guid id)
    {
        return Context.Categories
            .Include(c => c.Posts)
            .Include(c => c.Image)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}