using Mullayon.Core.Entities;

namespace Mullayon.Api.Dtos;

public class CreateCategoryDto
{
    public string Name { get; set; }
    public Image Image { get; set; }
    public string Description { get; set; }
}