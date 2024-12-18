import { type NodeProps, Position } from "@xyflow/react";
import { memo, useMemo } from "react";
import NodeBase from "../../../components/react-flow/NodeBase";
import NodeHandle from "../../../components/react-flow/NodeHandle";
import NodeHandleContainer from "../../../components/react-flow/NodeHandleContainer";
import NodeHeader from "../../../components/react-flow/NodeHeader";
import usePipelineSettings from "../../../hooks/usePipelineSettings";
import { useUpdateNodeState } from "../../../hooks/useUpdateNodeState";
import useRAGOptions from "../../../stores/rag-options-store";
import { type RFNode } from "../../../types/react-flow";
import UserQueryForm from "./UserQueryForm";

export type UserQueryInputNode = RFNode<"user-query-input">;

const UserQueryInputNode = memo<NodeProps<UserQueryInputNode>>(function UserQueryInputNode() {
  const { strategy } = usePipelineSettings();
  const userQuery = useRAGOptions(state => state[strategy].userQuery);

  const nodeState = useMemo(
    function getNodeState() {
      return {
        locked: false,
        active: !userQuery,
        valid: !!userQuery,
      };
    },
    [userQuery]
  );
  useUpdateNodeState(nodeState);

  return (
    <NodeBase>
      <NodeHeader title="User Query Input" />
      <section className="flex w-full min-w-[300px] max-w-[300px] flex-col p-4">
        <UserQueryForm />
      </section>
      <NodeHandleContainer>
        <NodeHandle position={Position.Right} type="source" id="user-query" payload="User query" />
      </NodeHandleContainer>
    </NodeBase>
  );
});

export default UserQueryInputNode;
