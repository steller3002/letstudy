using System.Security.Claims;
using Letstudy.Core;
using Letstudy.Models;
using Letstudy.Requests;
using Letstudy.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Letstudy.Controllers;

[ApiController]
[Route("api/students")]
[Authorize(Roles = "Student")]
public class StudentController(AccessKeyService keyService, StudentService studentService) : ControllerBase
{
    // Подписка ученика на доску
    // POST api/students/boards
    [HttpPost("boards")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> SubscribeToBoardAsync([FromBody] SubscribeToBoardRequest request)
    {
        var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (nameIdentifier == null || !Guid.TryParse(nameIdentifier, out var id))
        {
            return Unauthorized();
        }
        
        var accessKey = await keyService.GetKeyByValueAsync(request.AccessKey);
        if (accessKey == null)
        {
            return NotFound();
        }

        if (accessKey.IsActivated)
        {
            return BadRequest();
        }
        
        var student = await studentService.GetStudentWithBoardsAsync(id);
        if (student == null)
        {
            return NotFound();
        }

        await studentService.SubscribeToBoardAsync(id, accessKey);
        return NoContent();
    }
    
    // Получение всех досок ученика
    // GET api/students/boards
    [HttpGet("boards")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> GetBoardsAsync()
    {
        var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (nameIdentifier == null || !Guid.TryParse(nameIdentifier, out var id))
        {
            return NotFound();
        }
        
        var student = await studentService.GetStudentWithBoardsAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        
        return Ok(new { boards = student.Boards });
    }
}