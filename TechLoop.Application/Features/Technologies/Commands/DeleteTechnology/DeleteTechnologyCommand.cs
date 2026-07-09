using TechLoop.Application.Features.Technologies.DTOs;
using MediatR;

namespace TechLoop.Application.Features.Technologies.Commands.DeleteTechnology;

public sealed record DeleteTechnologyCommand(
    int Id
) : IRequest<DeleteTechnologyResponse>;
