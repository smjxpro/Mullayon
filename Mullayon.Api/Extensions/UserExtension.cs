using System.Security.Claims;

namespace Mullayon.Api.Extensions;

public static class UserExtension
{
    public static Guid GetId(this ClaimsPrincipal user)
    {
        return Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}