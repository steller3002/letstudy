using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Letstudy.Models;

[Table("users")]
public class User
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();
    [Required, EmailAddress]
    public required string Email { get; set; }
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Surname { get; set; }
    [Required]
    public required string PasswordHash { get; set; }
    public UserRole Role { get; set; }
    public List<Board> Boards { get; set; } = [];
}

public enum UserRole { Tutor, Student }