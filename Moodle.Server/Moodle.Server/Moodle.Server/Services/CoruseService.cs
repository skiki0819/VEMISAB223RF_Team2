using Moodle.Server.Models;
using Moodle.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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
            var courses = await _context.Courses
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalItems = await _context.Courses.CountAsync();
            var result = new PagedResult<Course>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling((double)totalItems / pageSize),
                Items = courses
            };

            return result;
        }
    }
}