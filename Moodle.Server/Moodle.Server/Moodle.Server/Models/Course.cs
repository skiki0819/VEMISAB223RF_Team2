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
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }
        public List<User> Users { get; set; }
        public List<Event> Events { get; set; }
        public List<Degree> Degrees { get; set; }
    }
}