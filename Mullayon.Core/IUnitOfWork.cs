using Mullayon.Core.Repositories;

namespace Mullayon.Core;

public interface IUnitOfWork
{
    Task CommitAsync();

    public IPostRepository Post { get; set; }
}