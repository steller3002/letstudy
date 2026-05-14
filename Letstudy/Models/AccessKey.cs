using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Letstudy.Models;

[Table(("access_keys"))]
public class AccessKey
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();
    public bool IsActivated { get; private set; }
    [Required]
    [MaxLength(150)]
    public required string Value { get; init; }
    public required Guid BoardId { get; init; }
    [JsonIgnore]
    public Board? Board { get; set; }
    public Guid? StudentId { get; init; }
    public Student? Student { get; set; }

    public void Activate() => IsActivated = true;
}