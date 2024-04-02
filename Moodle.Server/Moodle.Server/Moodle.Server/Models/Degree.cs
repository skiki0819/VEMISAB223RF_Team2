using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moodle.Server.Models
{
    public class Degree
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    public ICollection<User> Users { get; set; }

    public ICollection<Course> Courses { get; set; }

    public ICollection<ApprovedDegree> ApprovedDegrees { get; set; }
}
}