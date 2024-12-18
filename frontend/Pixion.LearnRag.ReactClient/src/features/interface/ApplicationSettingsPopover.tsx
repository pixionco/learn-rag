import Design from "@/assets/icons/design.svg?react";
import LeftRight from "@/assets/icons/left-right.svg?react";
import Map from "@/assets/icons/map.svg?react";
import Settings from "@/assets/icons/settings.svg?react";
import UpDown from "@/assets/icons/up-down.svg?react";
import {
  Dialog,
  DialogTrigger,
  Header,
  Heading,
  Popover,
  TooltipTrigger,
} from "react-aria-components";
import DefaultOverlayArrow from "../../components/DefaultOverlayArrow";
import DefaultTooltip from "../../components/DefaultTooltip";
import { IconButton } from "../../components/IconButton";
import { IconToggleButton } from "../../components/IconToggleButton";
import GoogleAnalytics from "../../lib/ga/ga";
import useApplicationSettings from "../../stores/application-settings-store";

function ApplicationSettingsPopover() {
  const layoutDirection = useApplicationSettings(state => state.layoutDirection);
  const setLayoutDirection = useApplicationSettings(state => state.setLayoutDirection);

  const canvasControls = useApplicationSettings(state => state.canvasControls);
  const setCanvasControls = useApplicationSettings(state => state.setCanvasControls);

  return (
    <DialogTrigger>
      <IconButton aria-label="Menu" Icon={Settings} />
      <Popover offset={12} className="shadow-xl">
        <DefaultOverlayArrow className="fill-white" />
        <Dialog className="flex flex-col gap-4 rounded-lg  bg-white p-4">
          <Header className="flex flex-col gap-1">
            <Heading slot="title" className="text-lg font-semibold text-neutral-800">
              Application Settings
            </Heading>
            <hr className="border-neutral-800" />
          </Header>
          <div className="flex flex-col gap-4">
            <div className="flex flex-col gap-2">
              <span className="text-sm font-semibold text-neutral-700">Layout direction:</span>
              <div className="flex items-center gap-2">
                <IconToggleButton
                  isSelected={layoutDirection === "col"}
                  onPress={() => setLayoutDirection("col")}
                  Icon={UpDown}
                />
                <IconToggleButton
                  isSelected={layoutDirection === "row"}
                  onPress={() => setLayoutDirection("row")}
                  Icon={LeftRight}
                />
              </div>
            </div>
            <div className="flex flex-col gap-2">
              <span className="text-sm font-semibold text-neutral-700">Controls:</span>
              <div className="flex items-center gap-2">
                <TooltipTrigger delay={500}>
                  <IconToggleButton
                    isSelected={canvasControls === "slippy-map"}
                    onPress={() => {
                      setCanvasControls("slippy-map");
                      GoogleAnalytics.canvasControlsEvent("slippy-map");
                    }}
                    Icon={Map}
                  />
                  <DefaultTooltip>Follows the slippy map controls</DefaultTooltip>
                </TooltipTrigger>
                <TooltipTrigger delay={500}>
                  <IconToggleButton
                    isSelected={canvasControls === "design"}
                    onPress={() => {
                      setCanvasControls("design");
                      GoogleAnalytics.canvasControlsEvent("design");
                    }}
                    Icon={Design}
                  />
                  <DefaultTooltip>Follows the figma/sketch/design tool controls</DefaultTooltip>
                </TooltipTrigger>
              </div>
            </div>
          </div>
        </Dialog>
      </Popover>
    </DialogTrigger>
  );
}

export default ApplicationSettingsPopover;
