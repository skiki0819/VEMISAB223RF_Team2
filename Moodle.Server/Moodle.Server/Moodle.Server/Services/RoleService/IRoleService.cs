namespace Moodle.Server.Services.RoleService
{
    public interface IRoleService
    {
        Task<ServiceResponse<List<GetRoleDto>>> GetRoles();
    }
}
