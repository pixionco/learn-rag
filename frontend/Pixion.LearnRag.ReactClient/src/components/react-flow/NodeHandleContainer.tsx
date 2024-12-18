import { memo, type PropsWithChildren } from "react";

const NodeHandleContainer = memo<PropsWithChildren>(function NodeHandleContainer({ children }) {
  return (
    <div className="flex flex-col divide-y divide-dashed divide-neutral-300 rounded-b-xl border-y border-neutral-300 bg-white">
      {children}
    </div>
  );
});

export default NodeHandleContainer;
