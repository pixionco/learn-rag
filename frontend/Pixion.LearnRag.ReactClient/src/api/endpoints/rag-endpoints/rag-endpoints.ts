/**
 * Generated by orval v7.3.0 🍺
 * Do not edit manually.
 * Learn RAG API
 * OpenAPI spec version: v1
 */
import type {
  AnswerPromptTemplate,
  GenerateAnswerQuery,
  GenerateEmbeddingQuery,
  SingleReadOnlyMemory,
} from "../../model";
import generateEmbeddingMutator from "../../axios";
import generateAnswerMutator from "../../axios";
import getAnswerTemplateMutator from "../../axios";

type SecondParameter<T extends (...args: any) => any> = Parameters<T>[1];

export const generateEmbedding = (
  generateEmbeddingQuery: GenerateEmbeddingQuery,
  options?: SecondParameter<typeof generateEmbeddingMutator>
) => {
  return generateEmbeddingMutator<SingleReadOnlyMemory>(
    {
      url: `/learn-rag-api/rag/embedding`,
      method: "POST",
      headers: { "Content-Type": "application/json" },
      data: generateEmbeddingQuery,
    },
    options
  );
};
export const generateAnswer = (
  generateAnswerQuery: GenerateAnswerQuery,
  options?: SecondParameter<typeof generateAnswerMutator>
) => {
  return generateAnswerMutator<string>(
    {
      url: `/learn-rag-api/rag/generate-answer`,
      method: "POST",
      headers: { "Content-Type": "application/json" },
      data: generateAnswerQuery,
    },
    options
  );
};
export const getAnswerTemplate = (options?: SecondParameter<typeof getAnswerTemplateMutator>) => {
  return getAnswerTemplateMutator<AnswerPromptTemplate>(
    { url: `/learn-rag-api/rag/answer-template`, method: "GET" },
    options
  );
};
export type GenerateEmbeddingResult = NonNullable<Awaited<ReturnType<typeof generateEmbedding>>>;
export type GenerateAnswerResult = NonNullable<Awaited<ReturnType<typeof generateAnswer>>>;
export type GetAnswerTemplateResult = NonNullable<Awaited<ReturnType<typeof getAnswerTemplate>>>;
