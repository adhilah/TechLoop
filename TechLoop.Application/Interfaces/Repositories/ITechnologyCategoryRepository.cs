using TechLoop.Application.Features.TechnologyCategories.DTOs;
using TechLoop.Domain.Entities;

namespace TechLoop.Application.Interfaces.Repositories;

public interface ITechnologyCategoryRepository
{
    // Write

    Task<int> CreateAsync(TechnologyCategory technologyCategory, CancellationToken cancellationToken);

    Task<int> UpdateAsync(TechnologyCategory technologyCategory, CancellationToken cancellationToken);

    Task<int> PublishAsync(int id, CancellationToken cancellationToken);

    Task<int> DeleteAsync(int id, Guid deletedBy, CancellationToken cancellationToken);

    // Read

    Task<IEnumerable<AdminTechnologyCategoryResponse>> GetAllForAdminAsync();

    Task<IEnumerable<LearnerMentorTechnologyCategoryResponse>> GetAllForPublicAsync();

    Task<AdminTechnologyCategoryResponse?> GetByIdForAdminAsync(int id, CancellationToken cancellationToken);
    
    Task<LearnerMentorTechnologyCategoryResponse?> GetByIdForPublicAsync(int id);
    
    Task<bool> NameExistsAsync(string name, int? excludeId, CancellationToken cancellationToken);

    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
}


