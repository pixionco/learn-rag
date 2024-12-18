import { memo } from "react";
import { twJoin } from "tailwind-merge";
import { PipelineSettingsContext } from "../../../hooks/usePipelineSettings.ts";
import useApplicationSettings from "../../../stores/application-settings-store.ts";
import { type RAGStrategy } from "../../../stores/rag-options-store.ts";
import IngestionPipeline from "../../ingestion-pipeline/IngestionPipeline.tsx";
import RetrievalPipeline from "../../retrieval-pipeline/RetrievalPipeline.tsx";
import ResponsivePipeline from "./ResponsivePipeline.tsx";

type RAGPipelineProps = {
  strategy: RAGStrategy;
};

const RAGPipeline = memo<RAGPipelineProps>(function RAGPipeline({ strategy }) {
  const layoutDirection = useApplicationSettings(state => state.layoutDirection);

  return (
    <PipelineSettingsContext.Provider value={{ strategy }}>
      <section
        className={twJoin("size-full flex", layoutDirection === "row" ? "flex-row" : "flex-col")}
      >
        <ResponsivePipeline pipeline="ingestion">
          <IngestionPipeline key={strategy} />
        </ResponsivePipeline>
        <ResponsivePipeline pipeline="retrieval">
          <RetrievalPipeline key={strategy} />
        </ResponsivePipeline>
      </section>
    </PipelineSettingsContext.Provider>
  );
});

export default RAGPipeline;
