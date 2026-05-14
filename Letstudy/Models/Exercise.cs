using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Letstudy.Models;

[Table(("exercises"))]
public class Exercise
{
    [Key] public Guid Id { get; init; } = Guid.NewGuid();
    [Required] [MaxLength(32)] public required string Number { get; set; }
    [Required] public required string Topic { get; set; } = "";
    [Required] public required string ContentJson { get; set; } = "{}";
    [Required] public required string Answer { get; set; }
    [Required] public required string SolutionJson { get; set; } = "{}";
    public List<ExerciseReference> Usages { get; set; } = [];
    public DateTime CreatedAd { get; set; } = DateTime.UtcNow;
}