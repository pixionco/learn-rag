import { type ChunkEmbeddingNode } from "./ChunkEmbeddingNode";
import { type DocumentChunkerNode } from "./document-chunker-node/DocumentChunkerNode";
import { type DocumentPickerNode } from "./document-picker-node/DocumentPickerNode";
import { type VectorDatabasePreviewNode } from "./vector-database-preview-node/VectorDatabasePreviewNode";

export type IngestionNode =
  | ChunkEmbeddingNode
  | DocumentPickerNode
  | DocumentChunkerNode
  | VectorDatabasePreviewNode;
