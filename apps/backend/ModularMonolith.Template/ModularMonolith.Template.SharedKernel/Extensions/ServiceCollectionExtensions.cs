using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Template.SharedKernel.Mediator.Senders;

namespace ModularMonolith.Template.SharedKernel.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSharedKernelServices(this IServiceCollection services)
        {
            services.AddScoped<ISender, Sender>();
            return services;
        }

    }
}
