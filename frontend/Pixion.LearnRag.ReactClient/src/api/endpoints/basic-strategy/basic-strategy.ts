/**
 * Generated by orval v7.3.0 🍺
 * Do not edit manually.
 * Learn RAG API
 * OpenAPI spec version: v1
 */
import type {
  BasicEmbeddingOptions,
  BasicMetadataEmbeddingRecord,
  BasicMetadataSearchResult,
  BasicPreviewParams,
  BasicSearchParams,
  GetBasicEmbeddingOptionsParams,
} from "../../model";
import getBasicEmbeddingOptionsMutator from "../../axios";
import basicSearchMutator from "../../axios";
import basicPreviewMutator from "../../axios";

type SecondParameter<T extends (...args: any) => any> = Parameters<T>[1];

export const getBasicEmbeddingOptions = (
  params: GetBasicEmbeddingOptionsParams,
  options?: SecondParameter<typeof getBasicEmbeddingOptionsMutator>
) => {
  return getBasicEmbeddingOptionsMutator<BasicEmbeddingOptions[]>(
    { url: `/learn-rag-api/basic/embedding-options`, method: "GET", params },
    options
  );
};
export const basicSearch = (
  params: BasicSearchParams,
  options?: SecondParameter<typeof basicSearchMutator>
) => {
  return basicSearchMutator<BasicMetadataSearchResult[]>(
    { url: `/learn-rag-api/basic/search`, method: "GET", params },
    options
  );
};
export const basicPreview = (
  params: BasicPreviewParams,
  options?: SecondParameter<typeof basicPreviewMutator>
) => {
  return basicPreviewMutator<BasicMetadataEmbeddingRecord[]>(
    { url: `/learn-rag-api/basic/preview`, method: "GET", params },
    options
  );
};
export type GetBasicEmbeddingOptionsResult = NonNullable<
  Awaited<ReturnType<typeof getBasicEmbeddingOptions>>
>;
export type BasicSearchResult = NonNullable<Awaited<ReturnType<typeof basicSearch>>>;
export type BasicPreviewResult = NonNullable<Awaited<ReturnType<typeof basicPreview>>>;
