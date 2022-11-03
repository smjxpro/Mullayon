namespace Mullayon.Core.Entities;

public class Tag: BaseEntity
{
    public string Name { get; set; }
    public IEnumerable<Post> Posts { get; set; }
}