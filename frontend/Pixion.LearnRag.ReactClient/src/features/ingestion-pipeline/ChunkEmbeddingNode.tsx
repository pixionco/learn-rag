import { Position, type NodeProps } from "@xyflow/react";
import { memo, useMemo } from "react";
import NodeBase from "../../components/react-flow/NodeBase";
import NodeHandle from "../../components/react-flow/NodeHandle";
import NodeHandleContainer from "../../components/react-flow/NodeHandleContainer";
import NodeHeader from "../../components/react-flow/NodeHeader";
import TextEmbeddingFlow from "../../components/TextEmbeddingFlow";
import usePipelineSettings from "../../hooks/usePipelineSettings";
import { useUpdateNodeState } from "../../hooks/useUpdateNodeState";
import usePreview from "../../queries/usePreview";
import { type RFNode } from "../../types/react-flow";

export type ChunkEmbeddingNode = RFNode<"chunk-embedding">;

const ChunkEmbeddingNode = memo<NodeProps<ChunkEmbeddingNode>>(
  function ChunkEmbeddingNodeContent() {
    const { strategy } = usePipelineSettings();
    const previewQuery = usePreview(strategy);

    const nodeState = useMemo(
      function getNodeState() {
        return {
          locked: previewQuery.isError,
          active: previewQuery.isLoading,
          valid: previewQuery.isSuccess,
        };
      },
      [previewQuery.isError, previewQuery.isLoading, previewQuery.isSuccess]
    );
    useUpdateNodeState(nodeState);

    return (
      <NodeBase>
        <NodeHeader
          title="Embedding Model"
          blogLinkHref="https://pixion.co/blog/rag-in-practice-embedding"
        />
        <section className="min-w-[600px] max-w-[600px] p-4">
          <div className="flex flex-col gap-6">
            <div className="flex flex-col items-center justify-between gap-4">
              <TextEmbeddingFlow
                text={previewQuery.data?.at(0)?.text ?? undefined}
                embedding={previewQuery.data?.at(0)?.embedding}
              />
              <TextEmbeddingFlow
                text={previewQuery.data?.at(1)?.text ?? undefined}
                embedding={previewQuery.data?.at(1)?.embedding}
              />
            </div>
          </div>
        </section>
        <NodeHandleContainer>
          <NodeHandle position={Position.Left} type="target" />
          <NodeHandle position={Position.Right} type="source" payload="Embedding" />
        </NodeHandleContainer>
      </NodeBase>
    );
  }
);

export default ChunkEmbeddingNode;
