namespace Moodle.Server.Profiles
{
    public class EventMapperProfile : Profile
    {
        public EventMapperProfile()
        {
            CreateMap<CreateEventDto, Event>();
            CreateMap<Event, GetEventDto>();
        }
    }
}
