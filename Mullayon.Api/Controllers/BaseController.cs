using Microsoft.AspNetCore.Mvc;
using Mullayon.Core;

namespace Mullayon.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    public IUnitOfWork UnitOfWork { get; }

    public BaseController(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }
}