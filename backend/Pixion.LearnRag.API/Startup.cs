using Pixion.LearnRag.API.Configs;
using Pixion.LearnRag.API.Extensions;
using Pixion.LearnRag.API.Infrastructure;
using Pixion.LearnRag.Infrastructure;
using Pixion.LearnRag.Infrastructure.Configs;
using Pixion.LearnRag.Infrastructure.Extensions;
using Pixion.LearnRag.UseCases;
using Scalar.AspNetCore;
using Serilog;
using System.Text.Json;

namespace Pixion.LearnRag.API;

public static class Startup
{
    public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
    {
        var azureOpenAiChatSection = builder.Configuration.GetSection(AzureOpenAiChatConfig.Key);
        var azureOpenAiEmbeddingSection = builder.Configuration.GetSection(AzureOpenAiEmbeddingConfig.Key);
        var vectorDatabaseSection = builder.Configuration.GetSection(VectorDatabaseConfig.Key);
        var documentDatabaseSection = builder.Configuration.GetSection(DocumentDatabaseConfig.Key);
        var mockSection = builder.Configuration.GetSection(MockConfig.Key);
        var seedSection = builder.Configuration.GetSection(SeedConfig.Key);
        var endpointsSection = builder.Configuration.GetSection(EndpointsConfig.Key);

        // if we're mocking AI models no need for AI model configuration
        var mockConfig = mockSection.Get<MockConfig>()!;
        if (!mockConfig.MockAiModels)
        {
            builder.Services
                .AddFluentValidatedOptions<AzureOpenAiChatConfig, AzureOpenAiChatConfigValidator>(azureOpenAiChatSection)
                .AddFluentValidatedOptions<AzureOpenAiEmbeddingConfig, AzureOpenAiEmbeddingConfigValidator>(
                azureOpenAiEmbeddingSection
            );
        }

        builder.Services
            .AddFluentValidatedOptions<VectorDatabaseConfig, VectorDatabaseConfigValidator>(vectorDatabaseSection)
            .AddFluentValidatedOptions<DocumentDatabaseConfig, DocumentDatabaseConfigValidator>(documentDatabaseSection)
            .AddFluentValidatedOptions<MockConfig, MockConfigValidator>(mockSection)
            .AddFluentValidatedOptions<SeedConfig, SeedConfigValidator>(seedSection)
            .AddFluentValidatedOptions<EndpointsConfig, EndpointsConfigValidator>(endpointsSection);

        return builder;
    }

    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        // Serilog
        builder.Services.AddSerilog();

        // OpenAPI
        builder.Services.AddOpenApi(o =>
        {
            o.AddOperationTransformer((operation, context, cancellationToken) =>
            {
                if (operation.Parameters != null)
                    foreach (var parameter in operation.Parameters)
                        parameter.Name = JsonNamingPolicy.CamelCase.ConvertName(parameter.Name);
                return Task.CompletedTask;
            });
        });

        // Services
        builder.Services
            .AddUseCasesServices()
            .AddInfrastructureServices(
                builder.Configuration.GetSection(MockConfig.Key).Get<MockConfig>()!,
                builder.Configuration.GetSection(SeedConfig.Key).Get<SeedConfig>()!
            );

        // Global exception handler
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        // Seeding
        builder.Services
            .SeedDatabase()
            .Wait();

        return builder;
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseHttpsRedirection();

        if (!app.Environment.IsProduction())
            app.UseDeveloperExceptionPage();
        else
            app.UseHsts();

        app.MapEndpoints();
        if (!app.Environment.IsProduction())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseExceptionHandler();

        // fallback to client
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.MapFallbackToFile("/index.html");

        return app;
    }
}