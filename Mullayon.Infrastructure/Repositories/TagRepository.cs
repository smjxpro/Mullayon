using Microsoft.EntityFrameworkCore;
using Mullayon.Core.Entities;
using Mullayon.Core.Repositories;
using Mullayon.Infrastructure.Data;

namespace Mullayon.Infrastructure.Repositories;

public class TagRepository: GenericRepository<Tag>,ITagRepository
{
    public TagRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override Task<Tag?> GetByIdAsync(Guid id)
    {
        return Context.Tags
            .Include(t => t.Posts)
            .FirstOrDefaultAsync(t => t.Id == id);
    }
}