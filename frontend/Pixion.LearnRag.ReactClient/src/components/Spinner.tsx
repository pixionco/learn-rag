import SpinnerIcon from "@/assets/icons/spinner.svg?react";
import { twMerge } from "tailwind-merge";
import { type SVGRProps } from "../types/svgr";

function Spinner({ className, ...rest }: SVGRProps) {
  return <SpinnerIcon className={twMerge("animate-spin", className)} {...rest} />;
}

export default Spinner;
