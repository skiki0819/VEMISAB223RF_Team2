using Microsoft.AspNetCore.Mvc;

namespace Moodle.Server.Services.CourseService
{
    public interface ICourseService
    {
        Task<ServiceResponse<List<GetCourseDto>>> GetAllCourse();
        Task<ServiceResponse<List<GetCourseDto>>> GetSingleCourse(int id);
        Task<ServiceResponse<List<GetCourseDto>>> AddCourse([FromBody] AddCourseDto course);
        Task<ServiceResponse<List<GetCourseDto>>> GetCoursesByDegree(int id);


    }
}
