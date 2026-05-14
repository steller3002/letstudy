using Letstudy.Core;
using Letstudy.Models;
using Microsoft.EntityFrameworkCore;

namespace Letstudy.Services;

public class AccessKeyService(AppDbContext context)
{
    public async Task<AccessKey?> GetKeyByValueAsync(string value) => await context.AccessKeys
        .Include(k => k.Board)
        .FirstOrDefaultAsync(k => k.Value == value);
}