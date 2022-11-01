namespace Mullayon.Core.Entities;

public class Post
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Image FeaturedImage { get; set; }
    public IEnumerable<Image> Images { get; set; }
    public ApplicationUser Author { get; set; }
    public IEnumerable<Category> Categories { get; set; }

}