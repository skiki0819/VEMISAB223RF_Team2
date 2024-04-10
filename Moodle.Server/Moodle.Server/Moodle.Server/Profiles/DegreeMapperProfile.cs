namespace Moodle.Server.Profiles
{
    public class DegreeMapperProfile : Profile
    {
        public DegreeMapperProfile()
        {
            CreateMap<Degree, DegreeDto>();
        }
    }
}
