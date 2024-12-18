using Pixion.LearnRag.API;
using Pixion.LearnRag.API.Infrastructure;
using Serilog;

if (SwaggerGen.EntryPointForSwaggerGenerationApplication(args))
    return;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));
builder.Services
    .AddConfiguration(builder.Configuration)
    .ConfigureServices(builder.Configuration);

var app = builder.Build();

app.ConfigurePipeline();
app.Run();