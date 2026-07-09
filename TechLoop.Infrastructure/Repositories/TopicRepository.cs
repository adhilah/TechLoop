using System.Data;
using Dapper;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Application.Interfaces.Infrastructure;
using TechLoop.Domain.Entities;

namespace TechLoop.Infrastructure.Repositories;

public sealed class TopicRepository : ITopicsRepository
{
    private readonly IDapperContext _context;

    public TopicRepository(IDapperContext context)
    {
        _context = context;
    }
    //ExistsAync
    public async Task<bool> ExistsAsync(string title, CancellationToken cancellationToken)
    {
        const string sql = @"
SELECT EXISTS
(
    SELECT 1
    FROM topics
    WHERE LOWER(title) = LOWER(@Title)
    AND deleted_at IS NULL
);";

        using var connection = _context.CreateConnection();

        return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(
                sql,
                new { Title = title },
                cancellationToken: cancellationToken));
    }
    
    //TechnologyExistsAsync
    public async Task<bool> TechnologyExistsAsync(int technologyId, CancellationToken cancellationToken)
    {
        const string sql = @"
SELECT EXISTS
(
    SELECT 1
    FROM technologies
    WHERE id = @TechnologyId
);";

        using var connection = _context.CreateConnection();

        return await connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(
                sql,
                new { TechnologyId = technologyId },
                cancellationToken: cancellationToken));
    }
    //create topic
    public async Task<int> CreateAsync(Topic topic, CancellationToken cancellationToken)
    {
        const string sql = @"
INSERT INTO topics
(
    technology_id,
    title,
    slug,
    description,
    image_url,
    position,
    status,
    published_at,
    published_by,
    created_by,
    created_at,
    updated_at,
    updated_by,
    deleted_at,
    deleted_by
)
VALUES
(
    @TechnologyId,
    @Title,
    @Slug,
    @Description,
    @ImageUrl,
    @Position,
    @Status,
    @PublishedAt,
    @PublishedBy,
    @CreatedBy,
    @CreatedAt,
    @UpdatedAt,
    @UpdatedBy,
    @DeletedAt,
    @DeletedBy
)
RETURNING id;
";

        using var connection = _context.CreateConnection();

        return await connection.ExecuteScalarAsync<int>(
            new CommandDefinition(sql, topic, cancellationToken: cancellationToken));
    }
    
    //update topic
    public async Task<int> UpdateAsync(Topic topic, CancellationToken cancellationToken)
    {
        const string sql = @"
UPDATE topics
SET
    technology_id = @TechnologyId,
    title = @Title,
    slug = @Slug,
    description = @Description,
    image_url = @ImageUrl,
    position = @Position,
    status = @Status,
    updated_by = @UpdatedBy,
    updated_at = @UpdatedAt
WHERE id = @Id
AND deleted_at IS NULL;
";
        using var connection = _context.CreateConnection();

        var rowsAffected = await connection.ExecuteAsync(sql, new
        {
            topic.Id,
            topic.TechnologyId,
            topic.Title,
            topic.Description,
            topic.Slug,
            topic.ImageUrl,
            topic.Position,
            topic.Status,
            topic.UpdatedBy,
            topic.UpdatedAt

        });
        
        return rowsAffected;
    }
    
    
    //soft delete
    public async Task<int> SoftDeleteAsync(int id, Guid deletedBy, CancellationToken cancellationToken)
    {
        const string sql = @"
UPDATE  topics
SET  deleted_at = @DeletedAt,
    deleted_by= @DeletedBy
WHERE id = @Id
AND deleted_at is null;
";

        using var connection = _context.CreateConnection();
        return await connection.ExecuteAsync(sql, new
        {
            Id = id,
            DeletedAt = DateTime.UtcNow,
            DeletedBy = deletedBy
        });
    }
    
    //Get all topics

    public async Task<IEnumerable<Topic>> GetAllAsync( CancellationToken cancellationToken)
    {
        const string sql = @"
SELECT id,
        technology_id,
        title,
        description,
        slug,
        image_url,
        position,
        status,
        published_at AS PublishedAt,
        published_by AS PublishedBy,
        created_at AS CreatedAt,
        created_by AS CreatedBy,
        updated_at AS UpdatedAt,
        updated_by as UpdatedBy,
        deleted_at AS DeletedAt,
        deleted_by AS DeletedBy
FROM topics
WHERE deleted_at is null
ORDER BY Position;
";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Topic>(
            new CommandDefinition(sql, cancellationToken: cancellationToken));
    }
    
    //get topic by id

    public async Task<Topic?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        const string sql = @"
SELECT
    id,
    technology_id AS TechnologyId,
    title,
    description,
    slug,
    image_url,
    position,
    status,
    published_at AS PublishedAt,
    published_by AS PublishedBy,
    created_at AS CreatedAt,
    created_by AS CreatedBy,
    updated_at AS UpdatedAt,
    updated_by AS UpdatedBy,
    deleted_at AS DeletedAt,
    deleted_by AS DeletedBy
FROM topics
WHERE id = @Id
AND deleted_at IS NULL;
";
        using var connection = _context.CreateConnection();
        
        return await  connection.QuerySingleOrDefaultAsync<Topic>(
            new CommandDefinition(sql, 
                new
                {
                    Id = id
                },
                cancellationToken: cancellationToken));
    }
}