using MediatR;
using TechLoop.Application.Features.MCQ.DTOs;

namespace TechLoop.Application.Features.MCQ.Commands.CreateMcqOption;

public sealed record CreateMcqOptionCommand : IRequest<CreateMcqOptionResponse>
{
    public int QuestionId { get; init; }
    public string OptionText { get; init; } = string.Empty;
    public bool IsCorrect { get; init; }
    public int Position { get; init; }
}