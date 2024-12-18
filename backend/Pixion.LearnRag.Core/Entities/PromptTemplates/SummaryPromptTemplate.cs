namespace Pixion.LearnRag.Core.Entities.PromptTemplates;

public class SummaryPromptTemplate(
    string templateString,
    string textKey
) : PromptTemplate(templateString)
{
    public string TextKey { get; } = textKey;
}