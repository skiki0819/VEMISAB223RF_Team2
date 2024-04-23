
namespace Moodle.Server.Services.EventService
{
    public class EventService : IEventService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EventService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetEventDto>>> GetEvents()
        {
            var response = new ServiceResponse<List<GetEventDto>>();
            try
            {
                var dbEvents = await _context.Event
                    .Include(c => c.Course)
                    .ToListAsync();
                if (dbEvents.Count == 0)
                {
                    response.Success = false;
                    response.Message = ResponseMessages.NoEventFound;
                    return response;
                }
                response.Data = _mapper.Map<List<GetEventDto>>(dbEvents);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<ServiceResponse<List<GetEventDto>>> CreateEvent(CreateEventDto eventInfo)
        {
            var response = new ServiceResponse<List<GetEventDto>>();
            try
            {
                var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == eventInfo.CourseId);
                var newEvent = new Event
                {
                    CourseId = eventInfo.CourseId,
                    Course = course,
                    Name = eventInfo.Name,
                    Description = eventInfo.Description
                };
                await _context.Event.AddAsync(newEvent);
                await _context.SaveChangesAsync();
                //var dbEvents = await _context.Event
                //    .Include(c => c.Course)
                //    .ToListAsync();
                //response.Data = _mapper.Map<List<GetEventDto>>(dbEvents);
                response.Message = ResponseMessages.EventSuccessfullyCreated;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
