using Letstudy.Models;
using Microsoft.EntityFrameworkCore;

namespace Letstudy.Core;

public class AppDbContext(DbContextOptions<AppDbContext> o) : DbContext(o)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExerciseBlock>().OwnsMany<ContentElement>(b => b.Given);
        modelBuilder.Entity<ExerciseBlock>().OwnsMany<ContentElement>(b => b.Solution);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Tutor> Tutors { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Board> Boards { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<Block> Blocks { get; set; }
    public DbSet<AccessKey> AccessKeys { get; set; }
}