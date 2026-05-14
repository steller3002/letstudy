using System.Security.Claims;

namespace Letstudy.Extensions;

public static class ClaimsExtensions
{
    public static Guid? GetUserId(this ClaimsPrincipal user)
    {
        var nameIdentifier = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(nameIdentifier, out var userId) ? userId : null;
    }
}