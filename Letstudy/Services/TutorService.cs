using Letstudy.Core;
using Letstudy.Models;
using Letstudy.Utils;

namespace Letstudy.Services;

public class TutorService(AppDbContext context)
{
    
    public async Task AddAsync(string name, string surname, string email, string password)
    {
        var hashedPassword = PasswordHasher.Hash(password);
        var tutor = new Tutor
        {
            Name = name,
            Surname = surname,
            Email = email,
            PasswordHash = hashedPassword
        };
        await context.Tutors.AddAsync(tutor);
    }

    public async Task<Tutor?> GetByIdAsync(Guid id) => await context.Tutors.FindAsync(id);
}