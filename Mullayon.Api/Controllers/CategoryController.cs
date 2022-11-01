using Microsoft.AspNetCore.Mvc;
using Mullayon.Core;
using Mullayon.Core.Entities;

namespace Mullayon.Api.Controllers;

public class CategoryController : BaseController
{
    public CategoryController(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> Get()
    {
        var result = await UnitOfWork.Category.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Category>> Get(Guid id)
    {
        var result = await UnitOfWork.Category.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}