using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mullayon.Api.Dtos;
using Mullayon.Core;
using Mullayon.Core.Constants;

namespace Mullayon.Api.Controllers;

[Authorize(Roles = RoleStrings.Admin)]
public class AdminController : BaseController
{
    public AdminController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    [HttpGet("post/pending")]
    public async Task<ActionResult<IEnumerable<PostListDto>>> GetPendingPosts()
    {
        var posts = await UnitOfWork.Post.GetPendingPostsAsync();
        return Ok(Mapper.Map<IEnumerable<PostListDto>>(posts));
    }
    
    [HttpGet("post/rejected")]
    public async Task<ActionResult<IEnumerable<PostListDto>>> GetRejectedPosts()
    {
        var posts = await UnitOfWork.Post.GetRejectedPostsAsync();
        return Ok(Mapper.Map<IEnumerable<PostListDto>>(posts));
    }
    

    [HttpGet("post/{id}")]
    public async Task<ActionResult<PostDetailsDto>> GetPostDetails(Guid id)
    {
        var post = await UnitOfWork.Post.GetByIdAsync(id);
        return Ok(Mapper.Map<PostDetailsDto>(post));
    }

    [HttpPut("post/pending/{id}/approve")]
    public async Task<ActionResult> ApprovePost(Guid id)
    {
        var post = await UnitOfWork.Post.GetByIdAsync(id);

        if (post == null)
            return NotFound();

        post.Approve();
        await UnitOfWork.CommitAsync();
        return NoContent();
    }
    
    [HttpPut("post/pending/{id}/reject")]
    public async Task<ActionResult> RejectPost(Guid id)
    {
        var post = await UnitOfWork.Post.GetByIdAsync(id);

        if (post == null)
            return NotFound();

        post.Reject();
        await UnitOfWork.CommitAsync();
        return NoContent();
    }
}