import { type Edge, type Node } from "@xyflow/react";
import { type PayloadEdge } from "../components/react-flow/PayloadEdge";
import { type IngestionNode } from "../features/ingestion-pipeline/types";
import { type RetrievalNode } from "../features/retrieval-pipeline/types";

export type NodeState = "valid" | "active" | "locked";
export type DefaultNodeData = { state: NodeState };
export type DefaultEdgeData = { state: NodeState };

export type RFNode<
  TName extends string,
  TProps extends Record<string, unknown> = DefaultNodeData,
> = Node<TProps & DefaultNodeData, TName>;

export type RFEdge<
  TName extends string,
  TProps extends Record<string, unknown> = DefaultEdgeData,
> = Edge<TProps & DefaultEdgeData, TName>;

export type RAGEdge = PayloadEdge;
export type RAGNode = IngestionNode | RetrievalNode;
