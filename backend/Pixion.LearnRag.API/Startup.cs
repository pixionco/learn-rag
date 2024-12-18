using Microsoft.OpenApi.Models;
using Pixion.LearnRag.API.Configs;
using Pixion.LearnRag.API.Extensions;
using Pixion.LearnRag.API.Infrastructure;
using Pixion.LearnRag.API.Infrastructure.SwashbuckleFilters;
using Pixion.LearnRag.Infrastructure;
using Pixion.LearnRag.Infrastructure.Configs;
using Pixion.LearnRag.Infrastructure.Extensions;
using Pixion.LearnRag.UseCases;

namespace Pixion.LearnRag.API;

public static class Startup
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var azureOpenAiChatConfig = configuration.GetSection(AzureOpenAiChatConfig.Key);
        var azureOpenAiEmbeddingConfig = configuration.GetSection(AzureOpenAiEmbeddingConfig.Key);
        var vectorDatabaseConfig = configuration.GetSection(VectorDatabaseConfig.Key);
        var documentDatabaseConfig = configuration.GetSection(DocumentDatabaseConfig.Key);
        var mockConfig = configuration.GetSection(MockConfig.Key);
        var seedConfig = configuration.GetSection(SeedConfig.Key);
        var endpointsConfig = configuration.GetSection(EndpointsConfig.Key);
        services
            .AddFluentValidatedOptions<AzureOpenAiChatConfig, AzureOpenAiChatConfigValidator>(azureOpenAiChatConfig)
            .AddFluentValidatedOptions<AzureOpenAiEmbeddingConfig, AzureOpenAiEmbeddingConfigValidator>(
                azureOpenAiEmbeddingConfig
            )
            .AddFluentValidatedOptions<VectorDatabaseConfig, VectorDatabaseConfigValidator>(vectorDatabaseConfig)
            .AddFluentValidatedOptions<DocumentDatabaseConfig, DocumentDatabaseConfigValidator>(documentDatabaseConfig)
            .AddFluentValidatedOptions<MockConfig, MockConfigValidator>(mockConfig)
            .AddFluentValidatedOptions<SeedConfig, SeedConfigValidator>(seedConfig)
            .AddFluentValidatedOptions<EndpointsConfig, EndpointsConfigValidator>(endpointsConfig);

        return services;
    }

    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(
                o =>
                {
                    o.SwaggerDoc("v1", new OpenApiInfo { Title = "Learn RAG API", Version = "v1" });
                    o.OperationFilter<CamelCaseParameterFilter>();
                    // o.DescribeAllParametersInCamelCase();
                    o.SchemaFilter<ReadOnlyMemoryFloatSchemaFilter>();
                }
            );
        return services;
    }

    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Swagger
        services.ConfigureSwagger();

        // Services
        services
            .AddUseCasesServices()
            .AddInfrastructureServices(
                configuration.GetSection(MockConfig.Key).Get<MockConfig>()!,
                configuration.GetSection(SeedConfig.Key).Get<SeedConfig>()!
            );

        // Global exception handler
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        // Seeding
        services
            .SeedDatabase()
            .Wait();

        return services;
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
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseExceptionHandler();

        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.MapFallbackToFile("/index.html");

        return app;
    }
}