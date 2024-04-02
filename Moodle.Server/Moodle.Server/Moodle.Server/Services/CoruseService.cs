using Moodle.Server.Models;
using Moodle.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Moodle.Server.Services
{
    public interface ICourseService
    {
        Task<PagedResult<Course>> GetCourses(int pageNumber, int pageSize);
    }

    public class CourseService : ICourseService
    {
        private readonly CoursesDbContext _context;

        public CourseService(CoursesDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Course>> GetCourses(int pageNumber, int pageSize)
        {
            var courses = await _context.Courses.Include(x => x.Code).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var result = courses.Create(await _context.Courses.CountAsync(), pageNumber, pageSize);
            return result;
        }

    }
}
