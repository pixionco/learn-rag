import { memo, type PropsWithChildren } from "react";
import { twJoin } from "tailwind-merge";
import { useCurrentNode } from "../../hooks/useCurrentNode";

const NodeBase = memo<PropsWithChildren>(function NodeBase({ children }) {
  const currentNode = useCurrentNode();

  return (
    <div
      className={twJoin(
        "border-2 flex flex-col rounded-xl bg-neutral-50 shadow-[0px_0px_16px_-7px] hover:shadow-[0px_0px_16px_-4px]",
        currentNode?.data.state === "valid" && "border-state-valid",
        currentNode?.data.state === "active" && "border-state-active",
        currentNode?.data.state === "locked" && "border-state-invalid"
      )}
    >
      {children}
    </div>
  );
});

export default NodeBase;
