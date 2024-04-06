namespace Moodle.Server.Profiles
{
    public class CourseMapperProfile : Profile
    {
        public CourseMapperProfile()
        {
            CreateMap<Course, GetCourseDto>();
        }

    }
}
