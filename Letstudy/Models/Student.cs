using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Letstudy.Models;

[Table("students")]
public class Student : User
{
    public List<Tutor> Tutors { get; set; } = [];
}