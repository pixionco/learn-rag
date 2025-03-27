import { Position, type NodeProps } from "@xyflow/react";
import { memo, useMemo } from "react";
import NodeBase from "../../../components/react-flow/NodeBase.tsx";
import NodeHandle from "../../../components/react-flow/NodeHandle.tsx";
import NodeHandleContainer from "../../../components/react-flow/NodeHandleContainer.tsx";
import NodeHeader from "../../../components/react-flow/NodeHeader.tsx";
import usePipelineSettings from "../../../hooks/usePipelineSettings.ts";
import { useUpdateNodeState } from "../../../hooks/useUpdateNodeState.ts";
import usePreview from "../../../queries/usePreview.ts";
import { type RFNode } from "../../../types/react-flow.ts";
import VectorDatabasePreviewDialog from "./VectorDatabasePreviewDialog.tsx";

export type VectorDatabasePreviewNode = RFNode<"database-preview">;

const VectorDatabasePreviewNode = memo<NodeProps<VectorDatabasePreviewNode>>(
  function VectorDatabasePreviewNode() {
    const { strategy } = usePipelineSettings();
    const embeddingRecordsQuery = usePreview(strategy);

    const nodeState = useMemo(
      function getNodeState() {
        return {
          locked: embeddingRecordsQuery.isError,
          active: embeddingRecordsQuery.isLoading,
          valid: embeddingRecordsQuery.isSuccess,
        };
      },
      [
        embeddingRecordsQuery.isError,
        embeddingRecordsQuery.isLoading,
        embeddingRecordsQuery.isSuccess,
      ]
    );
    useUpdateNodeState(nodeState);

    return (
      <NodeBase>
        <NodeHeader
          title="Vector Database"
          blogLinkHref="https://pixion.co/blog/choosing-a-vector-database-when-working-with-rag"
          blogTitle="Choosing a Vector Database when Working with RAG"
        >
          <VectorDatabasePreviewDialog
            embeddingRecords={embeddingRecordsQuery.data}
            loading={embeddingRecordsQuery.isLoading}
          />
        </NodeHeader>
        <NodeHandleContainer>
          <NodeHandle position={Position.Left} type="target" id="embeddings" />
          <NodeHandle position={Position.Left} type="target" id="chunks" />
          <NodeHandle position={Position.Left} type="target" id="metadata" />
        </NodeHandleContainer>
      </NodeBase>
    );
  }
);

export default VectorDatabasePreviewNode;
