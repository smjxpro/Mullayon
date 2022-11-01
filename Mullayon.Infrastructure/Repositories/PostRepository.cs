using Microsoft.EntityFrameworkCore;
using Mullayon.Core.Entities;
using Mullayon.Core.Repositories;
using Mullayon.Infrastructure.Data;

namespace Mullayon.Infrastructure.Repositories;

public class PostRepository: GenericRepository<Post>, IPostRepository
{
    public PostRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Post>> GetPostsByAuthorAsync(Guid id)
    {
        return await Context.Posts.Where(p => p.Author.Id == id.ToString()).ToListAsync();
    }

    public override async Task<IEnumerable<Post>> GetAllAsync()
    {
        return await Context.Posts.Include(p => p.Author).ToListAsync();
    }
}