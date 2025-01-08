using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Pixion.LearnRag.Infrastructure.Clients;
using Pixion.LearnRag.Infrastructure.Clients.MockClients;
using Pixion.LearnRag.Infrastructure.Configs;
using Pixion.LearnRag.Infrastructure.Repositories;
using Pixion.LearnRag.Infrastructure.Seeders;
using Pixion.LearnRag.Infrastructure.Services;
using Pixion.LearnRag.Infrastructure.Services.MockServices;
using Pixion.LearnRag.UseCases.Common.Clients;
using Pixion.LearnRag.UseCases.Common.Repositories;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        MockConfig mockConfig,
        SeedConfig seedConfig
    )
    {
        // Repositories
        services.AddSingleton<IBasicStrategyRepository, BasicStrategyRepository>();
        services.AddSingleton<ISentenceWindowStrategyRepository, SentenceWindowStrategyRepository>();
        services.AddSingleton<IAutoMergingStrategyRepository, AutoMergingStrategyRepository>();
        services.AddSingleton<IHierarchicalStrategyRepository, HierarchicalStrategyRepository>();
        services.AddSingleton<IHypotheticalQuestionStrategyRepository, HypotheticalQuestionStrategyRepository>();
        services.AddSingleton<IDocumentRepository, DocumentRepository>();

        // Services
        if (mockConfig.MockAiModels)
        {
            if (mockConfig.UseFailingMocks)
            {
                services.AddSingleton<IEmbeddingClient, AzureOpenAiEmbeddingClientFailingMock>();
                services.AddSingleton<ISummaryGenerationService, SummaryGenerationServiceFailingMock>();
                services.AddSingleton<IQuestionGenerationService, QuestionGenerationServiceFailingMock>();
                services.AddSingleton<IAnswerGenerationService, AnswerGenerationServiceFailingMock>();
            }
            else
            {
                services.AddSingleton<IEmbeddingClient, AzureOpenAiEmbeddingClientMock>();
                services.AddSingleton<ISummaryGenerationService, SummaryGenerationServiceMock>();
                services.AddSingleton<IQuestionGenerationService, QuestionGenerationServiceMock>();
                services.AddSingleton<IAnswerGenerationService, AnswerGenerationServiceMock>();
            }
        }
        else
        {
            services.AddSingleton<IEmbeddingClient, AzureOpenAiEmbeddingClient>();
            services.AddSingleton<ISummaryGenerationService, SummaryGenerationService>();
            services.AddSingleton<IQuestionGenerationService, QuestionGenerationService>();
            services.AddSingleton<IAnswerGenerationService, AnswerGenerationService>();

            // Semantic Kernel 
            using var servicesProvider = services.BuildServiceProvider();
            var azureOpenAiChatConfig = servicesProvider.GetService<IOptions<AzureOpenAiChatConfig>>()
                ?.Value;
            ArgumentNullException.ThrowIfNull(azureOpenAiChatConfig);
            services
                .AddKernel()
                .AddAzureOpenAIChatCompletion(
                    azureOpenAiChatConfig.DeploymentName,
                    azureOpenAiChatConfig.Endpoint,
                    azureOpenAiChatConfig.ApiKey
                );
        }
        services.AddSingleton<IChunkingService, ChunkingService>();

        // Postgres Seeders
        // NOTE: order of seeders matters
        services.AddScoped<ISeeder, StrategyTableSeeder>();
        services.AddScoped<ISeeder, DocumentTableSeeder>();
        if (seedConfig.EmbedDocuments) services.AddScoped<ISeeder, DocumentEmbeddingSeeder>();

        // Clients
        services.AddTransient<ITokenCounter, OpenAiTokenCounter>();
        services.AddTransient<IPromptTemplateClient, PromptTemplateClient>();

        return services;
    }
}