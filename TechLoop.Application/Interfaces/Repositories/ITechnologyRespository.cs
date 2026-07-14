using TechLoop.Domain.Entities;

namespace TechLoop.Application.Interfaces.Repositories;

public interface ITechnologyRepository
{
    Task<bool> ExistsAsync(string name, CancellationToken cancellationToken);
    Task<bool> SlugExistsAsync(string slug, CancellationToken cancellationToken);

    Task<bool> PositionExistsAsync(int position, CancellationToken cancellationToken);
    Task<bool> CategoryExistsAsync(int categoryId, CancellationToken cancellationToken);

    Task<int> CreateAsync(Technology technology, CancellationToken cancellationToken);
    Task<int> UpdateAsync(Technology technology, CancellationToken cancellationToken);
    
    Task<int> SoftDeleteAsync(int id, Guid deletedBy, CancellationToken cancellationToken);
    
    Task<IEnumerable<Technology>> GetAllAsync(CancellationToken cancellationToken);
    Task<Technology?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<int> PublishAsync(Technology technology, CancellationToken cancellationToken);
}