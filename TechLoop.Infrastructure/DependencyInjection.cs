using TechLoop.Application.Interfaces.Repositories;
using TechLoop.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace TechLoop.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<
            ITechnologyRepository,
            TechnologyRepository>();

        return services;
    }
}