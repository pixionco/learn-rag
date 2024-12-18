using FluentValidation;

namespace Pixion.LearnRag.API.Extensions;

public static class CustomValidators
{
    public static IRuleBuilderOptions<T, ushort> IsValidChunkSize<T>(this IRuleBuilder<T, ushort> ruleBuilder)
    {
        return ruleBuilder
            .GreaterThan((ushort)0)
            .WithMessage("Chunk size must be greater than 0")
            .LessThanOrEqualTo((ushort)2048)
            .WithMessage("Chunk size must be less than or equal to 2048.");
    }

    public static IRuleBuilderOptions<T, ushort> IsValidChunkOverlap<T>(this IRuleBuilder<T, ushort> ruleBuilder)
    {
        return ruleBuilder
            .GreaterThanOrEqualTo((ushort)0)
            .WithMessage("Chunk overlap must be greater than or equal to 0")
            .LessThanOrEqualTo((ushort)50)
            .WithMessage("Chunk overlap must be less than or equal to 50.");
    }

    public static IRuleBuilderOptions<T, string> IsValidSearchQuery<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage("Search query can't be empty.");
    }
}