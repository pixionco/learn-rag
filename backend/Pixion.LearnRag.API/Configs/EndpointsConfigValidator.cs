using FluentValidation;

namespace Pixion.LearnRag.API.Configs;

public class EndpointsConfigValidator : AbstractValidator<EndpointsConfig>
{
    public EndpointsConfigValidator()
    {
        RuleFor(x => x.DisableEmbedEndpoints)
            .NotNull()
            .WithMessage($"{nameof(EndpointsConfig.DisableEmbedEndpoints)} is required!");
    }
}