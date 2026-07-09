using Dapper;
using TechLoop.Application.Interfaces.Infrastructure;
using TechLoop.Application.Interfaces.Repositories;

namespace TechLoop.Infrastructure.Repositories;

public sealed class CategoryRepository : ICategoryRepository
{
    private readonly IDapperContext _context;

    public CategoryRepository(IDapperContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
    {
        const string sql = @"
SELECT EXISTS
(
    SELECT 1
    FROM technology_categories
    WHERE id = @Id
);";

        using var connection = _context.CreateConnection();

        return await connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(
                sql,
                new { Id = id },
                cancellationToken: cancellationToken));
    }
}