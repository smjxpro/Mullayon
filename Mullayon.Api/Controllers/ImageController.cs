using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mullayon.Api.Dtos;
using Mullayon.Core;
using Mullayon.Core.Entities;

namespace Mullayon.Api.Controllers;

public class ImageController : BaseController
{
    private readonly IWebHostEnvironment _environment;

    public ImageController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment) : base(unitOfWork,
        mapper)
    {
        _environment = environment;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Image>> GetImage(Guid id)
    {
        var image = await UnitOfWork.Image.GetByIdAsync(id);
        if (image == null)
        {
            return NotFound();
        }

        return Ok(image);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Image>> UploadImage([FromForm] FileUploadDto dto)
    {
        if (dto.File.Length <= 0) return BadRequest();
        var path = _environment.WebRootPath + "/uploads/" + DateTime.Now.ToString("yyyy-MM-dd");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var fileName = Guid.NewGuid() + Path.GetExtension(dto.File.FileName);
        var uploads = Path.Combine(_environment.WebRootPath, "uploads/" + DateTime.Now.ToString("yyyy-MM-dd"),
            fileName);

        await using var fileStream = new FileStream(uploads, FileMode.Create);
        await dto.File.CopyToAsync(fileStream);
        var image = new Image
        {
            Id = Guid.NewGuid(),
            Url = Path.Combine("/uploads/" + DateTime.Now.ToString("yyyy-MM-dd"), fileName)
        };
        await UnitOfWork.Image.AddAsync(image);
        await UnitOfWork.CommitAsync();
        return CreatedAtAction(nameof(GetImage), new { id = image.Id }, image);
    }
}