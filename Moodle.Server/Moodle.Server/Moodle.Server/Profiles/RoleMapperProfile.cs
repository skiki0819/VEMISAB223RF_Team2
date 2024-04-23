namespace Moodle.Server.Profiles
{
    public class RoleMapperProfile : Profile
    {
        public RoleMapperProfile()
        {
            CreateMap<Role, GetRoleDto>();
        }
    }
}
