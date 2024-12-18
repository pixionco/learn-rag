import { type RAGStrategy } from "../stores/rag-options-store";

export function checkRetrievalOptionsValid(
  strategy: RAGStrategy,
  retrievalOptions?: Record<string, unknown>
) {
  if (!retrievalOptions) return false;
  switch (strategy) {
    case "basic":
      return !!retrievalOptions.limit;
    case "sentence-window":
      return !!retrievalOptions.limit && !!retrievalOptions.range;
    case "auto-merging":
      return !!retrievalOptions.limit && !!retrievalOptions.childParentPrevalenceFactor;
    case "hierarchical":
      return !!retrievalOptions.limit && !!retrievalOptions.childLimit;
    case "hypothetical-question":
      return !!retrievalOptions.limit;
    default:
      console.error(
        `${strategy} was passed into areRetrievalOptionsValid instead of a valid strategy`
      );

      return false;
  }
}

export function checkEmbeddingOptionsValid(
  strategy: RAGStrategy,
  embeddingOptions?: Record<string, unknown>
) {
  if (!embeddingOptions) return false;
  switch (strategy) {
    case "basic":
      return embeddingOptions.chunkSize != null && embeddingOptions.chunkOverlap != null;
    case "sentence-window":
      return embeddingOptions.chunkSize != null;
    case "auto-merging":
      return (
        embeddingOptions.chunkSize != null &&
        embeddingOptions.chunkOverlap != null &&
        embeddingOptions.childChunkSize != null &&
        embeddingOptions.childChunkOverlap != null
      );
    case "hierarchical":
      return (
        embeddingOptions.chunkSize != null &&
        embeddingOptions.chunkOverlap != null &&
        embeddingOptions.childChunkSize != null &&
        embeddingOptions.childChunkOverlap != null
      );
    case "hypothetical-question":
      return (
        embeddingOptions.chunkSize != null &&
        embeddingOptions.chunkOverlap != null &&
        embeddingOptions.numberOfQuestions != null
      );
    default:
      console.error(
        `${strategy} was passed into areEmbeddingOptionsValid instead of a valid strategy`
      );

      return false;
  }
}
