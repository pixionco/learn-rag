import { useQuery, type UseQueryResult } from "@tanstack/react-query";
import {
  getAutoMergingEmbeddingOptions,
  getBasicEmbeddingOptions,
  getHierarchicalEmbeddingOptions,
  getHypotheticalQuestionEmbeddingOptions,
  getSentenceWindowEmbeddingOptions,
  type AutoMergingEmbeddingOptions,
  type BasicEmbeddingOptions,
  type HierarchicalEmbeddingOptions,
  type HypotheticalQuestionEmbeddingOptions,
  type SentenceWindowEmbeddingOptions,
} from "../api";
import useRAGOptions, { type RAGStrategy } from "../stores/rag-options-store";

export default function useEmbeddingOptions(strategy: RAGStrategy) {
  const documentId = useRAGOptions(state => state[strategy].documentId);

  const query = useQuery({
    queryKey: ["embedding-options", strategy, documentId],
    queryFn: () =>
      getEmbeddingOptionsFunction(strategy)?.({
        documentId: documentId!,
      }),
    enabled: !!documentId,
    refetchOnMount: false,
    refetchOnWindowFocus: false,
  });
  return query as UseQueryResult<
    | BasicEmbeddingOptions[]
    | AutoMergingEmbeddingOptions[]
    | HierarchicalEmbeddingOptions[]
    | SentenceWindowEmbeddingOptions[]
    | HypotheticalQuestionEmbeddingOptions[]
  >;
}

function getEmbeddingOptionsFunction(strategy: RAGStrategy) {
  switch (strategy) {
    case "basic":
      return getBasicEmbeddingOptions;
    case "sentence-window":
      return getSentenceWindowEmbeddingOptions;
    case "auto-merging":
      return getAutoMergingEmbeddingOptions;
    case "hierarchical":
      return getHierarchicalEmbeddingOptions;
    case "hypothetical-question":
      return getHypotheticalQuestionEmbeddingOptions;
    default:
      return undefined;
  }
}
