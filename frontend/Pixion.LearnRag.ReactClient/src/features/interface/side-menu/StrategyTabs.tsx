import { memo } from "react";
import { Label, ListBox, ListBoxItem, Select, type Key } from "react-aria-components";
import { type RAGStrategy } from "../../../stores/rag-options-store";
import { strategyTabs } from "./constants";

type StrategyTabsProps = {
  strategy: RAGStrategy;
  onStrategyChange: (strategy: Key) => void;
};

const StrategyTabs = memo<StrategyTabsProps>(function SideMenu({ strategy, onStrategyChange }) {
  return (
    <Select
      className="flex flex-wrap items-center gap-2"
      selectedKey={strategy}
      onSelectionChange={onStrategyChange}
    >
      <Label className="text-lg font-medium text-neutral-800">RAG Strategy:</Label>
      <ListBox className="flex w-full flex-col gap-2">
        {strategyTabs.map(tab => (
          <ListBoxItem
            key={tab.id}
            id={tab.id}
            className="flex flex-col gap-2 rounded-full bg-neutral-500 px-4 py-1 font-semibold text-white  transition-colors hover:bg-neutral-400 selected:bg-brand-500"
          >
            {tab.title}
          </ListBoxItem>
        ))}
      </ListBox>
    </Select>
  );
});

export default StrategyTabs;
