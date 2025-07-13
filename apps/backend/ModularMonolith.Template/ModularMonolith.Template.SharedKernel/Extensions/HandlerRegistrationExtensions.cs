using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Template.SharedKernel.Mediator.Handlers;
using System.Reflection;

namespace ModularMonolith.Template.SharedKernel.Extensions;

public static class HandlerRegistrationExtensions
{
    public static IServiceCollection AddRequestHandlersFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        var handlerInterfaceType = typeof(IRequestHandler<,>);

        var implementations = assembly
            .GetTypes()
            .Where(t => t is { IsAbstract: false, IsClass: true } &&
                        t.GetInterfaces()
                         .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType));

        foreach (var implementation in implementations)
        {
            var interfaces = implementation.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType);

            foreach (var handlerInterface in interfaces)
            {
                services.AddScoped(handlerInterface, implementation);
            }
        }

        return services;
    }
}
