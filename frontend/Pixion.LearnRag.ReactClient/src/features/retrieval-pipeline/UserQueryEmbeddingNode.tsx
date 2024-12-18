import { type NodeProps, Position } from "@xyflow/react";
import { memo, useMemo } from "react";
import ErrorMessage from "../../components/ErrorMessage";
import NodeBase from "../../components/react-flow/NodeBase";
import NodeHandle from "../../components/react-flow/NodeHandle";
import NodeHandleContainer from "../../components/react-flow/NodeHandleContainer";
import NodeHeader from "../../components/react-flow/NodeHeader";
import TextEmbeddingFlow from "../../components/TextEmbeddingFlow";
import usePipelineSettings from "../../hooks/usePipelineSettings";
import { useUpdateNodeState } from "../../hooks/useUpdateNodeState";
import useGenerateEmbedding from "../../queries/useGenerateEmbedding";
import useRAGOptions from "../../stores/rag-options-store";
import { type RFNode } from "../../types/react-flow";

export type UserQueryEmbeddingNode = RFNode<"user-query-embedding">;

const UserQueryEmbeddingNode = memo<NodeProps<UserQueryEmbeddingNode>>(
  function UserQueryEmbeddingNode() {
    const { strategy } = usePipelineSettings();
    const userQuery = useRAGOptions(state => state[strategy].userQuery);
    const embeddingQuery = useGenerateEmbedding(userQuery);

    const nodeState = useMemo(
      function getNodeState() {
        return {
          locked: embeddingQuery.isError,
          active: embeddingQuery.isFetching,
          valid: embeddingQuery.isSuccess,
        };
      },
      [embeddingQuery.isError, embeddingQuery.isFetching, embeddingQuery.isSuccess]
    );
    useUpdateNodeState(nodeState);

    return (
      <NodeBase>
        <NodeHeader title="Embedding Model" />
        <section className="flex min-w-[600px] max-w-[600px] flex-col gap-4 p-4">
          <TextEmbeddingFlow text={userQuery} embedding={embeddingQuery.data} />
          {embeddingQuery.isError && (
            <ErrorMessage
              message={
                embeddingQuery.error.status === 429
                  ? "The ChatGPT server is overloaded. Please try again in few minutes."
                  : "There is an error with the ChatGPT server."
              }
            ></ErrorMessage>
          )}
        </section>
        <NodeHandleContainer>
          <NodeHandle position={Position.Left} type="target" id="user-query" />
          <NodeHandle position={Position.Right} type="source" id="embedding" payload="Embedding" />
        </NodeHandleContainer>
      </NodeBase>
    );
  }
);

export default UserQueryEmbeddingNode;
