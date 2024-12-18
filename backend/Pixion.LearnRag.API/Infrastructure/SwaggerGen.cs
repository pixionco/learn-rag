using System.Reflection;
using Pixion.LearnRag.API.Configs;
using Pixion.LearnRag.API.Extensions;

namespace Pixion.LearnRag.API.Infrastructure;

public static class SwaggerGen
{
    public static bool EntryPointForSwaggerGenerationApplication(string[] args)
    {
        if (Assembly.GetEntryAssembly()?.GetName().Name != "dotnet-swagger") return false;

        var builder = WebApplication.CreateBuilder(args);

        var configSection = builder.Configuration.GetSection(EndpointsConfig.Key);
        if (configSection.Exists())
            builder.Services.Configure<EndpointsConfig>(configSection);
        else
            builder.Services.Configure<EndpointsConfig>(o => { o.DisableEmbedEndpoints = true; });

        builder.Services
            .ConfigureSwagger()
            .AddMediatR(
                cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); }
            );

        var app = builder.Build();
        app.MapEndpoints();
        app.UseSwagger();
        app.UseSwaggerUI();

        app.Run();
        return true;
    }
}