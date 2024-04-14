using Microsoft.AspNetCore.Mvc;

namespace Moodle.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [Route("GetRoles")]
        public async Task<ActionResult<List<GetRoleDto>>> GetRoles()
        {
            var result = await _roleService.GetRoles();
            if (result.Data is null)
                return NotFound(result.Message);

            return Ok(result);
        }
    }
}
