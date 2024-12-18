namespace Pixion.LearnRag.UseCases.Common.Clients;

public interface ITokenCounter
{
    int GetTokenCount(string text);
}