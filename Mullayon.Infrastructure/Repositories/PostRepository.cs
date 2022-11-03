using Microsoft.EntityFrameworkCore;
using Mullayon.Core.Entities;
using Mullayon.Core.Enums;
using Mullayon.Core.Repositories;
using Mullayon.Infrastructure.Data;

namespace Mullayon.Infrastructure.Repositories;

public class PostRepository : GenericRepository<Post>, IPostRepository
{
    public PostRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Post>> GetPostsByAuthorAsync(Guid id)
    {
        return await Context.Posts.Where(p => p.Author.Id == id.ToString()).ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetPendingPostsAsync()
    {
        return await Context.Posts
            .Include(p => p.Categories)
            .Include(p => p.Tags)
            .Include(p => p.Images)
            .Include(p => p.Author)
            .Where(p => p.SubmissionStatus == SubmissionStatus.Pending)
            .ToListAsync();
    }

    public async Task<Post?> GetPendingPostAsync(Guid id)
    {
        return await Context.Posts
            .Include(p => p.Categories)
            .Include(p => p.Tags)
            .Include(p => p.Images)
            .Include(p => p.Author)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Post>> GetRejectedPostsAsync()
    {
        return await Context.Posts
            .Include(p => p.Categories)
            .Include(p => p.Tags)
            .Include(p => p.Images)
            .Include(p => p.Author)
            .Where(p => p.SubmissionStatus == SubmissionStatus.Rejected)
            .ToListAsync();
    }

    public override async Task<IEnumerable<Post>> GetAllAsync()
    {
        return await Context.Posts
            .Include(p => p.Categories)
            .Include(p => p.Tags)
            .Include(p => p.Images)
            .Include(p => p.Author)
            .ToListAsync();
    }

    public override async Task<Post?> GetByIdAsync(Guid id)
    {
        return await Context.Posts
            .Include(p => p.Categories)
            .Include(p => p.Tags)
            .Include(p => p.Images)
            .Include(p => p.Author)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}