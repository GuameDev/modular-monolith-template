using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ModularMonolith.Template.SharedKernel.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

namespace ModularMonolith.API.Swagger
{
    public class SwaggerConfigurationOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly SwaggerOptions _options;

        public SwaggerConfigurationOptions(IOptions<SwaggerOptions> options)
        {
            _options = options.Value;
        }

        public void Configure(SwaggerGenOptions options)
        {
            options.SwaggerDoc(_options.Version, new OpenApiInfo
            {
                Title = _options.Title,
                Version = _options.Version,
                Description = _options.Description,
                Contact = new OpenApiContact
                {
                    Name = _options.ContactName,
                    Email = _options.ContactEmail
                }
            });

            options.AddSecurityDefinition(_options.AuthScheme, new OpenApiSecurityScheme
            {
                Name = _options.AuthHeader,
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = _options.AuthScheme,
                BearerFormat = "JWT",
                Description = "Enter your JWT token like this: **Bearer <token>**"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = _options.AuthScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                },
                Array.Empty<string>()
            }
        });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            options.OrderActionsBy(apiDesc => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");
        }
    }
}