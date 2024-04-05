using Microsoft.AspNetCore.Mvc;

namespace Moodle.Server.Services
{
    public interface ICourseService
    {
        List<Course> GetAllCourse();
        Course GetSingleCourse(int id);
        List<Course> AddCourse([FromBody] Course course);
        List<Course>? UpdateCourse(int id, Course updateCourse);
        List<Course>? DeleteCourse(int id);

    }

    public class CourseService : ICourseService
    {
        private readonly DataContext _context;
        private static List<Course> courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Code = "VEMILEKVAR",
                    Name = "Lekvár",
                    Credit = 8,
                },
                new Course
                {
                    Id = 2,
                    Code = "VEMIGEM",
                    Name = "GEM",
                    Credit = 3,
                }
            };
        public CourseService(DataContext context)
        {
            _context = context;
        }

        public List<Course> GetAllCourse()
        {
            return courses;
        }

        public Course GetSingleCourse(int id)
        {
            var course = courses.Find(x => x.Id == id);
            return course;
        }

        public List<Course> AddCourse([FromBody] Course course)
        {
            courses.Add(course);
            return courses;
        }

        public List<Course>? UpdateCourse(int id, Course updateCourse)
        {
            var course = courses.Find(x => x.Id == id);
            if (course is null)
                return null;

            course.Code = updateCourse.Code;
            course.Credit = updateCourse.Credit;
            course.Name = updateCourse.Name;

            return courses;
        }

        public List<Course>? DeleteCourse(int id)
        {
            var course = courses.Find(x => x.Id == id);
            if (course is null)
                return null;

            courses.Remove(course);
            return courses;
        }
    }
}