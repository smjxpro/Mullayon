using Microsoft.AspNetCore.Identity;
using Mullayon.Core;
using Mullayon.Core.Entities;
using Mullayon.Core.Repositories;
using Mullayon.Infrastructure.Data;
using Mullayon.Infrastructure.Repositories;

namespace Mullayon.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;

        //Repositories
        Post = new PostRepository(_context);
        Category = new CategoryRepository(_context);
        Tag = new TagRepository(_context);
        Image = new ImageRepository(_context);
        User = new UserRepository(userManager);
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public IPostRepository Post { get; set; }
    public ICategoryRepository Category { get; set; }
    public ITagRepository Tag { get; set; }
    public IImageRepository Image { get; set; }
    public IUserRepository User { get; set; }
}