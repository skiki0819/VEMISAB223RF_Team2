
using Moodle.Server.Models;

namespace Moodle.Server.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public RoleService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetRoleDto>>> GetRoles()
        {
            var response = new ServiceResponse<List<GetRoleDto>>();
            try
            {
                var dbRoles = await _context.Role.ToListAsync();
                response.Data = _mapper.Map<List<GetRoleDto>>(dbRoles);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Data = null;
            }
            return response;
        }
    }
}
