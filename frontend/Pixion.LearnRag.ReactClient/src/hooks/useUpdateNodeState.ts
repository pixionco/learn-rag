import { useNodeId, useReactFlow } from "@xyflow/react";
import { useEffect } from "react";
import { type NodeState, type RAGEdge, type RAGNode } from "../types/react-flow";

type UpdateNodeStateProps = Record<NodeState, boolean>;

export function useUpdateNodeState(nodeStates: UpdateNodeStateProps) {
  const { updateNodeData } = useReactFlow<RAGNode, RAGEdge>();
  const id = useNodeId();
  console.assert(id != null, "useNodeId must be called inside of a Node component");

  useEffect(() => {
    if (!id) return;
    // bad states override good ones, default is locked
    const finalState = nodeStates.locked
      ? "locked"
      : nodeStates.active
        ? "active"
        : nodeStates.valid
          ? "valid"
          : "active";

    updateNodeData(id, { state: finalState });
  }, [id, nodeStates.active, nodeStates.locked, nodeStates.valid, updateNodeData]);
}
