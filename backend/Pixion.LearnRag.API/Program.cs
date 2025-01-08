using Pixion.LearnRag.API;
using Pixion.LearnRag.API.Infrastructure;
using Serilog;

Log.Logger = SerilogHelper
    .BuildSerilogLoggerConfiguration()
    .CreateLogger();

try
{
    // post-build OpenAPI specification generation
    if (OpenApiStartup.OpenApiGenerationApplication(args)) return;

    Log.Information("Starting web host");
    BuildAndRun(args);
    Log.Information("Host stopped");
}
catch (Exception exception)
{
    Log.Fatal(exception, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

static void BuildAndRun(string[] args)
{
    var builder = WebApplication.CreateBuilder(args);
    builder.AddConfiguration();
    builder.ConfigureServices();

    var app = builder.Build();
    app.ConfigurePipeline();
    app.Run();
}