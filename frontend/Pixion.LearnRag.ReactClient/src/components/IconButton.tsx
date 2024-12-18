import { Button, type ButtonProps } from "react-aria-components";
import { type SVGRIcon } from "../types/svgr";

type IconButtonProps = Omit<ButtonProps, "className" | "style"> & {
  Icon: SVGRIcon;
};

export function IconButton({ Icon, ...props }: IconButtonProps) {
  return (
    <Button
      {...props}
      className="group flex items-center justify-center rounded-md border  border-neutral-800 bg-white p-1 transition-colors hover:border-brand-700 hover:bg-brand-50 pressed:border-brand-700 pressed:bg-brand-50"
    >
      <Icon className="fill-neutral-700 transition-colors group-hover:fill-brand-700 group-pressed:fill-brand-700" />
    </Button>
  );
}
