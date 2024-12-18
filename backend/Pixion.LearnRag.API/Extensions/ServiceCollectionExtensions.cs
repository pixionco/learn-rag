using FluentValidation;

namespace Pixion.LearnRag.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFluentValidatedOptions<TOptions, TOptionsValidator>(
        this IServiceCollection services,
        IConfigurationSection configurationSection
    ) where TOptions : class, new()
        where TOptionsValidator : AbstractValidator<TOptions>, new()
    {
        ArgumentNullException.ThrowIfNull(configurationSection);

        var options = configurationSection.Get<TOptions>();
        ArgumentNullException.ThrowIfNull(options);

        new TOptionsValidator().ValidateAndThrow(options);

        services.Configure<TOptions>(configurationSection);

        return services;
    }
}