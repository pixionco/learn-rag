using Pixion.LearnRag.Core.Entities.PromptTemplates;

namespace Pixion.LearnRag.UseCases.Common.Clients;

public interface IPromptTemplateClient
{
    QuestionPromptTemplate GetQuestionPromptTemplate();
    SummaryPromptTemplate GetSummaryPromptTemplate();
    AnswerPromptTemplate GetAnswerPromptTemplate();
}