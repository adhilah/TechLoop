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
    
    
    public async Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken)
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
}