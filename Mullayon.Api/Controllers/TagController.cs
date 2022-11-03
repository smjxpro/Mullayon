using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mullayon.Api.Dtos;
using Mullayon.Core;
using Mullayon.Core.Constants;
using Mullayon.Core.Entities;

namespace Mullayon.Api.Controllers;

public class TagController : BaseController
{
    public TagController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TagListDto>>> GetTags()
    {
        var tags = await UnitOfWork.Tag.GetAllAsync();
        return Ok(Mapper.Map<IEnumerable<TagListDto>>(tags));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TagDetailsDto>> GetTag(Guid id)
    {
        var tag = await UnitOfWork.Tag.GetByIdAsync(id);
        if (tag == null)
        {
            return NotFound();
        }

        return Ok(Mapper.Map<TagDetailsDto>(tag));
    }

    [HttpPost]
    [Authorize(Roles = RoleStrings.Admin)]
    public async Task<ActionResult<TagDetailsDto>> PostTag(CreateTagDto tagDto)
    {
        var tag = Mapper.Map<Tag>(tagDto);
        await UnitOfWork.Tag.AddAsync(tag);
        await UnitOfWork.CommitAsync();
        return CreatedAtAction("GetTag", new { id = tag.Id }, Mapper.Map<TagDetailsDto>(tag));
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = RoleStrings.Admin)]
    public async Task<IActionResult> PutTag(Guid id, UpdateTagDto tagDto)
    {
        if (id != tagDto.Id)
        {
            return BadRequest();
        }

        var tag = await UnitOfWork.Tag.GetByIdAsync(id);
        if (tag == null)
        {
            return NotFound();
        }

        Mapper.Map(tagDto, tag);
        await UnitOfWork.Tag.UpdateAsync(tag);
        await UnitOfWork.CommitAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = RoleStrings.Admin)]
    public async Task<ActionResult<TagListDto>> DeleteTag(Guid id)
    {
        var tag = await UnitOfWork.Tag.GetByIdAsync(id);
        if (tag == null)
        {
            return NotFound();
        }

        await UnitOfWork.Tag.DeleteAsync(tag);
        await UnitOfWork.CommitAsync();
        return NoContent();
    }
}