using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using static System.String;

namespace Letstudy.Models;

[Table("tutors")]
public class Tutor : User
{
    public List<Student> Students { get; set; } = [];
}