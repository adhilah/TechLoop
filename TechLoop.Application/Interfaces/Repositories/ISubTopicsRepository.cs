using TechLoop.Domain.Entities;

namespace TechLoop.Application.Interfaces.Repositories;

public interface ISubTopicsRepository
{
    Task<bool> ExistsAsync(string slug, CancellationToken cancellationToken);
    Task<bool> SlugExistsAsync(string slug, CancellationToken cancellationToken);
    Task<bool> PositionExistsAsync(int position, CancellationToken cancellationToken);
    Task<int> CreateAsync(SubTopic subTopic, CancellationToken cancellationToken);
    Task<int> UpdateAsync(SubTopic subTopic, CancellationToken cancellationToken);
    Task<int> SoftDeleteAsync(int id, Guid deletedBy, CancellationToken cancellationToken);
    Task<SubTopic?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<SubTopic>> GetAllAsync(CancellationToken cancellationToken);
    Task<bool> TopicExistsAsync(int topicId, CancellationToken cancellationToken);
    Task<int> PublishAsync(SubTopic subTopic, CancellationToken cancellationToken);
}