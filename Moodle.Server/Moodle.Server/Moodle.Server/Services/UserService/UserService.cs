
namespace Moodle.Server.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public UserService(IMapper mapper, DataContext context, ILogger<UserService> logger)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<List<GetCourseDto>>> AddCourseToUser(AddCourseToUserDto requestObject)
        {
            var user = await _context.Users.Include(x => x.Degree).Include(x => x.Courses).FirstOrDefaultAsync(x => x.Id == requestObject.UserId);
            var course = await _context.Courses.Include(x => x.Degrees).FirstOrDefaultAsync(x => x.Id == requestObject.CourseId);
            if (!course.Degrees.Contains(user.Degree)) // Requested course is not allowed for the user's degree
            {                
                return new ServiceResponse<List<GetCourseDto>>
                {
                    Success = false,
                    Message = ResponseMessages.SubscriptionToCourseRejected,
                    Data = null
                };
            }
            if(user.Courses == null)
            {
                user.Courses = new List<Course>();
            }
            if (user.Courses.Any(c => c.Id == course.Id)) //The user is already subscribed to the course
            {
                return new ServiceResponse<List<GetCourseDto>>
                {
                    Success = false,
                    Message = ResponseMessages.AlreadySubcribed,
                    Data = null
                };
            }
            else { // The user is allowed to subscribe to the course and is not subscribed yet
                user.Courses.Add(course);
                await _context.SaveChangesAsync();
                return new ServiceResponse<List<GetCourseDto>>
                {
                    Success = true,
                    Message = ResponseMessages.CourseAddedToUser,
                    Data = GetCoursesByUser(user.Id).Result.Data
                };
            }
            
        }
        public async Task<ServiceResponse<List<GetCourseDto>>> GetCoursesByUser(int id)
        {
            var response = new ServiceResponse<List<GetCourseDto>>();
            var dbCourses = await _context.Users
                .Where(u => u.Id == id)
                .SelectMany(u => u.Courses)
                .ToListAsync();
            response.Data = dbCourses.Select(c => _mapper.Map<GetCourseDto>(c)).ToList();
            if (response.Data.Count == 0)
            {
                response.Message = ResponseMessages.NoCourseFoundForUser;
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetUsersByCourseId(int id)
        {
            var response = new ServiceResponse<List<GetUserDto>>();

            try
            {
                var course = await _context.Courses
                    .Include(c => c.Users).ThenInclude(u => u.Degree)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (course == null)
                {
                    response.Message = "Course not found.";
                    response.Success = false;
                    return response;
                }

                var users = course.Users.Select(u => new GetUserDto
                {
                    Name = u.Name,
                    Degree = u.Degree != null ? new DegreeDto { Name = u.Degree.Name } : null

                }).ToList();

                response.Data = users;
                response.Message = "Users successfully retrieved by Course Id.";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }
    }
}
