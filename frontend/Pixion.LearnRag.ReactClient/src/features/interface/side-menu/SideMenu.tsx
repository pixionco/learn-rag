import Left from "@/assets/icons/chevron-left.svg?react";
import Menu from "@/assets/icons/menu.svg?react";
import LogoText from "@/assets/logo-text.svg?react";
import { memo, useMemo } from "react";
import {
  Button,
  Dialog,
  DialogTrigger,
  Header,
  Heading,
  Modal,
  Text,
  TooltipTrigger,
  type Key,
} from "react-aria-components";
import BlogLinkButton from "../../../components/blog-link/BlogLinkButton";
import DefaultTooltip from "../../../components/DefaultTooltip";
import { type RAGStrategy } from "../../../stores/rag-options-store";
import ApplicationSettingsPopover from "../ApplicationSettingsPopover";
import { strategyTabs } from "./constants";
import StrategyTabs from "./StrategyTabs";

type SideMenuProps = {
  strategy: RAGStrategy;
  onStrategyChange: (strategy: Key) => void;
};

const SideMenu = memo<SideMenuProps>(function SideMenu({ strategy, onStrategyChange }) {
  const selectedStrategyTab = useMemo(() => strategyTabs.find(s => s.id === strategy)!, [strategy]);

  return (
    <DialogTrigger>
      <TooltipTrigger delay={200} closeDelay={100}>
        <div className="flex items-center">
          <Button className="flex w-4 items-center justify-center bg-neutral-700 py-8 text-white transition-colors hover:bg-brand-500">
            <Menu className="size-4 scale-y-[180%] fill-white stroke-white" />
          </Button>
        </div>
        <DefaultTooltip>Open the menu</DefaultTooltip>
      </TooltipTrigger>
      <Modal
        isDismissable
        className="fixed inset-y-0 left-0 w-72 max-w-[100vw] outline-none entering:animate-slide-in exiting:animate-slide-out"
      >
        <Dialog className="flex h-full">
          <section className="flex flex-col gap-4 overflow-y-auto border-r border-neutral-300 bg-neutral-100">
            <Header className="flex items-center justify-between gap-8 px-4 py-2 tracking-wider ">
              <Heading slot="title" className="text-2xl font-semibold text-neutral-800 select-none">
                Menu
              </Heading>
              <div className="flex items-center gap-2">
                <ApplicationSettingsPopover />
              </div>
            </Header>
            <div className="flex grow flex-col gap-4 px-4">
              <StrategyTabs strategy={strategy} onStrategyChange={onStrategyChange} />
              <Text
                id="strategy-description"
                slot="description"
                className="flex flex-col gap-4 text-neutral-700 select-none"
              >
                <p>{selectedStrategyTab.description}</p>
                <p>Read more:</p>
                <BlogLinkButton href={selectedStrategyTab.href} text={selectedStrategyTab.title} />
              </Text>
            </div>
            <footer className="flex flex-col gap-2 p-4">
              <LogoText className="text-neutral-400" />
              <span className="text-neutral-400">© 2024 Pixion</span>
            </footer>
          </section>
          <div className="flex items-center bg-neutral-50">
            <TooltipTrigger delay={200} closeDelay={100}>
              <Button
                slot="close"
                className="w-4 bg-neutral-700 py-8 text-white transition-colors hover:bg-brand-500"
              >
                <Left className="size-4 scale-[180%] fill-white" />
              </Button>
              <DefaultTooltip>Close the menu</DefaultTooltip>
            </TooltipTrigger>
          </div>
        </Dialog>
      </Modal>
    </DialogTrigger>
  );
});

export default SideMenu;
