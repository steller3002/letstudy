using Letstudy.Core;
using Letstudy.Models;
using Microsoft.EntityFrameworkCore;

namespace Letstudy.Services;

public class ModuleService(AppDbContext context)
{
    public async Task CreateModuleAsync(Module module)
    {
        await context.Modules.AddAsync(module);
        await context.SaveChangesAsync();
    }

    public async Task<bool> ModuleBelongsToBoard(Guid modelId, Guid boardId) => await context.Modules
        .AnyAsync(m => m.Id == modelId && m.BoardId == boardId);

    public async Task<List<Module>?> GetBoardModulesAsync(Guid boardId)
    {
        var board = await context.Boards
            .Include(b => b.Modules)
            .FirstOrDefaultAsync(b => b.Id == boardId);

        return board?.Modules;
    }
}