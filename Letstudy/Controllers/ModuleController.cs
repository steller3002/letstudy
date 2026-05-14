using System.Security.Claims;
using Letstudy.Extensions;
using Letstudy.Identity;
using Letstudy.Models;
using Letstudy.Requests;
using Letstudy.Responses;
using Letstudy.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Letstudy.Controllers;

[ApiController]
[Route("api")]
[Authorize(Roles = "Tutor, Student")]
public class ModuleController(
    BoardService boardService,
    ModuleService moduleService,
    UserService userService,
    IAuthorizationService authorizationService) : ControllerBase
{
    // Добавление на доску модуля
    // POST api/boards/{boardId}/module
    [HttpPost("boards/{boardId:guid}/module")]
    [Authorize(Roles = "Tutor")]
    public async Task<IActionResult> AddModuleAsync([FromBody] AddModuleRequest request, Guid boardId)
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized();
        
        var board = await boardService.GetBoardByIdAsync(boardId);
        if (board == null) return NotFound();

        var authResult = await authorizationService.AuthorizeAsync(User, board, new BoardAccessRequirement());
        if (!authResult.Succeeded) return Forbid();

        var module = new Module { Title = request.Title, BoardId = board.Id };
        
        await moduleService.CreateModuleAsync(module);
        return CreatedAtRoute(nameof(new AddModuleResponse(module.Id));
    }
    
    // Получение всех модулей доски
    // GET api/boards/{boardId}
    [HttpGet("boards/{boardId:guid}")]
    [Authorize(Roles = "Tutor, Student")]
    public async Task<IActionResult> GetBoardModulesAsync(Guid boardId)
    {
        var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (nameIdentifier == null || !Guid.TryParse(nameIdentifier, out var userId))
        {
            return NotFound("identifier not found");
        }

        var isSubscribedToBoard = await userService.IsUserHasBoard(userId, boardId);
        if (!isSubscribedToBoard)
        {
            return NotFound("Not subscribed to board");
        }

        var modules = await moduleService.GetBoardModulesAsync(boardId);
        if (modules == null)
        {
            return NotFound("No modules found");
        }
        
        return Ok(new { modules });
    }
    
    // Получение конкретного модуля доски по ID
    // GET api/boards/{boardId}/{moduleId}
    [HttpGet("boards/{boardId:guid}/module/{moduleId:guid}")]
    [Authorize(Roles = "Tutor, Student")]
    public async Task<IActionResult> GetModuleAsync(Guid boardId, Guid moduleId)
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized();
        
        var board = await boardService.GetBoardByIdAsync(boardId);
        if (board == null) return NotFound();
        
        var authResult = await authorizationService.AuthorizeAsync(User, board, new BoardAccessRequirement());
        if (!authResult.Succeeded) return Forbid();
        
        
    }
}