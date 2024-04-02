using Moodle.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Moodle.Server.Data
{
    public class CoursesDbContext : DbContext
    {
        public CoursesDbContext(DbContextOptions<CoursesDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<MyCourse> MyCourses { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ApprovedDegree> ApprovedDegrees { get; set; }
        public DbSet<Degree> Degrees { get; set; }
    }
}