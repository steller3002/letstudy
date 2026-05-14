using Letstudy.Core;
using Letstudy.Models;
using Microsoft.EntityFrameworkCore;

namespace Letstudy.Services;

public class StudentService(AppDbContext context, BoardService boardService)
{
    public async Task<Student?> GetStudentWithBoardsAsync(Guid id) => await context.Students
        .Include(s => s.Boards)
        .FirstOrDefaultAsync(s => s.Id == id);

    public async Task SubscribeToBoardAsync(Guid studentId, AccessKey accessKey)
    {
        var updated = await context.AccessKeys
            .Where(k => k.Id == accessKey.Id && !k.IsActivated)
            .ExecuteUpdateAsync(s => s
                .SetProperty(k => k.IsActivated, true)
            );

        if (updated == 0) return;
        
        var board = await boardService.GetBoardByIdAsync(accessKey.BoardId);
        if (board == null) return;
        
        var student = await context.Students.FirstOrDefaultAsync(s => s.Id == studentId);
        if (student == null) return;
        
        student.Boards.Add(board);
        await context.SaveChangesAsync();
    }
}