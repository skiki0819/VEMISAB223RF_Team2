namespace Moodle.Server.Services.Init
{
    public class RoleInit
    {
        private readonly DataContext _context;
        public RoleInit(DataContext context)
        {
            _context = context;
        }

        public async Task Init()
        {
            if (!_context.Role.Any())
            {
                await _context.Role.AddAsync(new Role { Id=1, Name = Roles.Student });
                await _context.Role.AddAsync(new Role { Id=2, Name = Roles.Teacher });
                await _context.SaveChangesAsync();
            }
        }
    }
}
