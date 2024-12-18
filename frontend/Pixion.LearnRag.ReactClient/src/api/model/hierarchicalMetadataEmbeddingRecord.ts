/**
 * Generated by orval v7.3.0 🍺
 * Do not edit manually.
 * Learn RAG API
 * OpenAPI spec version: v1
 */
import type { SingleReadOnlyMemory } from "./singleReadOnlyMemory";
import type { HierarchicalMetadata } from "./hierarchicalMetadata";

export type HierarchicalMetadataEmbeddingRecord = {
  embedding?: SingleReadOnlyMemory;
  id?: string;
  metadata?: HierarchicalMetadata;
  /** @nullable */
  text?: string | null;
};