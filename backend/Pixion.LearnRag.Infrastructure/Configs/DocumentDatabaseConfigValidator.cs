using FluentValidation;

namespace Pixion.LearnRag.Infrastructure.Configs;

public class DocumentDatabaseConfigValidator : AbstractValidator<DocumentDatabaseConfig>
{
    public DocumentDatabaseConfigValidator()
    {
        RuleFor(x => x.DatabaseConnection)
            .NotEmpty()
            .WithMessage($"{nameof(DocumentDatabaseConfig.DatabaseConnection)} is required!");
    }
}