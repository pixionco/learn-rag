using FluentValidation;

namespace Pixion.LearnRag.Infrastructure.Configs;

public class SeedConfigValidator : AbstractValidator<SeedConfig>
{
    public SeedConfigValidator()
    {
        RuleFor(x => x.EmbedDocuments)
            .NotNull()
            .WithMessage($"{nameof(SeedConfig.EmbedDocuments)} is required!");
    }
}