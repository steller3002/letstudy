using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static System.String;

namespace Letstudy.Models;

[Table("boards")]
public class Board
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();
    [Required]
    public required string Title { get; set; }
    public List<Module> Modules { get; set; } = [];
    public List<AccessKey> AccessKeys { get; set; } = [];
    public List<User> Users { get; set; } = [];
    public required Guid TutorId { get; set; }
    [JsonIgnore]
    public Tutor? Tutor { get; set; }
}