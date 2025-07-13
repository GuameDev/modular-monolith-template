using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ModularMonolith.API.Filters;
using ModularMonolith.API.ParameterTransformers;
using ModularMonolith.Template.SharedKernel.Configuration;
using System.Text;
using System.Text.Json.Serialization;

namespace ModularMonolith.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseRouteTransformer()));

                options.Filters.Add<LoggingActionFilter>();
                options.Filters.Add<ProblemDetailsExceptionFilter>();
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                // https://learn.microsoft.com/es-es/dotnet/core/compatibility/aspnet-core/7.0/api-controller-action-parameters-di
                options.DisableImplicitFromServicesParameters = true;

            }).AddApplicationPart(typeof(ServiceCollectionExtensions).Assembly);

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddProblemDetails();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                JwtOptions jwtSettings = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>()!;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.AccessToken.Issuer,
                    ValidAudience = jwtSettings.AccessToken.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.AccessToken.Key))
                };
            });
            services.AddAuthorization();
            services.AddHttpContextAccessor();

            services.AddEndpointsApiExplorer();

            return services;
        }
    }
}
