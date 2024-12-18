using Microsoft.SemanticKernel.Text;
using Pixion.LearnRag.UseCases.Common.Clients;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.Infrastructure.Services;

#pragma warning disable SKEXP0050

public class ChunkingService(ITokenCounter tokenCounter) : IChunkingService
{
    public IList<string> ChunkText(
        string text,
        int size,
        int overlap = 0
    )
    {
        return TextChunker.SplitPlainTextParagraphs(
            text.Split(["\n"], StringSplitOptions.None),
            size,
            (int)Math.Round(size * overlap / 100.0),
            tokenCounter: tokenCounter.GetTokenCount
        );
    }
}