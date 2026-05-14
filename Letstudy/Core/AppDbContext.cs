using Letstudy.Models;
using Microsoft.EntityFrameworkCore;

namespace Letstudy.Core;

public class AppDbContext(DbContextOptions<AppDbContext> o) : DbContext(o)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Exercise>().Property(e => e.ContentJson).HasColumnType("jsonb");
        modelBuilder.Entity<Exercise>().Property(e => e.SolutionJson).HasColumnType("jsonb");
        modelBuilder.Entity<Block>().Property(e => e.ContentJson).HasColumnType("jsonb");
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Tutor> Tutors { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Board> Boards { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<Block> Blocks { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<LectureModule> LectureModules { get; set; }
    public DbSet<PracticeModule> PracticeModules { get; set; }
    public DbSet<ExerciseReference> ExerciseReferences { get; set; }
    public DbSet<AccessKey> AccessKeys { get; set; }
}