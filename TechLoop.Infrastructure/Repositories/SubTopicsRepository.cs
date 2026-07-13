using Dapper;
using TechLoop.Application.Interfaces.Infrastructure;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Domain.Entities;

namespace TechLoop.Infrastructure.Repositories;

public class SubTopicsRepository : ISubTopicsRepository
{
    private readonly IDapperContext _context;

    public SubTopicsRepository(IDapperContext context)
    {
        _context = context;
    }
    
    
    public async Task<bool> ExistsAsync(string title, CancellationToken cancellationToken)
    {
        const string sql = @"
SELECT EXISTS
(
    SELECT 1
    FROM sub_topics
    WHERE LOWER(title) = LOWER(@Title)
    AND deleted_at IS NULL
);";

        using var connection = _context.CreateConnection();
        return await connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(
                sql,
                new { Title = title },
                cancellationToken: cancellationToken));
    }
    
    //SlugExists
    public async Task<bool> SlugExistsAsync(string slug, CancellationToken cancellationToken)
    {
        const string sql = @"
SELECT EXISTS
(
    SELECT 1
    FROM sub_topics
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
    FROM sub_topics
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

    public async Task<int> CreateAsync(SubTopic subTopic, CancellationToken cancellationToken)
    {
        const string sql = @"
            INSERT INTO sub_topics
            (
                topic_id,
                slug,
                title,
                description,
                image_url,
                position,
                status,
                published_at,
                published_by,
                created_at,
                created_by
            )
            VALUES
            (
                @TopicId,
                @Slug,
                @Title,
                @Description,
                @ImageUrl,
                @Position,
                @Status,
                @PublishedAt,
                @PublishedBy,
                @CreatedAt,
                @CreatedBy
            )
            RETURNING id;";

        using var connection = _context.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(sql, subTopic);
    }
    
    //Update subtopic
    public async Task<int> UpdateAsync(SubTopic subTopic, CancellationToken cancellationToken)
    {
        const string sql = @"
UPDATE sub_topics
SET
    topic_id = @TopicId,
    title = @Title,
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
        return await connection.ExecuteAsync(
            new CommandDefinition(sql, subTopic, cancellationToken: cancellationToken));
    }
    
    //Soft delete subtopic
    public async Task<int> SoftDeleteAsync(int id, Guid deletedBy, CancellationToken cancellationToken)
    {
        const string sql = @"
UPDATE sub_topics
SET
    deleted_at = @DeletedAt,
    deleted_by = @DeletedBy
WHERE id = @Id
AND deleted_at IS NULL;
";

        using var connection = _context.CreateConnection();
        return await connection.ExecuteAsync(
            new CommandDefinition(
                sql,
                new
                {
                    Id = id,
                    DeletedAt = DateTime.UtcNow,
                    DeletedBy = deletedBy
                },
                cancellationToken: cancellationToken));
    }
    
    //topic exista
    public async Task<bool> TopicExistsAsync(int topicId, CancellationToken cancellationToken)
    {
        const string sql = @"
SELECT EXISTS
(
    SELECT 1
    FROM topics
    WHERE id = @TopicId
    AND deleted_at IS NULL
);";

        using var connection = _context.CreateConnection();

        return await connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(
                sql,
                new { TopicId = topicId },
                cancellationToken: cancellationToken));
    }
    
    //Get subtopic by id 
    public async Task<SubTopic?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        const string sql = @"
SELECT
    id,
    topic_id AS TopicId,
    slug,
    title,
    description,
    image_url AS ImageUrl,
    position,
    status,
    published_at AS PublishedAt,
    published_by AS PublishedBy,
    created_by AS CreatedBy,
    created_at AS CreatedAt,
    updated_by AS UpdatedBy,
    updated_at AS UpdatedAt,
    deleted_at AS DeletedAt,
    deleted_by AS DeletedBy
FROM sub_topics
WHERE id = @Id
AND deleted_at IS NULL;
";

        using var connection = _context.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<SubTopic>(
            new CommandDefinition(
                sql,
                new { Id = id },
                cancellationToken: cancellationToken));
    }
    
    //Get all subtopics
    public async Task<IEnumerable<SubTopic>> GetAllAsync(CancellationToken cancellationToken)
    {
        const string sql = @"
SELECT
    id,
    topic_id AS TopicId,
    slug,
    title,
    description,
    image_url AS ImageUrl,
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
FROM sub_topics
WHERE deleted_at IS NULL
ORDER BY position;
";

        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<SubTopic>(
            new CommandDefinition(sql, cancellationToken: cancellationToken));
    }
}