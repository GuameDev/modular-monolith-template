using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ModularMonolith.API.Swagger;
using ModularMonolith.Template.SharedKernel.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ModularMonolith.Template.API.DependencyInjection;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigurationOptions>();

        return services;
    }

    public static WebApplication UseSwaggerDocumentation(this WebApplication app)
    {
        var swaggerOptions = app.Services.GetRequiredService<IOptions<SwaggerOptions>>().Value;

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint($"/swagger/{swaggerOptions.Version}/swagger.json", $"{swaggerOptions.Title} {swaggerOptions.Version}");
            options.RoutePrefix = swaggerOptions.RoutePrefix;
        });

        return app;
    }
}
