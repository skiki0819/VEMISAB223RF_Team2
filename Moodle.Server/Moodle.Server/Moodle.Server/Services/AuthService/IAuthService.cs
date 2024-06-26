﻿namespace Moodle.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<LoginResponseDto>> Login(LoginRequestDto request);
        Task<ServiceResponse<RegisterResponseDto>> Register(RegisterRequestDto request);
    }
}
