namespace Moodle.Server.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetCourseDto>>> AddCourseToUser(AddCourseToUserDto request);
        Task<ServiceResponse<List<GetCourseDto>>> GetCoursesByUser(int id);
        Task<ServiceResponse<List<GetUserDto>>> GetUsersByCourseId(int id);

    }
}
