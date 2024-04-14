using System.ComponentModel.DataAnnotations.Schema;

namespace Moodle.Server.Models.Dtos
{
    public class EventDto
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateEventDto : EventDto
    {
    }
    
    
}
