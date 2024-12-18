import { ToggleButton, type ToggleButtonProps } from "react-aria-components";
import { type SVGRIcon } from "../types/svgr";

type IconToggleButtonProps = Omit<ToggleButtonProps, "className" | "style"> & {
  Icon: SVGRIcon;
};

export function IconToggleButton({ Icon, ...props }: IconToggleButtonProps) {
  return (
    <ToggleButton
      {...props}
      className="group flex items-center justify-center rounded-md border border-neutral-800 bg-white p-1 outline-none transition-colors selected:border-brand-700 selected:bg-brand-50"
    >
      <Icon className="fill-neutral-400 transition-colors group-selected:fill-brand-700" />
    </ToggleButton>
  );
}
