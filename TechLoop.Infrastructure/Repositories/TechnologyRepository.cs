using Dapper;
using TechLoop.Application.Interfaces.Infrastructure;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Domain.Entities;

namespace TechLoop.Infrastructure.Repositories;

public sealed class TechnologyRepository : ITechnologyRepository
{
    private readonly IDapperContext _context;

    public TechnologyRepository(
        IDapperContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(
        string name,
        CancellationToken cancellationToken)
    {
        const string sql = @"
SELECT EXISTS
(
    SELECT 1
    FROM technologies
    WHERE LOWER(name)=LOWER(@Name)
    AND deleted_at IS NULL
);";
        using var connection =
            _context.CreateConnection();

        return await connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(
                sql,
                new
                {
                    Name = name
                },
                cancellationToken: cancellationToken));
    }
    
    //SlugExists
    public async Task<bool> SlugExistsAsync(string slug, CancellationToken cancellationToken)
    {
        const string sql = @"
SELECT EXISTS
(
    SELECT 1
    FROM technologies
    WHERE LOWER(slug) = LOWER(@Slug)
    AND deleted_at IS NULL
);";
        using var connection = _context.CreateConnection();
        return await connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(
                sql,
                new { Slug = slug },
                cancellationToken: cancellationToken));
    }
    
    
    //PositionExists
    public async Task<bool> PositionExistsAsync(int position, CancellationToken cancellationToken)
    {
        const string sql = @"
SELECT EXISTS
(
    SELECT 1
    FROM technologies
    WHERE position = @Position
    AND deleted_at IS NULL
);";
        using var connection = _context.CreateConnection();
        return await connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(
                sql,
                new { Position = position },
                cancellationToken: cancellationToken));
    }
    
    
    
    //CategoryExistsAsync
    public async Task<bool> CategoryExistsAsync(
        int categoryId,
        CancellationToken cancellationToken)
    {
        const string sql = @"
SELECT EXISTS
(
    SELECT 1
    FROM technology_categories
    WHERE id = @CategoryId
);";

        using var connection = _context.CreateConnection();

        return await connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(
                sql,
                new { CategoryId = categoryId },
                cancellationToken: cancellationToken));
    }

    public async Task<int> CreateAsync(Technology technology, CancellationToken cancellationToken)
    {
        const string sql = @"
INSERT INTO technologies
(
    category_id,
    name,
    slug,
    description,
    image_url,
    position,
    status,
    published_at,
    published_by,
    created_by,
    created_at
)
VALUES
(
    @CategoryId,
    @Name,
    @Slug,
    @Description,
    @ImageUrl,
    @Position,
    @Status,
    @PublishedAt,
    @PublishedBy,
    @CreatedBy,
    @CreatedAt
)
RETURNING id;
";
        using var connection = _context.CreateConnection();

        return await connection.ExecuteScalarAsync<int>(
            new CommandDefinition(
                sql,
                technology,
                cancellationToken: cancellationToken));
    }

    //get technology by id
    public async Task<Technology?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        const string sql = @"
SELECT
    id,
    category_id      AS CategoryId,
    name,
    slug,
    description,
    image_url        AS ImageUrl,
    position,
    status,
    published_at     AS PublishedAt,
    published_by     AS PublishedBy,
    created_by       AS CreatedBy,
    created_at       AS CreatedAt,
    updated_at       AS UpdatedAt,
    updated_by       AS UpdatedBy,
    deleted_at       AS DeletedAt,
    deleted_by       AS DeletedBy
FROM technologies
WHERE id = @Id
AND deleted_at IS NULL;
";
        using var connection = _context.CreateConnection();

        return await connection.QuerySingleOrDefaultAsync<Technology>(
            new CommandDefinition(
                sql,
                new
                {
                    Id = id
                },
                cancellationToken: cancellationToken));
    }


    //updateTechnology

    public async Task<int> UpdateAsync(Technology technology, CancellationToken cancellationToken)
    {
        const string sql = @"
UPDATE technologies
SET
    category_id = @CategoryId,
    name = @Name,
    slug = @Slug,
    description = @Description,
    image_url = @ImageUrl,
    position = @Position,
    updated_by = @UpdatedBy,
    updated_at = @UpdatedAt
WHERE id = @Id
AND deleted_at IS NULL;
";
        using var connection = _context.CreateConnection();

        var rowsAffected = await connection.ExecuteAsync(sql, new
        {
            technology.Id,
            technology.CategoryId,
            technology.Name,
            technology.Slug,
            technology.Description,
            technology.ImageUrl,
            technology.Position,
            technology.UpdatedAt,
            technology.UpdatedBy,

        });

        return rowsAffected;

    }
    
    //Soft Delete
    public async Task<int> SoftDeleteAsync(int id, Guid deletedBy, CancellationToken cancellationToken)
    {
        const string sql = @"
UPDATE technologies
SET
    deleted_at = @DeletedAt,
    deleted_by = @DeletedBy
WHERE id = @Id
AND deleted_at IS NULL;";

        using var connection = _context.CreateConnection();

        return await connection.ExecuteAsync(sql, new
        {
            Id = id,
            DeletedAt = DateTime.UtcNow,
            DeletedBy = deletedBy
        });
    }
    
    //Get all technologies
    public async Task<IEnumerable<Technology>> GetAllAsync(CancellationToken cancellationToken)
    {
        const string sql = @"
SELECT
    id,
    category_id AS CategoryId,
    name,
    slug,
    description,
    image_url AS ImageUrl,
    position,
    status,
    published_at AS PublishedAt,
    published_by AS PublishedBy,
    created_by AS CreatedBy,
    created_at AS CreatedAt,
    updated_by AS UpdatedBy,
    updated_at AS UpdatedAt
FROM technologies
WHERE deleted_at IS NULL
ORDER BY position;";

        using var connection = _context.CreateConnection();

        return await connection.QueryAsync<Technology>(
            new CommandDefinition(
                sql,
                cancellationToken: cancellationToken));
    }
    
    public async Task<int> PublishAsync(Technology technology, CancellationToken cancellationToken)
    {
        const string sql = @"
UPDATE technologies
SET
    published_at = @PublishedAt,
    published_by = @PublishedBy
WHERE id = @Id
AND deleted_at IS NULL;";

        using var connection = _context.CreateConnection();
        return await connection.ExecuteAsync(new CommandDefinition(sql, technology, cancellationToken: cancellationToken));
    }
}