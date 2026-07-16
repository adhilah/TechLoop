using MediatR;
using TechLoop.Application.Common.Exceptions;
using TechLoop.Application.Features.Coding.DTOs;
using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Application.Interfaces.Services;

namespace TechLoop.Application.Features.Coding.Commands.DeleteTestCase;

public sealed class DeleteTestCaseCommandHandler : IRequestHandler<DeleteTestCaseCommand, DeleteTestCaseResponse>
{
    private readonly ITestCaseRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public DeleteTestCaseCommandHandler(ITestCaseRepository repository, ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<DeleteTestCaseResponse> Handle(DeleteTestCaseCommand request, CancellationToken cancellationToken)
    {
        var testCase = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (testCase is null)
            throw new NotFoundException("Test case not found.");

        var rowsAffected = await _repository.SoftDeleteAsync(request.Id, _currentUser.UserId, cancellationToken);
        if (rowsAffected <= 0)
            throw new Exception("Failed to delete test case.");

        return new DeleteTestCaseResponse
        {
            Success = true,
            Message = "Test case deleted successfully."
        };
    }
}