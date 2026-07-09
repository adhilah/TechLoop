using TechLoop.Domain.Entities;

namespace TechLoop.Application.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
}