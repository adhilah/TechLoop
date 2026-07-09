using TechLoop.Application.DTOs.Auth;
using TechLoop.Application.Interfaces.Authentication;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Application.Interfaces.Services;
using TechLoop.Application.Common.Exceptions;
using TechLoop.Domain.Entities;
using TechLoop.Domain.Enums;

namespace  TechLoop.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository,
        IJwtGenerator jwtGenerator, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtGenerator = jwtGenerator;
        _passwordHasher = passwordHasher;
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
    {
        var existingEmail = await _userRepository.GetByEmailAsync(request.Email);

        if (existingEmail != null)
            throw new BadRequestException("Email already exists.");

        var existingUsername = await _userRepository.GetByUsernameAsync(request.Username);

        if (existingUsername != null)
            throw new BadRequestException("Username already exists.");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            Email = request.Email,
            PasswordHash = _passwordHasher.HashPassword(request.Password),
            RoleId = 1,
            FailedLoginAttempts = 0,
            LockedUntil = null,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user);

        return new RegisterResponse
        {
            Message = "User registered successfully."
        };
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
{
    var user = await _userRepository.GetByEmailAsync(request.Email);

    if (user == null)
        throw new NotFoundException("User not found.");

    if (user.LockedUntil.HasValue &&
        user.LockedUntil > DateTime.UtcNow)
    {
        throw new UnauthorizedException(
            $"Account locked until {user.LockedUntil:yyyy-MM-dd HH:mm:ss} UTC");
    }

    var isValidPassword =
        _passwordHasher.VerifyHashedPassword(
            request.Password,
            user.PasswordHash);

    if (!isValidPassword)
    {
        user.FailedLoginAttempts++;

        if (user.FailedLoginAttempts >= 5)
        {
            user.LockedUntil = DateTime.UtcNow.AddMinutes(15);
        }

        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.UpdateSecurityAsync(user);

        throw new UnauthorizedException("Invalid password.");
    }
    
    user.FailedLoginAttempts = 0;
    user.LockedUntil = null;
    user.LastLoginAt = DateTime.UtcNow;
    user.UpdatedAt = DateTime.UtcNow;

    await _userRepository.UpdateSecurityAsync(user);
    
    var existingTokens =
        await _refreshTokenRepository.GetByUserIdAsync(user.Id);

    foreach (var token in existingTokens)
    {
        if (!token.IsRevoked)
        {
            await _refreshTokenRepository.RevokeAsync(token.Id);
        }
    }

    // Generate JWT
    var accessToken =
        _jwtGenerator.GenerateAccessToken(user);

    // Generate Refresh Token
    var refreshTokenValue =
        _jwtGenerator.GenerateRefreshToken();

    var refreshToken = new RefreshToken
    {
        Id = Guid.NewGuid(),
        UserId = user.Id,
        Token = refreshTokenValue,
        ExpiresAt = DateTime.UtcNow.AddDays(7),
        IsRevoked = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    };

    await _refreshTokenRepository.AddAsync(refreshToken);

    return new AuthResponse
    {
        Message = "User logged in successfully.",
        AccessToken = accessToken,
        RefreshToken = refreshTokenValue
    };
}

    public async Task<AuthResponse> RefreshTokenAsync(string refreshToken)
    {
        var token = await _refreshTokenRepository.GetByTokenAsync(refreshToken);

        if (token == null)
            throw new UnauthorizedException("Invalid refresh token.");

        if (token.IsRevoked)
            throw new UnauthorizedException("Refresh token has been revoked.");

        if (token.ExpiresAt <= DateTime.UtcNow)
            throw new UnauthorizedException("Refresh token has expired.");

        var user = await _userRepository.GetByIdAsync(token.UserId);

        if (user == null)
            throw new NotFoundException("User not found.");
        
        await _refreshTokenRepository.RevokeAsync(token.Id);
        
        var accessToken = _jwtGenerator.GenerateAccessToken(user);
        
        var refreshTokenValue = _jwtGenerator.GenerateRefreshToken();
        
        await _refreshTokenRepository.AddAsync(new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = refreshTokenValue,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            IsRevoked = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        return new AuthResponse
        {
            Message = "Token refreshed successfully.",
            AccessToken = accessToken,
            RefreshToken = refreshTokenValue
        };
    }

    public async Task LogoutAsync(string? refreshToken)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
            return;

        var token = await _refreshTokenRepository.GetByTokenAsync(refreshToken);

        if (token == null)
            return;

        if (token.IsRevoked)
            return;

        await _refreshTokenRepository.RevokeAsync(token.Id);
    }
}

