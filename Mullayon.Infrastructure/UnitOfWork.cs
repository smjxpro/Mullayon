using Mullayon.Core;
using Mullayon.Core.Repositories;
using Mullayon.Infrastructure.Data;
using Mullayon.Infrastructure.Repositories;

namespace Mullayon.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;

        //Repositories
        Post = new PostRepository(_context);
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public IPostRepository Post { get; set; }
}