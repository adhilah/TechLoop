using MediatR;
using TechLoop.Application.Features.Coding.DTOs;

namespace TechLoop.Application.Features.Coding.Commands.DeleteCodingTemplate;

public sealed record DeleteCodingTemplateCommand(int Id) : IRequest<DeleteCodingTemplateResponse>;