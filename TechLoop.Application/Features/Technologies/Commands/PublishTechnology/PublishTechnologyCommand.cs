using MediatR;
using TechLoop.Application.Features.Technologies.DTOs;

namespace TechLoop.Application.Features.Technologies.Commands.PublishTechnology;

public sealed record PublishTechnologyCommand(int Id ) : IRequest<PublishTechnologyResponse>;