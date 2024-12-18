import { type ReactFlowInstance, type ReactFlowJsonObject } from "@xyflow/react";
import { create } from "zustand";
import { type IngestionNode } from "../features/ingestion-pipeline/types";
import { type RetrievalNode } from "../features/retrieval-pipeline/types";
import { type RAGEdge } from "../types/react-flow";
import { type RAGStrategy } from "./rag-options-store";

type StrategyReactFlowSettings = {
  ingestionRFInstance?: ReactFlowInstance<IngestionNode, RAGEdge>;
  retrievalRFInstance?: ReactFlowInstance<RetrievalNode, RAGEdge>;
  ingestionPipelineData?: ReactFlowJsonObject<IngestionNode, RAGEdge>;
  retrievalPipelineData?: ReactFlowJsonObject<RetrievalNode, RAGEdge>;
};

type ReactFlowSettingsState = {
  [key in RAGStrategy]: StrategyReactFlowSettings;
};

type ReactFlowSettingsActions = {
  setIngestionRFInstance: (
    strategy: RAGStrategy,
    rfInstance: ReactFlowInstance<IngestionNode, RAGEdge>
  ) => void;
  setRetrievalRFInstance: (
    strategy: RAGStrategy,
    rfInstance: ReactFlowInstance<RetrievalNode, RAGEdge>
  ) => void;
  saveReactFlowData: (strategy: RAGStrategy) => void;
};

type ReactFlowSettingsStore = ReactFlowSettingsState & ReactFlowSettingsActions;

const useReactFlowSettings = create<ReactFlowSettingsStore>(set => ({
  basic: {},
  "sentence-window": {},
  "auto-merging": {},
  hierarchical: {},
  "hypothetical-question": {},

  setIngestionRFInstance: (strategy, rfInstance) =>
    set(state => ({
      [strategy]: {
        ...state[strategy],
        ingestionRFInstance: rfInstance,
      } satisfies StrategyReactFlowSettings,
    })),
  setRetrievalRFInstance: (strategy, rfInstance) =>
    set(state => ({
      [strategy]: {
        ...state[strategy],
        retrievalRFInstance: rfInstance,
      } satisfies StrategyReactFlowSettings,
    })),
  saveReactFlowData: strategy =>
    set(state => ({
      [strategy]: {
        // remove old object references
        retrievalPipelineData: state[strategy].retrievalRFInstance?.toObject(),
        ingestionPipelineData: state[strategy].ingestionRFInstance?.toObject(),
      } satisfies StrategyReactFlowSettings,
    })),
}));

export default useReactFlowSettings;
