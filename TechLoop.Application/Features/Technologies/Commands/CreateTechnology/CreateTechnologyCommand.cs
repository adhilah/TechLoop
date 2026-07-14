using TechLoop.Application.Features.Technologies.DTOs;
using TechLoop.Domain.Enums;
using MediatR;

namespace TechLoop.Application.Features.Technologies.Commands.CreateTechnology;

public sealed record CreateTechnologyCommand(
    int CategoryId,
    string Name,
    string? Description,
    string? Slug,
    string? ImageUrl,
    int Position
) : IRequest<CreateTechnologyResponse>;