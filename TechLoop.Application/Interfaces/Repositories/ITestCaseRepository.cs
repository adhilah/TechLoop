using TechLoop.Domain.Entities;

namespace TechLoop.Application.Interfaces.Repositories;

public interface ITestCaseRepository
{
    Task<int> CreateAsync(TestCase testCase, CancellationToken cancellationToken);
    Task<int> UpdateAsync(TestCase testCase, CancellationToken cancellationToken);
    Task<int> SoftDeleteAsync(int id, Guid deletedBy, CancellationToken cancellationToken);
    Task<TestCase?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<TestCase>> GetByQuestionIdAsync(int questionId, CancellationToken cancellationToken);
    Task<bool> PositionExistsAsync(int questionId, int position, CancellationToken cancellationToken);
    Task<IEnumerable<TestCase>> GetVisibleByQuestionIdAsync(int questionId, CancellationToken cancellationToken);
}