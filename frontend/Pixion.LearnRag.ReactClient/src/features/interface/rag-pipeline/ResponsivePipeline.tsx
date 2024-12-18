import Up from "@/assets/icons/chevron-up.svg?react";
import { ReactFlowProvider } from "@xyflow/react";
import { memo, type PropsWithChildren } from "react";
import { Button, TooltipTrigger } from "react-aria-components";
import { twJoin } from "tailwind-merge";
import DefaultTooltip from "../../../components/DefaultTooltip.tsx";
import useApplicationSettings, {
  type Pipeline,
} from "../../../stores/application-settings-store.ts";

type ResponsivePipelineProps = PropsWithChildren<{
  pipeline: Pipeline;
}>;

const ResponsivePipeline = memo<ResponsivePipelineProps>(function ResponsivePipeline({
  pipeline,
  children,
}) {
  const layoutDirection = useApplicationSettings(state => state.layoutDirection);
  const minimized = useApplicationSettings(state => state[pipeline].minimized);
  const toggleMinimized = useApplicationSettings(state => state.toggleMinimized);

  return (
    <ReactFlowProvider>
      <div
        className={twJoin(
          "size-full flex transition-all",
          layoutDirection === "row" ? "flex-row" : "flex-col",
          minimized && layoutDirection === "col" && "h-3",
          minimized && layoutDirection === "row" && "w-3"
        )}
      >
        <div
          className={twJoin(
            "flex items-center justify-center ",
            layoutDirection === "col" && "h-3",
            layoutDirection === "row" && "w-3"
          )}
        >
          <TooltipTrigger delay={200} closeDelay={100}>
            <Button
              className={twJoin(
                "flex items-center justify-center bg-neutral-700 hover:bg-brand-500 transition-colors",
                layoutDirection === "row" ? "py-6" : "px-6"
              )}
              onPress={() => toggleMinimized(pipeline)}
            >
              <Up
                className={twJoin(
                  "size-3 fill-neutral-50 scale-[180%]",
                  layoutDirection === "col" && minimized && "rotate-0",
                  layoutDirection === "col" && !minimized && "rotate-180",
                  layoutDirection === "row" && minimized && "-rotate-90",
                  layoutDirection === "row" && !minimized && "rotate-90"
                )}
              />
              <DefaultTooltip>Toggle pipeline minimized</DefaultTooltip>
            </Button>
          </TooltipTrigger>
        </div>
        {children}
      </div>
    </ReactFlowProvider>
  );
});

export default ResponsivePipeline;
