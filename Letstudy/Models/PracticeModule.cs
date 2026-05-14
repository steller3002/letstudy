using System.ComponentModel.DataAnnotations.Schema;

namespace Letstudy.Models;

[Table(("practice_modules"))]
public class PracticeModule : Module
{
    public override required ModuleType ModuleType { get; init; } = ModuleType.Practice;
    public List<Exercise> Tasks { get; set; } = [];
}