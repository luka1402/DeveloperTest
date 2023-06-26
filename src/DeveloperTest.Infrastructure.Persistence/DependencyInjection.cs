using DeveloperTest.Core.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperTest.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IDeveloperTestContext, DeveloperTestContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("DeveloperTest"), optionts => optionts.MigrationsAssembly("DeveloperTest.Infrastructure.Persistence"));

        });
       
        return services;
    }
}

