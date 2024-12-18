import { type LLMAnswerNode } from "./LLMAnswerNode";
import { type PromptTemplateNode } from "./prompt-template-node/PromptTemplateNode";
import { type UserQueryInputNode } from "./user-query-input-node/UserQueryInputNode";
import { type UserQueryEmbeddingNode } from "./UserQueryEmbeddingNode";
import { type VectorDatabaseSearchNode } from "./vector-database-search-node/VectorDatabaseSearchNode";

export type RetrievalNode =
  | LLMAnswerNode
  | PromptTemplateNode
  | UserQueryEmbeddingNode
  | UserQueryInputNode
  | VectorDatabaseSearchNode;
