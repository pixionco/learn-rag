/**
 * Generated by orval v7.3.0 🍺
 * Do not edit manually.
 * Learn RAG API
 * OpenAPI spec version: v1
 */
import type { SentenceWindowMetadata } from "./sentenceWindowMetadata";

export type SentenceWindowMetadataSearchResult = {
  id?: string;
  metadata?: SentenceWindowMetadata;
  /** @nullable */
  relevance?: number | null;
  /** @nullable */
  text?: string | null;
};
