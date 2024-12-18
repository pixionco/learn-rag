namespace Pixion.LearnRag.Core.Exceptions;

public abstract class LearnRAGException : Exception
{
    public readonly string Title;

    protected LearnRAGException(string title, string message)
        : base(message)
    {
        Title = title;
    }

    // Inner Exception is used only for logging purposes 
    protected LearnRAGException(string title, string message, Exception? inner)
        : base(message, inner)
    {
        Title = title;
    }
}