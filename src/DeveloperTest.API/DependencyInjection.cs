using DeveloperTest.API.Filters;
using FluentValidation.AspNetCore;

namespace DeveloperTest.API;

public static class DependencyInjection
{
    public static IServiceCollection AddValidator(this IServiceCollection services)
    {

        services.AddControllersWithViews(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>())
            .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

        return services;
    }

}