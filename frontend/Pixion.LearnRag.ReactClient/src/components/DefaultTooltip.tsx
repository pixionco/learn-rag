import { type PropsWithChildren } from "react";
import {
  Tooltip as AriaTooltip,
  type TooltipProps as AriaTooltipProps,
} from "react-aria-components";
import DefaultOverlayArrow from "./DefaultOverlayArrow";

type DefaultTooltipProps = PropsWithChildren<Omit<AriaTooltipProps, "className">>;

function DefaultTooltip({ children, offset = 12, ...props }: DefaultTooltipProps) {
  return (
    <AriaTooltip
      {...props}
      offset={offset}
      className="group max-w-prose rounded-md border-2 border-brand-500 bg-brand-500 p-2 text-sm font-medium text-white"
    >
      <DefaultOverlayArrow className="fill-brand-500" />
      {children}
    </AriaTooltip>
  );
}

export default DefaultTooltip;
