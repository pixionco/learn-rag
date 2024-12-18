using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Pixion.LearnRag.UseCases.Common.Behaviours;
using Pixion.LearnRag.UseCases.Common.Services;
using Pixion.LearnRag.UseCases.Common.Services.Implementations;

namespace Pixion.LearnRag.UseCases;

public static class DependencyInjection
{
    public static IServiceCollection AddUseCasesServices(
        this IServiceCollection services
    )
    {
        // MediatR
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(
            cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            }
        );

        // Strategy services
        services.AddSingleton<IBasicStrategyService, BasicStrategyService>();
        services.AddSingleton<ISentenceWindowStrategyService, SentenceWindowStrategyService>();
        services.AddSingleton<IAutoMergingStrategyService, AutoMergingStrategyService>();
        services.AddSingleton<IHierarchicalStrategyService, HierarchicalStrategyService>();
        services.AddSingleton<IHypotheticalQuestionStrategyService, HypotheticalQuestionStrategyService>();

        return services;
    }
}