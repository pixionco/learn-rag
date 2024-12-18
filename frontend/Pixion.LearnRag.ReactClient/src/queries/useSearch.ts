import { useQuery, type UseQueryResult } from "@tanstack/react-query";
import {
  autoMergingSearch,
  basicSearch,
  hierarchicalSearch,
  hypotheticalQuestionSearch,
  sentenceWindowSearch,
} from "../api/";
import useRAGOptions, { type RAGStrategy } from "../stores/rag-options-store";
import { type SearchResults } from "../types/rag";

export default function useSearch(strategy: RAGStrategy, userQuery: string) {
  const embeddingOptions = useRAGOptions(state => state[strategy].embeddingOptions);
  const retrievalOptions = useRAGOptions(state => state[strategy].retrievalOptions);

  const query = useQuery({
    queryKey: ["search", strategy, userQuery, embeddingOptions, retrievalOptions],
    queryFn: () =>
      // @ts-expect-error The right strategy will pass the right types
      getSearchFunction(strategy)?.({
        query: userQuery,
        ...embeddingOptions,
        ...retrievalOptions,
      }),
    enabled: !!userQuery && !!embeddingOptions && !!retrievalOptions,
    refetchOnMount: false,
    refetchOnWindowFocus: false,
  });
  return query as UseQueryResult<SearchResults>;
}

function getSearchFunction(strategy: RAGStrategy) {
  switch (strategy) {
    case "basic":
      return basicSearch;
    case "sentence-window":
      return sentenceWindowSearch;
    case "auto-merging":
      return autoMergingSearch;
    case "hierarchical":
      return hierarchicalSearch;
    case "hypothetical-question":
      return hypotheticalQuestionSearch;
    default:
      return undefined;
  }
}
