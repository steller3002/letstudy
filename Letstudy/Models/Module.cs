using System.ComponentModel.DataAnnotations;

namespace Letstudy.Models;

public abstract class Module
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();
    [Required]
    public required string Title { get; set; }
    [Required]
    public required int Order {get; set;}
    public required Guid BoardId { get; set; }
    public Board? Board { get; set; }
    
    [Required]
    public abstract required ModuleType ModuleType { get; init; }
}

public enum ModuleType { Lecture, Practice }