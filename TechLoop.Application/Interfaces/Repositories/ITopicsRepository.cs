using TechLoop.Domain;
using TechLoop.Domain.Entities;

namespace TechLoop.Application.Interfaces.Repositories;

public interface ITopicsRepository
{
    Task<bool> ExistsAsync(string name, CancellationToken cancellationToken);
    Task<bool> SlugExistsAsync(string slug, CancellationToken cancellationToken);
    Task<bool> PositionExistsAsync(int position, CancellationToken cancellationToken);
    Task<bool> TechnologyExistsAsync(int technologyId, CancellationToken cancellationToken);
    Task<int> CreateAsync(Topic topic, CancellationToken cancellationToken);
    Task<int> UpdateAsync(Topic topic, CancellationToken cancellationToken);
    Task<int> SoftDeleteAsync(int id, Guid deletedBy, CancellationToken cancellationToken);
    Task<IEnumerable<Topic>> GetAllAsync(CancellationToken cancellationToken);
    Task<Topic?> GetByIdAsync(int id, CancellationToken cancellationToken);
}