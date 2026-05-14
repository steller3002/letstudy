using System.ComponentModel.DataAnnotations.Schema;

namespace Letstudy.Models;

[Table("tutors")]
public class Tutor : User
{
    public List<Student> Students { get; set; } = [];
}