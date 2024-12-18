import { createContext, useContext } from "react";
import { type RAGStrategy } from "../stores/rag-options-store";

export type PipelineSettings = {
  strategy: RAGStrategy;
};

export const PipelineSettingsContext = createContext<PipelineSettings | null>(null);

export default function usePipelineSettings() {
  const settings = useContext(PipelineSettingsContext);

  if (!settings) {
    throw new Error("usePipelineSettings must be used inside of PipelineSettingsContext.Provider");
  }

  return settings;
}
