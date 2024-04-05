using System.ComponentModel.DataAnnotations.Schema;

namespace Moodle.Server.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int DegreeId { get; set; }
        [ForeignKey("DegreeId")]
        public Degree Degree { get; set; }
        public List<Course> Courses { get; set; }
    }

}