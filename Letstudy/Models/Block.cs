using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Letstudy.Models;

[Table(("blocks"))]
public class Block
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();
    [Required]
    public required int Order { get; set; }
    [Required]
    public required BlockType Type { get; init; }
    [Required]
    public string ContentJson { get; init; } = "{}";
    
    public required Guid LectureModuleId { get; init; }
    public LectureModule?  LectureModule { get; set; }
}

public enum BlockType { Text, Image }