using MediatR;
using TechLoop.Application.Features.Coding.DTOs;

namespace TechLoop.Application.Features.Coding.Commands.CreateTestCase;

public sealed record CreateTestCaseCommand : IRequest<CreateTestCaseResponse>
{
    public int QuestionId { get; init; }
    public string Input { get; init; } = string.Empty;
    public string ExpectedOutput { get; init; } = string.Empty;
    public bool IsHidden { get; init; }
    public int Position { get; init; }
}