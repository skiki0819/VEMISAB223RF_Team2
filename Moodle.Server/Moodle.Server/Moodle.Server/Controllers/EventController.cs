using Microsoft.AspNetCore.Mvc;

namespace Moodle.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventservice;

        public EventController(IEventService eventservice)
        {
            _eventservice = eventservice;
        }
    }
}
