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
[Route("api/boards")]
public class BoardController(
    TutorService tutorService,
    BoardService boardService,
    UserService userService,
    IAuthorizationService authorizationService) : ControllerBase
{
    // TODO: BaseController с protected свойством UserId или ActionFilter
    // TODO: Extensions метод для BoardService с получением доски и проверкой прав доступа (по желанию) 
    // TODO: Внедрить ProblemDetails
    
    // Создание новой доски для репетитора
    // POST /api/boards
    [HttpPost("")]
    [Authorize(Roles = Role.Tutor)]
    public async Task<IActionResult> CreateBoardAsync([FromBody] CreateBoardRequest request)
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized();

        var tutor = await tutorService.GetByIdAsync(userId.Value);
        if (tutor == null) return NotFound();
        
        var newBoard = new Board { Title = request.Title, TutorId = userId.Value, Users = [tutor] };
        await boardService.CreateBoardAsync(newBoard);
        
        return CreatedAtRoute(nameof(GetBoardAsync),
            new { boardId = newBoard.Id },
            new CreateBoardResponse(newBoard.Id));
    }
    
    // Получение всех досок репетитора
    // GET api/boards
    [HttpGet("")]
    [Authorize(Roles = Role.Tutor)]
    public async Task<IActionResult> GetBoardsAsync()
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized();

        var boards = await boardService.GetTutorBoardsAsync(userId.Value);
        
        var boardItems = boards
            .Select(b => new GetBoardsResponseItem(b.Title))
            .ToList();
        return Ok(new GetBoardsResponse(boardItems));
    }
    
    // Получение конкретной доски по Id
    // GET api/boards{boardId}
    [HttpGet("{boardId:guid}")]
    [Authorize(Roles = $"{Role.Tutor}, {Role.Student}")]
    public async Task<IActionResult> GetBoardAsync(Guid boardId)
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized();
        
        var board = await boardService.GetBoardByIdAsync(boardId);
        if (board == null) return NotFound();

        var authResult = await authorizationService.AuthorizeAsync(User, board, new BoardAccessRequirement());
        if (!authResult.Succeeded) return Forbid();

        var moduleResponses = board.Modules
            .Select(m => new GetBoardResponseModuleItem(m.Title))
            .ToList();

        return Ok(new GetBoardResponse(board.Title, moduleResponses));
    }

    // Генерация ключа доступа для доски
    // POST api/boards/{boardId}/access-key")
    [HttpPost("{boardId:guid}/access-key")]
    [Authorize(Roles = Role.Tutor)]
    public async Task<IActionResult> GenerateAccessKeyAsync(Guid boardId)
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized();
        
        var board = await boardService.GetBoardByIdAsync(boardId);
        if (board == null) return NotFound();
        
        var authResult = await authorizationService.AuthorizeAsync(User, board, new BoardAccessRequirement());
        if (!authResult.Succeeded) return Forbid();

        var accessKey = await boardService.GenerateKeyAsync(boardId);
        return Ok(new GetAccessKeyResponse(accessKey.Value));
    }
}