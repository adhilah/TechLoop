using MediatR;
using TechLoop.Application.Features.MCQ.DTOs;

namespace TechLoop.Application.Features.MCQ.Commands.UpdateMcqOption;

public sealed record UpdateMcqOptionCommand : IRequest<UpdateMcqOptionResponse>
{
    public int Id { get; init; }
    public string OptionText { get; init; } = string.Empty;
    public bool IsCorrect { get; init; }
    public int Position { get; init; }
}