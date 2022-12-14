using Mullayon.Core.Entities;

namespace Mullayon.Core.Repositories;

public interface IPostRepository: IGenericRepository<Post>
{
    Task<IEnumerable<Post>> GetPostsByAuthorAsync(Guid id);
    Task<IEnumerable<Post>> GetPendingPostsAsync();
    Task<IEnumerable<Post>> GetRejectedPostsAsync();
}