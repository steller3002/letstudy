using Letstudy.Core;
using Letstudy.Models;
using Microsoft.EntityFrameworkCore;

namespace Letstudy.Services;

public class BoardService(AppDbContext context)
{
    public async Task CreateBoardAsync(Board board)
    {
        await context.Boards.AddAsync(board);
        await context.SaveChangesAsync();
    }

    public async Task<List<Board>> GetAllBoardsAsync() => await context.Boards
        .ToListAsync();
    
    public async Task<List<Board>> GetTutorBoardsAsync(Guid tutorId) => await context.Boards
        .Where(b => b.TutorId == tutorId)
        .ToListAsync();

    public async Task<Board?> GetBoardByIdAsync(Guid id) => await context.Boards
        .Include(b => b.Modules)
        .FirstOrDefaultAsync(b => b.Id == id);
    
    public async Task<AccessKey> GenerateKeyAsync(Guid boardId)
    {
        var board = await context.Boards.FirstOrDefaultAsync(b => b.Id == boardId);
        if (board == null)
        {
            throw new Exception("Board not found");
        }

        var accessKey = await GenerateUniqueKeyAsync(boardId);

        await context.AccessKeys.AddAsync(accessKey);
        await context.SaveChangesAsync();
        return accessKey;
    }

    private async Task<AccessKey> GenerateUniqueKeyAsync(Guid boardId)
    {
        string keyValue;
        bool keyIsUnique;
        
        do
        {
            keyValue = Guid.NewGuid().ToString();
            keyIsUnique = !await context.AccessKeys.AnyAsync(k => k.Value == keyValue);
        } while (!keyIsUnique);
        
        var accessKey = new AccessKey
        {
            Value = keyValue,
            BoardId = boardId
        };

        return accessKey;
    }

    public async Task<List<ExerciseBlock>> GetBoardExercisesAsync(Guid boardId)
    {
        var board = await context.Boards
            .Include(b => b.Modules)
            .ThenInclude(module => module.Blocks)
            .FirstOrDefaultAsync(b => b.Id == boardId);
        if (board == null)
        {
            throw new Exception("Board not found");
        }

        throw new NotImplementedException();
    }
}