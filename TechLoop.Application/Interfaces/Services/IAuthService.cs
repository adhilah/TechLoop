using TechLoop.Application.DTOs;
using TechLoop.Application.DTOs.Auth;

namespace TechLoop.Application.Interfaces.Services;
public interface IAuthService
{
    Task<RegisterResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task<AuthResponse> RefreshTokenAsync(string? refreshToken);
    Task LogoutAsync(string token);
}