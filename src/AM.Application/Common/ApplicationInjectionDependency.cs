using System.Diagnostics.CodeAnalysis;
using AM.Application.AutomowerFeature.Formatters;
using AM.Application.Common.Behaviors;
using AM.Application.Common.Mappings;
using AM.Application.Common.Validators;
using AM.Domain.DomainServices;
using AM.Domain.DomainServices.Abstractions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AM.Application.Common;

[ExcludeFromCodeCoverage]
public static class ApplicationInjectionDependency
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutomowerMappingProfile).Assembly);
        services.AddSingleton<IFileFormatter, TextFileFormatterRunner>();
        services.AddSingleton<ILawnDomainService, LawnDomainService>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(typeof(StartOperationCommandValidator).Assembly);
        return services;
    }
}