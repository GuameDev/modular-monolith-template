using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Template.SharedKernel.Configuration;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace ModularMonolith.API.Extensions
{
    public static class OptionsExtensions
    {
        /// <summary>
        /// Registers application option types from the specified assemblies into the service collection.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to which the options will be added.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> instance used to bind configuration sections to the option types.</param>
        /// <param name="assemblies">An array of <see cref="Assembly"/> objects to scan for types implementing <see cref="IAppOption"/>.</param>
        public static IServiceCollection AddAppOptionsFromAssembly(this IServiceCollection services, IConfiguration configuration, Assembly[] assemblies)
        {
            var optionTypes = assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type =>
                    !type.IsAbstract &&
                    !type.IsInterface &&
                    typeof(IAppOption).IsAssignableFrom(type))
                .ToList();

            foreach (var optionType in optionTypes)
            {
                var sectionNameProperty = optionType
                    .GetProperty(nameof(IAppOption.SectionName), BindingFlags.Public | BindingFlags.Static);

                if (sectionNameProperty == null)
                    throw new InvalidOperationException($"Missing static 'SectionName' on {optionType.Name}");

                var sectionName = sectionNameProperty.GetValue(null)?.ToString();

                if (string.IsNullOrWhiteSpace(sectionName))
                    throw new InvalidOperationException($"Empty 'SectionName' on {optionType.Name}");

                var method = typeof(OptionsExtensions)
                    .GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .FirstOrDefault(m => m.Name == nameof(AddAppOptions) && m.IsGenericMethod)?
                    .MakeGenericMethod(optionType)
                    ?? throw new InvalidOperationException("Could not find generic AddAppOptions method.");

                method.Invoke(null, [services, configuration]);
            }

            return services;
        }

        public static IServiceCollection AddAppOptions<T>(this IServiceCollection services, IConfiguration configration) where T : class, IAppOption
        {
            // Load and bind configuration
            var section = configration.GetSection(T.SectionName);
            services.Configure<T>(section);

            // Validate on startup using data annotations
            var instance = section.Get<T>();

            if (instance is null) return services;

            Validator.ValidateObject(instance, new ValidationContext(instance), validateAllProperties: true);

            return services;
        }
    }
}