using TechLoop.Domain;
using TechLoop.Domain.Entities;

namespace TechLoop.Application.Interfaces.Repositories;

public interface ITopicsRepository
{
    Task<int> CreateAsync(Topic topic, CancellationToken cancellationToken);
    Task<int> UpdateAsync(Topic topic, CancellationToken cancellationToken);
    Task<int> SoftDeleteAsync(int id, Guid deletedBy, CancellationToken cancellationToken);
    Task<IEnumerable<Topic>> GetAllAsync(CancellationToken cancellationToken);
    Task<Topic?> GetByIdAsync(int id, CancellationToken cancellationToken);
}