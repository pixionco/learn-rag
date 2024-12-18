import Chunks from "@/assets/icons/chunks.svg?react";
import Document from "@/assets/icons/document.svg?react";
import JSON from "@/assets/icons/json.svg?react";
import Vector from "@/assets/icons/vector.svg?react";
import { type RAGEdge } from "../../types/react-flow";
import ChunkEmbeddingNode from "./ChunkEmbeddingNode";
import DocumentChunkerNode from "./document-chunker-node/DocumentChunkerNode";
import DocumentPickerNode from "./document-picker-node/DocumentPickerNode";
import { type IngestionNode } from "./types";
import VectorDatabasePreviewNode from "./vector-database-preview-node/VectorDatabasePreviewNode";

export const ingestionNodeTypes = {
  "document-picker": DocumentPickerNode,
  "document-chunker": DocumentChunkerNode,
  "chunk-embedding": ChunkEmbeddingNode,
  "database-preview": VectorDatabasePreviewNode,
};

export const initalIngestionNodes: IngestionNode[] = [
  {
    type: "document-picker",
    id: "document-picker",
    data: {
      state: "active",
    },
    position: {
      x: 0,
      y: 0,
    },
  },
  {
    type: "document-chunker",
    id: "document-chunker",
    data: {
      state: "locked",
    },
    position: {
      x: 400,
      y: 0,
    },
  },
  {
    type: "chunk-embedding",
    id: "chunk-embedding",
    data: {
      state: "locked",
    },
    position: {
      x: 1000,
      y: -200,
    },
  },
  {
    type: "database-preview",
    id: "database-preview",
    data: {
      state: "locked",
    },
    position: {
      x: 1700,
      y: 150,
    },
  },
] as const;

export const initialIngestionEdges: RAGEdge[] = [
  {
    type: "payload",
    id: "document-chunker",
    source: "document-picker",
    target: "document-chunker",
    animated: true,
    data: {
      state: "active",
      PayloadIcon: Document,
    },
  },
  {
    type: "payload",
    id: "chunker-embedding",
    source: "document-chunker",
    target: "chunk-embedding",
    animated: true,
    data: {
      state: "locked",
      PayloadIcon: Chunks,
    },
  },
  {
    type: "payload",
    id: "chunks-to-database",
    source: "document-chunker",
    sourceHandle: "chunks",
    target: "database-preview",
    targetHandle: "chunks",
    animated: true,
    data: {
      state: "locked",
      PayloadIcon: Chunks,
    },
  },
  {
    type: "payload",
    id: "metadata-to-database",
    source: "document-chunker",
    sourceHandle: "metadata",
    target: "database-preview",
    targetHandle: "metadata",
    animated: true,
    data: {
      state: "locked",
      PayloadIcon: JSON,
    },
  },
  {
    type: "payload",
    id: "embeddings-to-database",
    source: "chunk-embedding",
    target: "database-preview",
    targetHandle: "embeddings",
    animated: true,
    data: {
      state: "locked",
      PayloadIcon: Vector,
    },
  },
] as const;
