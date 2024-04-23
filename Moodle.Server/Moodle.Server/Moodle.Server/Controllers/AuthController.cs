using Microsoft.AspNetCore.Mvc;

namespace Moodle.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto login)
        {
            var result = await _authService.Login(login);
            if (result.Data is null)
                return NotFound(result.Message);

            return Ok(result);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<RegisterResponseDto>> Register(RegisterRequestDto register)
        {
            var result = await _authService.Register(register);
            if (result.Success is false)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
    
}
