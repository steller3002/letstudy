using Letstudy.Core;
using Letstudy.Models;
using Microsoft.EntityFrameworkCore;

namespace Letstudy.Services;
    
public class BlockService(AppDbContext context)
{
    public async Task CreateBlockAsync(Block block)
    {
        await context.Blocks.AddAsync(block);
        await context.SaveChangesAsync();
    }
    
    public async Task<List<Block>> GetAllBlocksAsync() => await context.Blocks.ToListAsync();
    
    public async Task<List<Block>> GetModuleBlocksAsync(Guid moduleId) => await context.Blocks
        .Where(b => b.LectureModuleId == moduleId)
        .OrderBy(b => b.Order)
        .ToListAsync();

    public async Task MoveBlockAsync(Guid moduleId, Guid blockId, int newOrder)
    {
        var block = await context.Blocks.FindAsync(blockId);
        if (block == null) return;
        
        var module = await context.LectureModules
            .Include(m => m.Content)
            .FirstOrDefaultAsync(m => m.Id == moduleId);
        if (module == null) return;
        
        ShiftBlocks(newOrder, block.Order, module.Content);
        block.Order = newOrder;

        await context.SaveChangesAsync();
    }
    
    private static void ShiftBlocks(int newOrder, int currentOrder, List<Block> blocks)
    {
        if (newOrder == currentOrder) return;
        
        if (currentOrder > newOrder)
        {
            foreach (var block in blocks.Where(b => b.Order >= newOrder && b.Order < currentOrder))
                block.Order++;
        }
        else
        {
            foreach (var block in blocks.Where(b => b.Order > currentOrder && b.Order <= newOrder))
                block.Order--;
        }
    }
}