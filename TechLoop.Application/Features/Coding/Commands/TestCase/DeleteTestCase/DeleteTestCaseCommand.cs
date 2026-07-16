using MediatR;
using TechLoop.Application.Features.Coding.DTOs;

namespace TechLoop.Application.Features.Coding.Commands.DeleteTestCase;

public sealed record DeleteTestCaseCommand(int Id) : IRequest<DeleteTestCaseResponse>;