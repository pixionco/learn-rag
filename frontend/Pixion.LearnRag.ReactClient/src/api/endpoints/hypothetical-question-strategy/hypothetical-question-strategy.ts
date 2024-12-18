/**
 * Generated by orval v7.3.0 🍺
 * Do not edit manually.
 * Learn RAG API
 * OpenAPI spec version: v1
 */
import type {
  GetHypotheticalQuestionEmbeddingOptionsParams,
  HypotheticalQuestionEmbeddingOptions,
  HypotheticalQuestionMetadataEmbeddingRecord,
  HypotheticalQuestionMetadataSearchResult,
  HypotheticalQuestionPreviewParams,
  HypotheticalQuestionSearchParams,
} from "../../model";
import getHypotheticalQuestionEmbeddingOptionsMutator from "../../axios";
import hypotheticalQuestionSearchMutator from "../../axios";
import hypotheticalQuestionPreviewMutator from "../../axios";

type SecondParameter<T extends (...args: any) => any> = Parameters<T>[1];

export const getHypotheticalQuestionEmbeddingOptions = (
  params: GetHypotheticalQuestionEmbeddingOptionsParams,
  options?: SecondParameter<typeof getHypotheticalQuestionEmbeddingOptionsMutator>
) => {
  return getHypotheticalQuestionEmbeddingOptionsMutator<HypotheticalQuestionEmbeddingOptions[]>(
    { url: `/api/hypothetical-question/embedding-options`, method: "GET", params },
    options
  );
};
export const hypotheticalQuestionSearch = (
  params: HypotheticalQuestionSearchParams,
  options?: SecondParameter<typeof hypotheticalQuestionSearchMutator>
) => {
  return hypotheticalQuestionSearchMutator<HypotheticalQuestionMetadataSearchResult[]>(
    { url: `/api/hypothetical-question/search`, method: "GET", params },
    options
  );
};
export const hypotheticalQuestionPreview = (
  params: HypotheticalQuestionPreviewParams,
  options?: SecondParameter<typeof hypotheticalQuestionPreviewMutator>
) => {
  return hypotheticalQuestionPreviewMutator<HypotheticalQuestionMetadataEmbeddingRecord[]>(
    { url: `/api/hypothetical-question/preview`, method: "GET", params },
    options
  );
};
export type GetHypotheticalQuestionEmbeddingOptionsResult = NonNullable<
  Awaited<ReturnType<typeof getHypotheticalQuestionEmbeddingOptions>>
>;
export type HypotheticalQuestionSearchResult = NonNullable<
  Awaited<ReturnType<typeof hypotheticalQuestionSearch>>
>;
export type HypotheticalQuestionPreviewResult = NonNullable<
  Awaited<ReturnType<typeof hypotheticalQuestionPreview>>
>;
