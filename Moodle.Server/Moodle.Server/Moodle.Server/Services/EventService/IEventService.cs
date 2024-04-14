namespace Moodle.Server.Services.EventService
{
    public interface IEventService
    {
        public Task<ServiceResponse<List<GetEventDto>>> CreateEvent(CreateEventDto request);
        public Task<ServiceResponse<List<GetEventDto>>> GetEvents();
    }
}
