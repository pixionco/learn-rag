import { useNodeId, useNodesData } from "@xyflow/react";

export function useCurrentNode() {
  const id = useNodeId();
  console.assert(id != null, "useNodeId must be called inside of a Node component");

  const currentNode = useNodesData(id!);
  console.assert(currentNode != null, "currentNode should not be null");

  return currentNode;
}
