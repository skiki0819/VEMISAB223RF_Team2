namespace Moodle.Server.Models.Dtos
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GetRoleDto : RoleDto
    {
    }
}
