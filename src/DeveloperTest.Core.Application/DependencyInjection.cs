using DeveloperTest.Core.Application.Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using DeveloperTest.Core.Application.Validator;

namespace DeveloperTest.Core.Application;

public static class DependencyInjection
{
    private struct _Anchor
    {

    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(_Anchor).Assembly);

        services.AddValidatorsFromAssemblyContaining<CreateEmployeeCommandValidator>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

       return services;
    }
}
