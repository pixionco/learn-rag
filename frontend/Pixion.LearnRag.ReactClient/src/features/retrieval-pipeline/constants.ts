import Chat from "@/assets/icons/chat.svg?react";
import DatabaseSearch from "@/assets/icons/database-search.svg?react";
import Template from "@/assets/icons/template.svg?react";
import Vector from "@/assets/icons/vector.svg?react";
import { type RAGEdge } from "../../types/react-flow";
import LLMAnswerNode from "./LLMAnswerNode";
import PromptTemplateNode from "./prompt-template-node/PromptTemplateNode";
import { type RetrievalNode } from "./types";
import UserQueryInputNode from "./user-query-input-node/UserQueryInputNode";
import UserQueryEmbeddingNode from "./UserQueryEmbeddingNode";
import VectorDatabaseSearchNode from "./vector-database-search-node/VectorDatabaseSearchNode";

export const retrievalNodeTypes = {
  "user-query-input": UserQueryInputNode,
  "user-query-embedding": UserQueryEmbeddingNode,
  "vector-search": VectorDatabaseSearchNode,
  "prompt-template": PromptTemplateNode,
  "llm-answer": LLMAnswerNode,
};

export const initialRetrievalNodes: RetrievalNode[] = [
  {
    type: "user-query-input",
    id: "user-query-input",
    data: {
      state: "locked",
    },
    position: {
      x: 0,
      y: 0,
    },
  },
  {
    type: "user-query-embedding",
    id: "user-query-embedding",
    data: {
      state: "locked",
    },
    position: {
      x: 400,
      y: 0,
    },
  },
  {
    type: "vector-search",
    id: "vector-search",
    data: {
      state: "locked",
    },
    position: {
      x: 1100,
      y: 0,
    },
  },
  {
    type: "prompt-template",
    id: "prompt-template",
    data: {
      state: "locked",
    },
    position: {
      x: 1600,
      y: 0,
    },
  },
  {
    type: "llm-answer",
    id: "llm-answer",
    data: {
      state: "locked",
    },
    position: {
      x: 2200,
      y: 0,
    },
  },
] as const;

export const initialRetrievalEdges: RAGEdge[] = [
  {
    type: "payload",
    id: "user-query-to-embedding",
    source: "user-query-input",
    target: "user-query-embedding",
    animated: true,
    data: {
      state: "locked",
      PayloadIcon: Chat,
    },
  },
  {
    type: "payload",
    id: "embedding-to-database",
    source: "user-query-embedding",
    target: "vector-search",
    animated: true,
    data: {
      state: "locked",
      PayloadIcon: Vector,
    },
  },
  {
    type: "payload",
    id: "database-to-template",
    source: "vector-search",
    target: "prompt-template",
    animated: true,
    data: {
      state: "locked",
      PayloadIcon: DatabaseSearch,
    },
  },
  {
    type: "payload",
    id: "template-to-llm",
    source: "prompt-template",
    target: "llm-answer",
    animated: true,
    data: {
      state: "locked",
      PayloadIcon: Template,
    },
  },
] as const;
