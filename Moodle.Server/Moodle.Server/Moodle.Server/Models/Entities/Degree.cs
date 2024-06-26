namespace Moodle.Server.Models.Entities
{
    public class Degree
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public List<Course> Courses { get; set; }
    }
}