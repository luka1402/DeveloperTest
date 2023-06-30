using DeveloperTest.Core.Application.Interfaces;
using DeveloperTest.Core.Domain;
using DeveloperTest.Infrastructure.Persistence.Repositories;
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
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IEmployeeCompanyRepository, EmployeeCompanyRepository>();
        services.AddScoped<ISystemLogRepository, SystemLogRepository>();
        return services;
    }
}

