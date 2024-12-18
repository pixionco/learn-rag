using System.Diagnostics.CodeAnalysis;

namespace Pixion.LearnRag.UseCases.Common.Models;

public class Optional<T>(bool ok, T? value, Exception? exception)
{
    public Optional(Exception? exception) : this(false, default, exception)
    {
    }

    public Optional(T? value) : this(value != null, value, null)
    {
    }


    [MemberNotNullWhen(true, nameof(Value))]
    [MemberNotNullWhen(false, nameof(Exception))]
    public bool Ok { get; } = ok;

    public T? Value { init; get; } = value;
    public Exception? Exception { get; } = exception;
}