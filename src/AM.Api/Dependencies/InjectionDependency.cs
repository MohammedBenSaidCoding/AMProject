using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using AM.Application.AutomowerFeature.Commands.StartOperationCommand;
using AM.Application.Common.Behaviors;
using AM.Application.Common.Validators;
using FluentValidation;
using MediatR;

namespace AM.Api.Dependencies;

[ExcludeFromCodeCoverage]
public static class InjectionDependency
{
    public static IServiceCollection AddGlobalDependencies(this IServiceCollection services)
    { 
        services.AddMediatR(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(StartOperationCommandHandler)) ?? throw new InvalidOperationException());
      

       return services;
    }
}