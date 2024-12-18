import { Button as AriaButton, type ButtonProps as AriaButtonProps } from "react-aria-components";
import Spinner from "./Spinner";

type ButtonProps = Omit<AriaButtonProps, "className" | "style"> & {
  loading?: boolean;
};

export function Button({ loading = false, children, ...props }: ButtonProps) {
  return (
    <AriaButton
      {...props}
      className="nodrag flex w-fit items-center gap-2 rounded-full bg-brand-500 px-4 py-1 font-semibold text-white transition-colors hover:bg-brand-600 disabled:cursor-not-allowed disabled:bg-neutral-300 disabled:text-neutral-100"
    >
      {loading ? <Spinner className="fill-white" /> : children}
    </AriaButton>
  );
}
