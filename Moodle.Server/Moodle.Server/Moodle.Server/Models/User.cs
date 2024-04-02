using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moodle.Server.Models
{
     public class User
 {
     [Key]
     public int Id { get; set; }

     [Required]
     [MaxLength(255)]
     public string Username { get; set; }

     [Required]
     [MaxLength(255)]
     public string Name { get; set; }

     [Required]
     [MaxLength(255)]
     public string Password { get; set; }

     public int DegreeId { get; set; }

     [ForeignKey("DegreeId")]
     public Degree Degree { get; set; }

     public ICollection<MyCourse> MyCourses { get; set; }
 }

}