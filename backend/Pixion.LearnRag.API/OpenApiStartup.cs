using Pixion.LearnRag.API.Configs;
using Pixion.LearnRag.API.Extensions;
using System.Reflection;
using System.Text.Json;

namespace Pixion.LearnRag.API;

public static class OpenApiStartup
{
    // minimal setup needed for OpenAPI specification to be generated
    // skips database-related services etc.
    public static bool OpenApiGenerationApplication(string[] args)
    {
        if (Assembly.GetEntryAssembly()?.GetName().Name != "GetDocument.Insider") return false;

        var builder = WebApplication.CreateBuilder(args);

        var configSection = builder.Configuration.GetSection(EndpointsConfig.Key);
        if (configSection.Exists())
            builder.Services.Configure<EndpointsConfig>(configSection);
        else
            builder.Services.Configure<EndpointsConfig>(o => { o.DisableEmbedEndpoints = true; });

        builder.Services
            .AddOpenApi(o =>
            {
                o.AddOperationTransformer((operation, context, cancellationToken) =>
                {
                    if (operation.Parameters != null)
                        foreach (var parameter in operation.Parameters)
                            parameter.Name = JsonNamingPolicy.CamelCase.ConvertName(parameter.Name);
                    return Task.CompletedTask;
                });
            })
            .AddMediatR(
                cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); }
            );

        var app = builder.Build();
        app.MapEndpoints();
        app.MapOpenApi();
        app.Run();

        return true;
    }
}