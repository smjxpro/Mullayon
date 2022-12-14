namespace Mullayon.Core.Entities;

public class Category:BaseEntity
{
    public string Name { get; set; }
    public Image Image { get; set; }
    public string Description { get; set; }
    public IEnumerable<Post> Posts { get; set; }
}