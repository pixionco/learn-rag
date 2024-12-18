import { create } from "zustand";
import {
  type AutoMergingEmbeddingOptions,
  type BasicEmbeddingOptions,
  type HierarchicalEmbeddingOptions,
  type HypotheticalQuestionEmbeddingOptions,
  type SentenceWindowEmbeddingOptions,
} from "../api";
import { type Require } from "../types/helpers";
import {
  type AutoMergingRetrievalOptions,
  type BasicRetrievalOptions,
  type HierarchicalRetrievalOptions,
  type HypotheticalQuestionRetrievalOptions,
  type SentenceWindowRetrievalOptions,
} from "../types/rag";

export type RAGOptionsState = {
  basic: {
    documentId?: string;
    userQuery?: string;
    embeddingOptions?: BasicEmbeddingOptions;
    retrievalOptions?: BasicRetrievalOptions;
  };
  "sentence-window": {
    documentId?: string;
    userQuery?: string;
    embeddingOptions?: SentenceWindowEmbeddingOptions;
    retrievalOptions?: SentenceWindowRetrievalOptions;
  };
  "auto-merging": {
    documentId?: string;
    userQuery?: string;
    embeddingOptions?: AutoMergingEmbeddingOptions;
    retrievalOptions?: AutoMergingRetrievalOptions;
  };
  hierarchical: {
    documentId?: string;
    userQuery?: string;
    embeddingOptions?: HierarchicalEmbeddingOptions;
    retrievalOptions?: HierarchicalRetrievalOptions;
  };
  "hypothetical-question": {
    documentId?: string;
    userQuery?: string;
    embeddingOptions?: HypotheticalQuestionEmbeddingOptions;
    retrievalOptions?: HypotheticalQuestionRetrievalOptions;
  };
};
export type RAGStrategy = keyof RAGOptionsState;

type RAGOptionActions = {
  setDocumentId: (
    strategy: RAGStrategy,
    document: NonNullable<RAGOptionsState[RAGStrategy]["documentId"]>
  ) => void;
  setUserQuery: (
    strategy: RAGStrategy,
    userQuery: NonNullable<RAGOptionsState[RAGStrategy]["userQuery"]>
  ) => void;
  setEmbeddingOptions: (
    strategy: RAGStrategy,
    embeddingOptions: NonNullable<RAGOptionsState[RAGStrategy]["embeddingOptions"]>
  ) => void;
  setRetrievalOptions: (
    strategy: RAGStrategy,
    retrievalOptions: NonNullable<RAGOptionsState[RAGStrategy]["retrievalOptions"]>
  ) => void;
};

type RAGOptionsStore = RAGOptionsState & RAGOptionActions;

const useRAGOptions = create<RAGOptionsStore>(set => ({
  "auto-merging": {},
  "hypothetical-question": {},
  "sentence-window": {},
  basic: {},
  hierarchical: {},

  setDocumentId: (strategy, documentId) =>
    set(state => ({
      [strategy]: { ...state[strategy], documentId } satisfies Require<
        RAGOptionsState[typeof strategy],
        "documentId"
      >,
    })),
  setUserQuery: (strategy, userQuery) =>
    set(state => ({
      [strategy]: {
        ...state[strategy],
        userQuery,
      } satisfies Require<RAGOptionsState[typeof strategy], "userQuery">,
    })),
  setEmbeddingOptions: (strategy, embeddingOptions) =>
    set(state => ({
      [strategy]: {
        ...state[strategy],
        embeddingOptions,
      } satisfies Require<RAGOptionsState[typeof strategy], "embeddingOptions">,
    })),
  setRetrievalOptions: (strategy, retrievalOptions) =>
    set(state => ({
      [strategy]: {
        ...state[strategy],
        retrievalOptions,
      } satisfies Require<RAGOptionsState[typeof strategy], "retrievalOptions">,
    })),
}));

export default useRAGOptions;
