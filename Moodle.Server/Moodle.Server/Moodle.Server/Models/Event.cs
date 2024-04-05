using System.ComponentModel.DataAnnotations.Schema;

namespace Moodle.Server.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}