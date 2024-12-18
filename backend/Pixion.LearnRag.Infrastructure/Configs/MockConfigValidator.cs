using FluentValidation;

namespace Pixion.LearnRag.Infrastructure.Configs;

public class MockConfigValidator : AbstractValidator<MockConfig>
{
    public MockConfigValidator()
    {
        RuleFor(x => x.MockAiModels)
            .NotNull()
            .WithMessage($"{nameof(MockConfig.MockAiModels)} is required!");
        RuleFor(x => x.UseFailingMocks)
            .NotNull()
            .WithMessage($"{nameof(MockConfig.UseFailingMocks)} is required!");
    }
}