import { Handle, Position, type HandleProps } from "@xyflow/react";
import { memo } from "react";
import { twJoin } from "tailwind-merge";

type NodeHandleProps = Omit<HandleProps, "className"> & { payload?: string };

const NodeHandle = memo<NodeHandleProps>(function NodeHandle({ payload, ...props }) {
  return (
    <div
      className={twJoin(
        "flex gap-2 relative min-h-8 py-1 items-center",
        props.position === Position.Left && "justify-end flex-row-reverse",
        props.position === Position.Right && "justify-end"
      )}
    >
      {payload && <span className="text-neutral-700">{payload}</span>}
      <Handle
        {...props}
        className={twJoin(
          "h-4 w-1.5 rounded-sm border-0 bg-brand-500 static translate-y-0",
          props.position === Position.Left && "-translate-x-1",
          props.position === Position.Right && "translate-x-1"
        )}
      />
    </div>
  );
});

export default NodeHandle;
