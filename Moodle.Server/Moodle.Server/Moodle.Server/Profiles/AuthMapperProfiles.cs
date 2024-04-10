namespace Moodle.Server.Profiles
{
    public class AuthMapperProfiles : Profile
    {
        public AuthMapperProfiles()
        {
            CreateMap<User, LoginResponseDto>();
        }

    }
}
