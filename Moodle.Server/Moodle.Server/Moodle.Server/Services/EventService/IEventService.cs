namespace Moodle.Server.Services.EventService
{
    public interface IEventService
    {
        public Task<ServiceResponse<GetEventDto>> CreateEvent(CreateEventDto request);
        public Task<ServiceResponse<List<GetEventDto>>> GetEvents();
        public Task<ServiceResponse<List<GetEventDto>>> GetEventsByCourseId(int id);
    }
}
