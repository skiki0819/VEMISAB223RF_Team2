using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Moodle.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;


        public AuthService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<ServiceResponse<LoginResponseDto>> Login(LoginRequestDto request)
        {
            var response = new ServiceResponse<LoginResponseDto>();
            var user = await _context.Users.Include(u => u.Role).Include(u => u.Degree)
                .FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null)
            {
                response.Success = false;
                response.Message = ResponseMessages.LoginInformationError;
                response.Data = null;
                return response;
            }
            else if (VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = true;
                response.Message = ResponseMessages.LoginSuccess;
                response.Data = new LoginResponseDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Username = user.Username,
                    Token = CreateToken(user)
                };
                return response;
            }
            else { 
                response.Success = false;
                response.Message = ResponseMessages.LoginInformationError;
                response.Data = null;
                return response;
            }
        }

        public async Task<ServiceResponse<RegisterResponseDto>> Register(RegisterRequestDto userInfo)
        {
            var response = new ServiceResponse<RegisterResponseDto>();
            if (await UserExists(userInfo.Username))
            {
                response.Success = false;
                response.Message = ResponseMessages.UserAlreadyExists;
                response.Data = null;
            }
            CreatePasswordHash(userInfo.Password, out byte[] passwordHash, out byte[] passwordSalt);
            User user = new User
            {
                Username = userInfo.Username,
                Name = userInfo.Name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                DegreeId = userInfo.DegreeId,
                RoleId = userInfo.RoleId
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            response.Success = true;
            response.Message = ResponseMessages.UserRegistered;
            response.Data = null;
            return response;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computehash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computehash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims =
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.Name)
            ];
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value ?? throw new Exception("Signing key is missing")));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username.Equals(username));
        }
    }
}
