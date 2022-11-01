using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mullayon.Core;
using Mullayon.Core.Entities;

namespace Mullayon.Api.Controllers;

public class PostController : BaseController
{
    private readonly UserManager<ApplicationUser> _userManager;

    public PostController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : base(unitOfWork)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var posts = await UnitOfWork.Post.GetAllAsync();
        return Ok(posts);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var post = await UnitOfWork.Post.GetByIdAsync(id);
        return Ok(post);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post([FromBody] CreatePostDto post)
    {
        Console.WriteLine(User.Identity.Name);
        var author= await _userManager.GetUserAsync(User);
        if (author == null) return Unauthorized();
        var newPost = new Post
        {
            Title = post.Title,
            Content = post.Content,
            Author = author,
        };
        await UnitOfWork.Post.AddAsync(newPost);
        await UnitOfWork.CommitAsync();
        return CreatedAtAction(nameof(GetById), new { id = newPost.Id }, post);

    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] Post post)
    {
        var postToUpdate = await UnitOfWork.Post.GetByIdAsync(id);
        if (postToUpdate == null)
        {
            return NotFound();
        }

        postToUpdate.Title = post.Title;
        postToUpdate.Content = post.Content;

        await UnitOfWork.Post.UpdateAsync(postToUpdate);
        await UnitOfWork.CommitAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var postToDelete = await UnitOfWork.Post.GetByIdAsync(id);

        if (postToDelete == null)
        {
            return NotFound();
        }

        await UnitOfWork.Post.DeleteAsync(postToDelete);
        await UnitOfWork.CommitAsync();
        return NoContent();
    }

    [HttpGet("author/{id:guid}")]
    public async Task<IActionResult> GetByAuthor(Guid id)
    {
        var posts = await UnitOfWork.Post.GetPostsByAuthorAsync(id);
        return Ok(posts);
    }
}

public class CreatePostDto
{
    public string Title { get; set; }
    public string Content { get; set; }
}