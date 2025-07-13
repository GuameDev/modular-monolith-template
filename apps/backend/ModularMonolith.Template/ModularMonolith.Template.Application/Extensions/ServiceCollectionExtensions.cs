using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Template.SharedKernel.Extensions;

namespace ModularMonolith.Template.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddRequestHandlersFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
            return services;
        }
    }
}