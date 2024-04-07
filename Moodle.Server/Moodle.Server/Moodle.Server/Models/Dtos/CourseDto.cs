namespace Moodle.Server.Models.Dtos
{
    public class CourseDto
    {
    }

    public class GetCourseDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }

    }

    public class AddCourseDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }
    }
}
