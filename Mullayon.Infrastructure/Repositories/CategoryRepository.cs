using Mullayon.Core.Entities;
using Mullayon.Core.Repositories;
using Mullayon.Infrastructure.Data;

namespace Mullayon.Infrastructure.Repositories;

public class CategoryRepository: GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }
}