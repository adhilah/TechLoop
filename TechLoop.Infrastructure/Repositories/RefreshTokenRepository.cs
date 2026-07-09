using Dapper;
using TechLoop.Application.Interfaces.Infrastructure;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Domain.Entities;

namespace TechLoop.Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly IDapperContext _context;

    public RefreshTokenRepository(IDapperContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        using var connection = _context.CreateConnection();
        const string sql = """
            SELECT
                id,
                user_id,
                token,
                expires_at,
                is_revoked,
                created_at,
                updated_at
            FROM refresh_tokens
            WHERE token = @Token;
            """;
        return await connection.QueryFirstOrDefaultAsync<RefreshToken>(
            sql,
            new { Token = token });
    }

    public async Task<IEnumerable<RefreshToken>> GetByUserIdAsync(Guid userId)
    {
        using var connection = _context.CreateConnection();
        const string sql = """
            SELECT
                id,
                user_id,
                token,
                expires_at,
                is_revoked,
                created_at,
                updated_at
            FROM refresh_tokens
            WHERE user_id = @UserId
            ORDER BY created_at DESC;
            """;
        return await connection.QueryAsync<RefreshToken>(
            sql,
            new { UserId = userId });
    }

    public async Task AddAsync(RefreshToken refreshToken)
    {
        using var connection = _context.CreateConnection();
        const string sql = """
            INSERT INTO refresh_tokens
            (
                id,
                user_id,
                token,
                expires_at,
                is_revoked,
                created_at,
                updated_at
            )
            VALUES
            (
                @Id,
                @UserId,
                @Token,
                @ExpiresAt,
                @IsRevoked,
                @CreatedAt,
                @UpdatedAt
            );
            """;
        await connection.ExecuteAsync(sql, refreshToken);
    }
    public async Task UpdateAsync(RefreshToken refreshToken)
    {
        using var connection = _context.CreateConnection();
        const string sql = """
            UPDATE refresh_tokens
            SET
                token = @Token,
                expires_at = @ExpiresAt,
                is_revoked = @IsRevoked,
                updated_at = @UpdatedAt
            WHERE id = @Id;
            """;
        await connection.ExecuteAsync(sql, refreshToken);
    }
    public async Task RevokeAsync(Guid id)
    {
        using var connection = _context.CreateConnection();
        const string sql = """
            UPDATE refresh_tokens
            SET
                is_revoked = TRUE,
                updated_at = NOW()
            WHERE id = @Id;
            """;
        await connection.ExecuteAsync(sql, new { Id = id });
    }
}