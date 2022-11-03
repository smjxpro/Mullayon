using Mullayon.Core.Entities;
using Mullayon.Core.Enums;

namespace Mullayon.Api.Dtos;

public class PostListDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Image FeaturedImage { get; set; }
    public UserDto Author { get; set; }
    public IEnumerable<CategoryListDto> Categories { get; set; }
    public IEnumerable<TagListDto> Tags { get; set; }
    public int Stars { get; set; }
    public PublishStatus PublishStatus { get; set; }
    public SubmissionStatus SubmissionStatus { get; set; }
}