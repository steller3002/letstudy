using System.Security.Claims;
using Letstudy.Identity;
using Letstudy.Models;
using Letstudy.Requests;
using Letstudy.Responses;
using Letstudy.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using BlockType = Letstudy.Requests.BlockType;

namespace Letstudy.Controllers;

[ApiController]
[Route("api")]
[Authorize(Roles = $"{Role.Tutor}, {Role.Student}")]
public class BlockController(BlockService blockService, UserService userService, ModuleService moduleService) : ControllerBase
{
    // Свдиг блока на доске
    // PATCH api/boards/boardId/block
    [HttpPatch("boards/{boardId:guid}/{moduleId:guid}/block")]
    [Authorize(Roles = Role.Tutor)]
    public async Task<IActionResult> MoveBlockAsync([FromBody] MoveBlockRequest request, Guid boardId, Guid moduleId)
    {
        var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (nameIdentifier == null || !Guid.TryParse(nameIdentifier, out var userId))
        {
            return NotFound();
        }

        var isUserHasBoard = await userService.IsUserHasBoard(userId, boardId);
        if (!isUserHasBoard)
        {
            return NotFound();
        }
        
        var moduleBelongsToBoard = await moduleService.ModuleBelongsToBoard(moduleId, boardId);
        if (!moduleBelongsToBoard) return NotFound();

        if (request.NewOrder < 0) return BadRequest();
        
        await blockService.MoveBlockAsync(moduleId, request.BlockId, request.NewOrder);
        return NoContent();
    }
    
    // Получение всех блоков модуля
    // GET api/boards/{boardId}/{moduleId}
    [HttpGet("boards/{boardId:guid}/{moduleId:guid}")]
    [Authorize(Roles = $"{Role.Tutor}, {Role.Student}")]
    public async Task<IActionResult> GetModuleBlocksAsync(Guid boardId, Guid moduleId)
    {
        var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (nameIdentifier == null || !Guid.TryParse(nameIdentifier, out var userId))
        {
            return NotFound();
        }
        
        var isUserHasBoard = await userService.IsUserHasBoard(userId, boardId);
        if (!isUserHasBoard)
        {
            return NotFound();
        }
        
        var moduleBelongsToBoard = await moduleService.ModuleBelongsToBoard(moduleId, boardId);
        if (!moduleBelongsToBoard) return NotFound();
        
        var blocks = await blockService.GetModuleBlocksAsync(moduleId);
        
        var blocksResponse = blocks
            .Select(block => new GetModuleBlocksResponseItem(block.Id, block.Order))
            .ToList();
        
        return Ok(new GetModuleBlocksResponse(blocksResponse));
    }
}