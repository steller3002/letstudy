using Letstudy.Core;
using Letstudy.Models;
using Microsoft.EntityFrameworkCore;

namespace Letstudy.Services;

public class UserService(AppDbContext context)
{
    public async Task<bool> IsUserHasBoard(Guid userId, Guid boardId) => await context.Boards
        .AnyAsync(b => b.Id == boardId && b.Users
            .Any(u => u.Id == userId)
        );

    public async Task CreateAsync(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task<User?> FindUserByEmailAsync(string email) => await context.Users
        .FirstOrDefaultAsync(u => u.Email == email);
}