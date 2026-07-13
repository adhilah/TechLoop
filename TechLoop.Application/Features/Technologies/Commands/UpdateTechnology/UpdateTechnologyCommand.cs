using TechLoop.Application.Features.Technologies.DTOs;
using MediatR;
using TechLoop.Domain.Enums;

namespace TechLoop.Application.Features.Technologies.Commands.UpdateTechnology;

public sealed record UpdateTechnologyCommand(
     int id,
     int CategoryId,
     string Name,
     string Description,
     string Slug,
     string ImageUrl,
     int Position
) : IRequest<UpdateTechnologyResponse>;

