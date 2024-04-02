using Microsoft.EntityFrameworkCore;
using Moodle.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moodle.Server.Data
{
    public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Degree> Degrees { get; set; }
    public DbSet<Event> Event { get; set; }


}
}