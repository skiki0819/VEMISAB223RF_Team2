namespace Moodle.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public AuthService(IMapper mapper, DataContext context, ILogger<AuthService> logger)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<LoginResponseDto>> Login(LoginRequestDto request)
        {
            var response = new ServiceResponse<LoginResponseDto>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username && u.Password == request.Password);
            if (user == null)
            {
                response.Success = false;
                response.Message = ResponseMessages.LoginInformationError;
                response.Data = null;
                return response;
            }
            response.Success = true;
            response.Message = ResponseMessages.LoginSuccess;
            response.Data = _mapper.Map<LoginResponseDto>(user);
            return response;


        }
    }
}
