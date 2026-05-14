using System.ComponentModel.DataAnnotations.Schema;

namespace Letstudy.Models;

[Table(("lecture_modules"))]
public class LectureModule : Module
{
    public override required ModuleType ModuleType { get; init; } = ModuleType.Lecture;
    public List<Block> Content { get; set; } = [];
}