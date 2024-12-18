using System.Reflection;
using Microsoft.Extensions.Options;
using Pixion.LearnRag.API.Configs;
using Pixion.LearnRag.API.Infrastructure;

namespace Pixion.LearnRag.API.Extensions;

public static class WebApplicationExtensions
{
    public static RouteGroupBuilder MapGroup(this WebApplication app, string path, string tag)
    {
        return app
            .MapGroup($"/api/{path}")
            .WithTags(tag)
            .WithOpenApi();
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var endpointsConfig = app.Services.GetRequiredService<IOptions<EndpointsConfig>>().Value;
        var endpointGroupType = typeof(EndpointGroupBase);
        var assembly = Assembly.GetExecutingAssembly();

        var endpointGroupTypes = assembly.GetExportedTypes()
            .Where(t => t.IsSubclassOf(endpointGroupType));

        foreach (var type in endpointGroupTypes)
            if (Activator.CreateInstance(type) is EndpointGroupBase instance)
                instance.Map(app, endpointsConfig);

        return app;
    }
}