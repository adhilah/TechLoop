using TechLoop.Domain.Entities;

namespace TechLoop.Application.Interfaces.Repositories;

public interface ISubTopicsRepository
{
    Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken);
    Task<int> CreateAsync(SubTopic subTopic, CancellationToken cancellationToken);
    Task<SubTopic?> GetByIdAsync(int id, CancellationToken cancellationToken);
}