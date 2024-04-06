namespace Moodle.Server.Models.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public DegreeDto Degree { get; set; }
        public List<GetCourseDto> Courses { get; set; }
    }
    public class GetUserDto
    {
        public string Name { get; set; }
        public DegreeDto Degree { get; set; }
    }
    public class AddCourseToUserDto
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
    }
}
