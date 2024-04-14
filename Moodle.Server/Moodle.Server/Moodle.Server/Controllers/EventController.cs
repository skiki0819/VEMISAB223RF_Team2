using Microsoft.AspNetCore.Mvc;

namespace Moodle.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        [Route("GetEvents")]
        public async Task<IActionResult> GetEvents()
        {
            var result = await _eventService.GetEvents();
            if (result.Data is null)
                return NotFound(result.Message);

            return Ok(result);
        }

        [HttpPost]
        [Route("CreateEvent")]
        public async Task<IActionResult> CreateEvent(CreateEventDto eventInfo)
        {
            var result = await _eventService.CreateEvent(eventInfo);
            return Ok(result);
        }
    }
}
