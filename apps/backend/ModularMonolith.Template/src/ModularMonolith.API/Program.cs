using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModularMonolith.API.Extensions;
using ModularMonolith.Template.API.DependencyInjection;
using ModularMonolith.Template.Application.Extensions;
using ModularMonolith.Template.SharedKernel.Extensions;

namespace ModularMonolith.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddSharedKernelServices()
            .AddApplicationServices()
            .AddApiServices(builder.Configuration);

        builder.Services.AddControllers();
        builder.Services.AddAppOptionsFromAssembly(builder.Configuration, [typeof(Template.SharedKernel.Configuration.IAppOption).Assembly]);
        builder.Services.AddSwaggerDocumentation();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerDocumentation();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
