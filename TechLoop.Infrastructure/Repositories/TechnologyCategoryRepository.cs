using Dapper;
using TechLoop.Application.Features.TechnologyCategories.DTOs;
using TechLoop.Application.Interfaces.Infrastructure;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Domain.Entities;

namespace TechLoop.Infrastructure.Repositories;

public sealed class TechnologyCategoryRepository : ITechnologyCategoryRepository
{
    private readonly IDapperContext _context;

    public TechnologyCategoryRepository(IDapperContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAsync(TechnologyCategory technologyCategory, CancellationToken cancellationToken)
    {
        using var connection = _context.CreateConnection();

        return await connection.ExecuteAsync(
            "CALL sp_technology_category_create(@Name,@CreatedBy)",
            new
            {
                technologyCategory.Name,
                technologyCategory.CreatedBy
            });
    }

    public async Task<int> UpdateAsync(TechnologyCategory technologyCategory, CancellationToken cancellationToken)
    {
        using var connection = _context.CreateConnection();

        return await connection.ExecuteAsync(
            "CALL sp_technology_category_update(@Id,@Name,@UpdatedBy)",
            new
            {
                technologyCategory.Id,
                technologyCategory.Name,
                technologyCategory.UpdatedBy
            });
    }

    public async Task<int> PublishAsync(int id, CancellationToken cancellationToken)
    {
        using var connection = _context.CreateConnection();

        return await connection.ExecuteAsync(
            "CALL sp_technology_category_publish(@Id)",
            new { Id = id });
    }

    public async Task<int> DeleteAsync(int id, Guid deletedBy, CancellationToken cancellationToken)
    {
        using var connection = _context.CreateConnection();

        return await connection.ExecuteAsync(
            "CALL sp_technology_category_delete(@Id,@DeletedBy)",
            new
            {
                Id = id,
                DeletedBy = deletedBy
            });
    }

    public async Task<IEnumerable<AdminTechnologyCategoryResponse>> GetAllForAdminAsync()
    {
        using var connection = _context.CreateConnection();

        return await connection.QueryAsync<AdminTechnologyCategoryResponse>(
            "SELECT * FROM fn_technology_category_admin_get_all()");
    }

    public async Task<IEnumerable<LearnerMentorTechnologyCategoryResponse>> GetAllForPublicAsync()
    {
        using var connection = _context.CreateConnection();

        return await connection.QueryAsync<LearnerMentorTechnologyCategoryResponse>(
            "SELECT * FROM fn_technology_category_public_get_all()");
    }

    public async Task<AdminTechnologyCategoryResponse?> GetByIdForAdminAsync(int id, CancellationToken cancellationToken)
    {
        using var connection = _context.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<AdminTechnologyCategoryResponse>(
            new CommandDefinition(
                "SELECT * FROM fn_technology_category_admin_get_by_id(@Id)",
                new { Id = id },
                cancellationToken: cancellationToken));
    }

    public async Task<LearnerMentorTechnologyCategoryResponse?> GetByIdForPublicAsync(int id)
    {
        using var connection = _context.CreateConnection();

        return await connection.QuerySingleOrDefaultAsync<LearnerMentorTechnologyCategoryResponse>(
            "SELECT * FROM fn_technology_category_public_get_by_id(@Id)",
            new { Id = id });
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
    {
        using var connection = _context.CreateConnection();

        return await connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(
                "SELECT fn_technology_category_exists(@Id)",
                new { Id = id },
                cancellationToken: cancellationToken));
    }
    
    public async Task<bool> NameExistsAsync(string name, int? excludeId, CancellationToken cancellationToken)
    {
        using var connection = _context.CreateConnection();
        const string sql = "SELECT fn_technology_category_name_exists(@Name, @ExcludeId);";

        return await connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(
                sql,
                new
                {
                    Name = name,
                    ExcludeId = excludeId
                },
                cancellationToken: cancellationToken));
    }
}