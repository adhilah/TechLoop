using TechLoop.Domain.Entities;

namespace TechLoop.Application.Interfaces.Repositories;

public interface ICodingTemplateRepository
{
    Task<int> CreateAsync(CodingTemplate template, CancellationToken cancellationToken);
    Task<int> UpdateAsync(CodingTemplate template, CancellationToken cancellationToken);
    Task<int> SoftDeleteAsync(int id, Guid deletedBy, CancellationToken cancellationToken);
    Task<int> SoftDeleteByQuestionIdAsync(int questionId, Guid deletedBy, CancellationToken cancellationToken);
    Task<CodingTemplate?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<CodingTemplate>> GetByQuestionIdAsync(int questionId, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(int questionId, int technologyId, CancellationToken cancellationToken);
}