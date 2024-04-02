using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moodle.Server.Models
{
    public class Course
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Code { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    public int Credit { get; set; }

    public ICollection<MyCourse> MyCourses { get; set; }

    public ICollection<Event> Events { get; set; }

    public ICollection<ApprovedDegree> ApprovedDegrees { get; set; }
}
}