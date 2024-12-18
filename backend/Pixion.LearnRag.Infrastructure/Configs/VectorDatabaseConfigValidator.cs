using FluentValidation;

namespace Pixion.LearnRag.Infrastructure.Configs;

public class VectorDatabaseConfigValidator : AbstractValidator<VectorDatabaseConfig>
{
    public VectorDatabaseConfigValidator()
    {
        RuleFor(x => x.Schema)
            .NotEmpty()
            .WithMessage($"{nameof(VectorDatabaseConfig.Schema)} is required!");

        RuleFor(x => x.DatabaseConnection)
            .NotEmpty()
            .WithMessage($"{nameof(VectorDatabaseConfig.DatabaseConnection)} is required!");

        RuleFor(x => x.VectorSize)
            .NotEmpty()
            .WithMessage($"{nameof(VectorDatabaseConfig.VectorSize)} is required!");
    }
}