namespace Pixion.LearnRag.Core.Entities.PromptTemplates;

public class AnswerPromptTemplate(
    string templateString,
    string question,
    string context
) : PromptTemplate(templateString)
{
    public string Context { get; } = context;
    public string Question { get; } = question;
}