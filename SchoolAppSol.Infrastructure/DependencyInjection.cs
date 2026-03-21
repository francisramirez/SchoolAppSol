using Microsoft.Extensions.DependencyInjection;
using SchoolAppSol.Application.Interfaces.Base;
using SchoolAppSol.Application.Interfaces.Auth;
using SchoolAppSol.Infrastructure.Services;

namespace SchoolAppSol.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IDateTimeService, DateTimeService>();
        services.AddTransient<ITokenService, TokenService>();

        return services;
    }
}
