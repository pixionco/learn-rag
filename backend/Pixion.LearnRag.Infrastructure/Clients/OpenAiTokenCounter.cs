using Microsoft.DeepDev;
using Pixion.LearnRag.UseCases.Common.Clients;

namespace Pixion.LearnRag.Infrastructure.Clients;

public class OpenAiTokenCounter : ITokenCounter
{
    private readonly IReadOnlyCollection<string> _specialTokens = new List<string>();

    private readonly ITokenizer _tokenizer =
        Task.Run(async () => await TokenizerBuilder.CreateByEncoderNameAsync("cl100k_base")).Result;

    public int GetTokenCount(string text)
    {
        return _tokenizer.Encode(text, _specialTokens).Count;
    }
}