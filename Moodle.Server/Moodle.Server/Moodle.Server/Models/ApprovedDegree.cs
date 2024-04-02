using System;
using System.Collections.Generic;
using  System.ComponentModel.DataAnnotations;
using  System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Moodle.Server.Models
{
    public class ApprovedDegree
{
    [Key]
    public int Id { get; set; }

    public int CourseId { get; set; }

    [ForeignKey("CourseId")]
    public Course Course { get; set; }

    public int DegreeId { get; set; }

    [ForeignKey("DegreeId")]
    public Degree Degree { get; set; }
}
}