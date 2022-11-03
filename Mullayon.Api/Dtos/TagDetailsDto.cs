namespace Mullayon.Api.Dtos;

public class TagDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<PostListDto> Posts { get; set; }
}