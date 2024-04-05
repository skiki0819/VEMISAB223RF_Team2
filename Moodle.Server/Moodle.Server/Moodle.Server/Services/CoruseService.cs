using Moodle.Server.Models;
using Moodle.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Moodle.Server.Services
{
    public interface ICourseService
    {
    }

    public class CourseService : ICourseService
    {
        private readonly DataContext _context;

        public CourseService(DataContext context)
        {
            _context = context;
        }


    }
}