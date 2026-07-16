using MediatR;

namespace TechLoop.Application.Features.MCQ.Commands.DeleteMcqOption;

public sealed record DeleteMcqOptionCommand(int Id) : IRequest<bool>;