using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mullayon.Api.Dtos;
using Mullayon.Core;
using Mullayon.Core.Constants;
using Mullayon.Core.Entities;

namespace Mullayon.Api.Controllers;

public class CategoryController : BaseController
{
    public CategoryController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryListDto>>> Get()
    {
        var categories = await UnitOfWork.Category.GetAllAsync();
        var categoriesDto = Mapper.Map<IEnumerable<CategoryListDto>>(categories);
        return Ok(categoriesDto);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoryDetailsDto>> Get(Guid id)
    {
        var category = await UnitOfWork.Category.GetByIdAsync(id);

        if (category == null)
            return NotFound();

        var categoryDto = Mapper.Map<CategoryDetailsDto>(category);
        return Ok(categoryDto);
    }

    [HttpPost]
    [Authorize(Roles = RoleStrings.Admin)]
    public async Task<ActionResult<CategoryDetailsDto>> Post([FromBody] CreateCategoryDto categoryDto)
    {
        var category = Mapper.Map<Category>(categoryDto);
        await UnitOfWork.Category.AddAsync(category);
        await UnitOfWork.CommitAsync();
        return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = RoleStrings.Admin)]

    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateCategoryDto categoryDto)
    {
        if (id != categoryDto.Id)
            return BadRequest();

        var categoryToUpdate = await UnitOfWork.Category.GetByIdAsync(id);

        if (categoryToUpdate == null)
            return NotFound();

        Mapper.Map(categoryDto, categoryToUpdate);
        await UnitOfWork.CommitAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = RoleStrings.Admin)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var category = await UnitOfWork.Category.GetByIdAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        await UnitOfWork.Category.DeleteAsync(category);

        await UnitOfWork.CommitAsync();

        return NoContent();
    }
}