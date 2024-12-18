/**
 * Generated by orval v7.3.0 🍺
 * Do not edit manually.
 * Learn RAG API
 * OpenAPI spec version: v1
 */
import type { Document } from "../../model";
import getDocumentsMutator from "../../axios";

type SecondParameter<T extends (...args: any) => any> = Parameters<T>[1];

export const getDocuments = (options?: SecondParameter<typeof getDocumentsMutator>) => {
  return getDocumentsMutator<Document[]>({ url: `/api/documents`, method: "GET" }, options);
};
export type GetDocumentsResult = NonNullable<Awaited<ReturnType<typeof getDocuments>>>;
