import { memo, useCallback, useState } from "react";
import { type Key } from "react-aria-components";
import GoogleAnalytics from "../../lib/ga/ga.ts";
import { type RAGStrategy } from "../../stores/rag-options-store.ts";
import useReactFlowSettings from "../../stores/react-flow-settings-store.ts";
import RAGPipeline from "./rag-pipeline/RAGPipeline.tsx";
import SideMenu from "./side-menu/SideMenu.tsx";
import TutorialDialog from "./tutorial/TutorialDialog.tsx";

const Interface = memo(function Interface() {
  const [strategy, setStrategy] = useState<RAGStrategy>("basic");
  const saveReactFlowData = useReactFlowSettings(state => state.saveReactFlowData);

  const handleStrategyChange = useCallback(
    function handleStrategyChange(strategy: Key) {
      GoogleAnalytics.strategyChangeEvent(strategy as RAGStrategy);

      setStrategy(old => {
        saveReactFlowData(old);

        return strategy as RAGStrategy;
      });
      return strategy;
    },
    [saveReactFlowData]
  );

  return (
    <main className="flex h-screen w-screen overflow-hidden bg-neutral-50">
      <SideMenu onStrategyChange={handleStrategyChange} strategy={strategy} />
      <RAGPipeline strategy={strategy} />
      <div className="fixed bottom-2 right-2">
        <TutorialDialog />
      </div>
    </main>
  );
});

export default Interface;
