namespace Pixion.LearnRag.Core.Entities.PromptTemplates;

public abstract class PromptTemplate(string templateString)
{
    public string TemplateString { get; } = templateString;
}