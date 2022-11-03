using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mullayon.Api.Dtos;
using Mullayon.Api.Extensions;
using Mullayon.Core;
using Mullayon.Core.Entities;
using Mullayon.Core.Enums;

namespace Mullayon.Api.Controllers;

public class PostController : BaseController
{
    public PostController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostListDto>>> Get()
    {
        var posts = await UnitOfWork.Post.GetAllAsync();
        return Ok(Mapper.Map<IEnumerable<PostListDto>>(posts));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PostDetailsDto>> Get(Guid id)
    {
        var post = await UnitOfWork.Post.GetByIdAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        return Ok(Mapper.Map<PostDetailsDto>(post));
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<PostDetailsDto>> Post([FromBody] CreatePostDto postDto)
    {
        var categories = new List<Category>();
        
        foreach (var cat in postDto.Categories)
        {
            var category = await UnitOfWork.Category.GetByIdAsync(cat.Id);
            if (category != null) categories.Add(category);
        }
        
        var tags = new List<Tag>();
        
        foreach (var tag in postDto.Tags)
        {
            var tagEntity = await UnitOfWork.Tag.GetByIdAsync(tag.Id);
            if (tagEntity != null) tags.Add(tagEntity);
        }
        
        var featuredImage = await UnitOfWork.Image.GetByIdAsync(postDto.FeaturedImage.Id);
        
        var images = new List<Image>();
        
        foreach (var image in postDto.Images)
        {
            var imageEntity = await UnitOfWork.Image.GetByIdAsync(image.Id);
            if (imageEntity != null) images.Add(imageEntity);
        }
        
        var post = Mapper.Map<Post>(postDto);
        
        post.Categories = categories;
        post.Tags = tags;
        post.FeaturedImage = featuredImage;
        post.Images = images;
        post.Author = await UnitOfWork.User.GetAsync(User.GetId());

        await UnitOfWork.Post.AddAsync(post);
        await UnitOfWork.CommitAsync();
        return CreatedAtAction(nameof(Get), new { id = post.Id }, Mapper.Map<PostDetailsDto>(post));
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdatePostDto postDto)
    {


        if (id != postDto.Id)
            return BadRequest();

        var postToUpdate = await UnitOfWork.Post.GetByIdAsync(id);
        
        if (postToUpdate == null)
            return NotFound();
        
        var categories = new List<Category>();
        
        foreach (var cat in postDto.Categories)
        {
            var category = await UnitOfWork.Category.GetByIdAsync(cat.Id);
            if (category != null) categories.Add(category);
        }
        
        var tags = new List<Tag>();
        
        foreach (var tag in postDto.Tags)
        {
            var tagEntity = await UnitOfWork.Tag.GetByIdAsync(tag.Id);
            if (tagEntity != null) tags.Add(tagEntity);
        }
        
        var featuredImage = await UnitOfWork.Image.GetByIdAsync(postDto.FeaturedImage.Id);
        
        var images = new List<Image>();
        
        foreach (var image in postDto.Images)
        {
            var imageEntity = await UnitOfWork.Image.GetByIdAsync(image.Id);
            if (imageEntity != null) images.Add(imageEntity);
        }
        
        Mapper.Map(postDto, postToUpdate);
        postToUpdate.Categories = categories;
        postToUpdate.Tags = tags;
        postToUpdate.FeaturedImage = featuredImage;
        postToUpdate.Images = images;
        
        await UnitOfWork.Post.UpdateAsync(postToUpdate);
        
        await UnitOfWork.CommitAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        var post = await UnitOfWork.Post.GetByIdAsync(id);
        if (post == null)
            return NotFound();

        await UnitOfWork.Post.DeleteAsync(post);
        await UnitOfWork.CommitAsync();
        return NoContent();
    }
}