using TechLoop.Domain.Entities;

namespace TechLoop.Application.Interfaces.Repositories;

public interface IQuestionRepository
{
    Task<int> CreateAsync(Question question, CancellationToken cancellationToken);

    Task<int> UpdateAsync(Question question, CancellationToken cancellationToken);

    Task<int> SoftDeleteAsync(int id, Guid deletedBy, CancellationToken cancellationToken);

    // Mentor
    Task<IEnumerable<Question>> GetAllAsync(CancellationToken cancellationToken);
    Task<Question?> GetByIdAsync(int id, CancellationToken cancellationToken);

    // Learner
    Task<IEnumerable<Question>> GetPublishedAsync(CancellationToken cancellationToken);
    Task<Question?> GetPublishedByIdAsync(int id, CancellationToken cancellationToken);

    Task<bool> SlugExistsAsync(string slug, CancellationToken cancellationToken);

    Task<bool> PositionExistsAsync(int subTopicId, int position, CancellationToken cancellationToken);

    Task<bool> SubTopicExistsAsync(int subTopicId, CancellationToken cancellationToken);
}