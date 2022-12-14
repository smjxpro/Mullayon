using Mullayon.Core.Enums;

namespace Mullayon.Core.Entities;

public class Post : BaseEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Image FeaturedImage { get; set; }
    public IEnumerable<Image> Images { get; set; }
    public ApplicationUser Author { get; set; }
    public IEnumerable<Category> Categories { get; set; }
    public IEnumerable<Tag> Tags { get; set; }
    public int Stars { get; set; }
    public PublishStatus PublishStatus { get; set; }
    public SubmissionStatus SubmissionStatus { get; set; }

    public void Approve()
    {
        SubmissionStatus = SubmissionStatus.Approved;
    }

    public void Reject()
    {
        SubmissionStatus = SubmissionStatus.Rejected;
    }
}

