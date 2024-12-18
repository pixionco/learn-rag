import { Button as AriaButton, type ButtonProps as AriaButtonProps } from "react-aria-components";

type ToolbarButtonProps = Omit<AriaButtonProps, "className" | "style"> & { color?: string };

export function ToolbarButton({ ...props }: ToolbarButtonProps) {
  return (
    <AriaButton
      {...props}
      className="nodrag ml-auto w-fit bg-brand-500 px-4 py-1 font-semibold text-white hover:bg-brand-600 disabled:cursor-not-allowed"
    />
  );
}
