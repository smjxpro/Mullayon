using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mullayon.Core;

namespace Mullayon.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    protected readonly IMapper Mapper;
    protected IUnitOfWork UnitOfWork { get; }

    public BaseController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;
    }
}