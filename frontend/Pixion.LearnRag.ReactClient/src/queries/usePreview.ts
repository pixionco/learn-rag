import { useQuery, type UseQueryResult } from "@tanstack/react-query";
import {
  autoMergingPreview,
  basicPreview,
  hierarchicalPreview,
  hypotheticalQuestionPreview,
  sentenceWindowPreview,
} from "../api";
import useRAGOptions, { type RAGStrategy } from "../stores/rag-options-store";
import { type EmbeddingRecords } from "../types/rag";

export default function usePreview(strategy: RAGStrategy, limit = 10) {
  const documentId = useRAGOptions(state => state[strategy].documentId);
  const embeddingOptions = useRAGOptions(state => state[strategy].embeddingOptions);

  const query = useQuery({
    queryKey: ["preview", strategy, documentId, embeddingOptions, limit],
    queryFn: () =>
      // @ts-expect-error The right strategy will pass the right types
      getPreviewFunction(strategy)?.({
        documentId: documentId!,
        ...embeddingOptions,
        limit: limit,
      }),
    enabled: !!documentId && !!embeddingOptions && !!limit,
    refetchOnMount: false,
    refetchOnWindowFocus: false,
  });
  return query as UseQueryResult<EmbeddingRecords>;
}

function getPreviewFunction(strategy: RAGStrategy) {
  switch (strategy) {
    case "basic":
      return basicPreview;
    case "sentence-window":
      return sentenceWindowPreview;
    case "auto-merging":
      return autoMergingPreview;
    case "hierarchical":
      return hierarchicalPreview;
    case "hypothetical-question":
      return hypotheticalQuestionPreview;
    default:
      return undefined;
  }
}
