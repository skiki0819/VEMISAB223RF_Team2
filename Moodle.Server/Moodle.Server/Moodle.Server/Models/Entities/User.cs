using System.ComponentModel.DataAnnotations.Schema;

namespace Moodle.Server.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int DegreeId { get; set; }
        [ForeignKey("DegreeId")]
        public Degree Degree { get; set; }
        public List<Course> Courses { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }

}