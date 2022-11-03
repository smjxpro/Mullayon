using Mullayon.Core.Entities;

namespace Mullayon.Api.Dtos;

public class CategoryListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Image Image { get; set; }
    public string Description { get; set; }
}