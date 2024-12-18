namespace Pixion.LearnRag.Core.Entities.PromptTemplates;

public class QuestionPromptTemplate(
    string templateString,
    string numberOfQuestionsKey,
    string textKey
) : PromptTemplate(templateString)
{
    public string NumberOfQuestionsKey { get; } = numberOfQuestionsKey;
    public string TextKey { get; } = textKey;
}