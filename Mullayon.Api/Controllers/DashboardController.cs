using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mullayon.Api.Dtos;
using Mullayon.Api.Extensions;
using Mullayon.Core;
using Mullayon.Core.Enums;

namespace Mullayon.Api.Controllers;

public class DashboardController : BaseController
{
    public DashboardController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<DashboardDto>> Get()
    {
        var userId = User.GetId();
        var posts = await UnitOfWork.Post.GetPostsByAuthorAsync(userId);

        var acceptedPublications = posts.Where(p =>
            p.SubmissionStatus == SubmissionStatus.Approved && p.PublishStatus == PublishStatus.Published);

        var balance = acceptedPublications.Count() * 10;

        var dashboardDto = new DashboardDto
        {
            PublicationCount = acceptedPublications.Count(),
            Ballance = balance
        };
        return Ok(dashboardDto);
    }
}