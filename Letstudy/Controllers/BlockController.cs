using System.Security.Claims;
using Letstudy.Models;
using Letstudy.Requests;
using Letstudy.Responses;
using Letstudy.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Letstudy.Controllers;

[ApiController]
[Route("api")]
[Authorize(Roles = "Tutor, Student")]
public class BlockController(BlockService blockService, UserService userService, ModuleService moduleService) : ControllerBase
{
    // Создание блока на доске
    // POST api/boards/{boardId}/block
    [HttpPost("boards/{boardId:guid}/{moduleId:guid}/block")]
    [Authorize(Roles = "Tutor")]
    public async Task<IActionResult> AddBlockAsync([FromBody] AddBlockRequest request, Guid boardId, Guid moduleId)
    {
        var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (nameIdentifier == null || !Guid.TryParse(nameIdentifier, out var userId))
        {
            return NotFound();
        }

        var isSubscribedToBoard = await userService.IsUserHasBoard(userId, boardId);
        if (!isSubscribedToBoard)
        {
            return NotFound();
        }
        
        var moduleBelongsToBoard = await moduleService.ModuleBelongsToBoard(moduleId, boardId);
        if (!moduleBelongsToBoard) return NotFound();

        Block block = request.BlockType switch
        {
            BlockType.Text => new TextBlock { Order = request.Order, ModuleId = moduleId},
            BlockType.Image => new ImageBlock { Url = request.Url, Order = request.Order, ModuleId = moduleId },
            BlockType.Exercise => new ExerciseBlock { Order = request.Order, ModuleId = moduleId },
            _ => throw new ArgumentOutOfRangeException(request.BlockType.ToString())
        };
        
        await blockService.CreateBlockAsync(block);
        return Ok(new CreateBlockResponse(block.Id));
    }
    
    // Свдиг блока на доске
    // PATCH api/boards/boardId/block
    [HttpPatch("boards/{boardId:guid}/{moduleId:guid}/block")]
    [Authorize(Roles = "Tutor")]
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
    [Authorize(Roles = "Tutor, Student")]
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