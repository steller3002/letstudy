using System.Security.Claims;
using Letstudy.Models;
using Letstudy.Services;
using Microsoft.AspNetCore.Authorization;

namespace Letstudy.Identity;

public class BoardAccessHandler(UserService userService) : AuthorizationHandler<BoardAccessRequirement, Board>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        BoardAccessRequirement requirement, Board board)
    {
        var user = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (user == null || !Guid.TryParse(user, out var userId))
        {
            return;
        }
        
        var hasAccess = await userService.IsUserHasBoard(userId, board.Id);
        
        if (hasAccess) context.Succeed(requirement);
    }
}