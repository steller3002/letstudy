using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static System.String;

namespace Letstudy.Models;

[Table("modules")]
public class Module
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public required string Title { get; set; }
    public List<Block> Blocks { get; set; } = [];
    public required ModuleType Type { get; set; }
    public required Guid BoardId { get; set; }
    public Board? Board { get; set; }
}

public enum ModuleType { Theory, Practice }