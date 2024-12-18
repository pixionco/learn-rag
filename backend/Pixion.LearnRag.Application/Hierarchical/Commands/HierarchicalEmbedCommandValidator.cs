using FluentValidation;

namespace Pixion.LearnRag.UseCases.Hierarchical.Commands;

public class HierarchicalEmbedCommandValidator : AbstractValidator<HierarchicalEmbedCommand>
{
    public HierarchicalEmbedCommandValidator()
    {
        RuleFor(x => x.DocumentId)
            .NotEmpty()
            .WithMessage($"{nameof(HierarchicalEmbedCommand.DocumentId)} can't be null, empty, or a whitespace.");

        RuleFor(x => x.EmbeddingOptions).SetValidator(new HierarchicalEmbeddingOptionsValidator());
    }
}