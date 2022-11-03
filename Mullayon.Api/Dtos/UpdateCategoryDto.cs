using Mullayon.Core.Entities;

namespace Mullayon.Api.Dtos;

public class UpdateCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Image Image { get; set; }
    public string Description { get; set; }
}