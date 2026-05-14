using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Letstudy.Models;

[Table(("access_keys"))]
public class AccessKey
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsActivated { get; private set; }
    [Required]
    public required string Value { get; set; }
    public required Guid BoardId { get; set; }
    [JsonIgnore]
    public Board? Board { get; set; }
    public Guid? StudentId { get; set; }
    public Student? Student { get; set; }

    public void Activate() => IsActivated = true;
}