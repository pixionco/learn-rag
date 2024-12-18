import {
  type AutoMergingEmbeddingOptions,
  type AutoMergingMetadataEmbeddingRecord,
  type AutoMergingMetadataSearchResult,
  type AutoMergingSearchParams,
  type BasicEmbeddingOptions,
  type BasicMetadataEmbeddingRecord,
  type BasicMetadataSearchResult,
  type BasicSearchParams,
  type HierarchicalEmbeddingOptions,
  type HierarchicalMetadataEmbeddingRecord,
  type HierarchicalMetadataSearchResult,
  type HierarchicalSearchParams,
  type HypotheticalQuestionEmbeddingOptions,
  type HypotheticalQuestionMetadataEmbeddingRecord,
  type HypotheticalQuestionMetadataSearchResult,
  type HypotheticalQuestionSearchParams,
  type SentenceWindowEmbeddingOptions,
  type SentenceWindowMetadataEmbeddingRecord,
  type SentenceWindowMetadataSearchResult,
  type SentenceWindowSearchParams,
} from "../api";

export type EmbeddingOptions =
  | BasicEmbeddingOptions
  | AutoMergingEmbeddingOptions
  | HierarchicalEmbeddingOptions
  | SentenceWindowEmbeddingOptions
  | HypotheticalQuestionEmbeddingOptions;

export type ExtractRetrievalOptions<SP, EO> = Omit<SP, keyof EO | "query">;
export type BasicRetrievalOptions = ExtractRetrievalOptions<
  BasicSearchParams,
  BasicEmbeddingOptions
>;
export type SentenceWindowRetrievalOptions = ExtractRetrievalOptions<
  SentenceWindowSearchParams,
  SentenceWindowEmbeddingOptions
>;
export type AutoMergingRetrievalOptions = ExtractRetrievalOptions<
  AutoMergingSearchParams,
  AutoMergingEmbeddingOptions
>;
export type HierarchicalRetrievalOptions = ExtractRetrievalOptions<
  HierarchicalSearchParams,
  HierarchicalEmbeddingOptions
>;
export type HypotheticalQuestionRetrievalOptions = ExtractRetrievalOptions<
  HypotheticalQuestionSearchParams,
  HypotheticalQuestionEmbeddingOptions
>;

export type RetrievalOptions =
  | BasicRetrievalOptions
  | AutoMergingRetrievalOptions
  | HierarchicalRetrievalOptions
  | SentenceWindowRetrievalOptions
  | HypotheticalQuestionRetrievalOptions;

export type SearchResults =
  | BasicMetadataSearchResult[]
  | SentenceWindowMetadataSearchResult[]
  | AutoMergingMetadataSearchResult[]
  | HierarchicalMetadataSearchResult[]
  | HypotheticalQuestionMetadataSearchResult[];

export type EmbeddingRecords =
  | BasicMetadataEmbeddingRecord[]
  | SentenceWindowMetadataEmbeddingRecord[]
  | AutoMergingMetadataEmbeddingRecord[]
  | HierarchicalMetadataEmbeddingRecord[]
  | HypotheticalQuestionMetadataEmbeddingRecord[];

export type LearnRAGError = {
  title: string;
  status: number;
  detail: string;
  extensions?: string; // validation error
};
