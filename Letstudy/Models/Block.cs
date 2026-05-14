using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Letstudy.Models;

[Table("blocks")]
public class Block
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public required int Order { get; set; }
    public required BlockContent Content { get; set; }
    public required Guid ModuleId { get; set; }
    public Module? Module { get; set; }
}

public class BlockContent
{
    public string? Text { get; set; }
    public string? ImageUrl { get; set; }
    
}