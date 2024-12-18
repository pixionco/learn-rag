import { memo, type PropsWithChildren } from "react";
import { Button, DialogTrigger, TooltipTrigger } from "react-aria-components";
import { type SVGRIcon } from "../../types/svgr.ts";
import DefaultTooltip from "../DefaultTooltip.tsx";
import DefaultDialog from "../dialog/DefaultDialog.tsx";

type NodeDialogProps = PropsWithChildren<{
  Icon: SVGRIcon;
  tooltip: string;
  disabled: boolean;
}>;

const NodeDialog = memo<NodeDialogProps>(function NodeDialog({
  Icon,
  tooltip,
  disabled,
  children,
}) {
  return (
    <DialogTrigger>
      <TooltipTrigger delay={500}>
        <Button
          className="group rounded-full bg-neutral-100 px-2 py-1 hover:bg-brand-100 disabled:cursor-not-allowed disabled:bg-neutral-50"
          isDisabled={disabled}
        >
          <Icon className="fill-neutral-700 hover:fill-brand-600 group-disabled:fill-neutral-300" />
        </Button>
        <DefaultTooltip>{tooltip}</DefaultTooltip>
      </TooltipTrigger>
      <DefaultDialog isDismissable>{children}</DefaultDialog>
    </DialogTrigger>
  );
});

export default NodeDialog;
