namespace Pixion.LearnRag.UseCases.Common.Services;

public interface IChunkingService
{
    IList<string> ChunkText(string text, int chunkSize, int chunkOverlap);
}