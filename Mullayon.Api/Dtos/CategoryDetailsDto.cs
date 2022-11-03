using Mullayon.Core.Entities;

namespace Mullayon.Api.Dtos;

public class CategoryDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Image Image { get; set; }
    public string Description { get; set; }
    public IEnumerable<PostDetailsDto> Posts { get; set; }
}