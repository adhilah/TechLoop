using TechLoop.Domain.Enums;
namespace TechLoop.Application.Interfaces.Services;

public interface ICurrentUserService
{
    Guid UserId { get; }

    UserRole Role { get; }

    bool IsAuthenticated { get; }
}