using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Letstudy.Models;

[Table("exercise_references")]
public class ExerciseReference
{
    [Key] public Guid Id { get; init; } = Guid.NewGuid();
    [Required] public required int Order { get; init; }
    
    [Required] public required Guid PraticeModuleId { get; init; }
    public PracticeModule? PracticeModule { get; set; }
    
    [Required] public required Guid ExerciseId { get; init; }
    public Exercise? Exercise { get; set; }

    public bool IsSolved { get; set; } = false;
}