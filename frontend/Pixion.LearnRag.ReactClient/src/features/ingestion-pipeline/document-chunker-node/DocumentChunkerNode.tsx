import { type NodeProps, Position } from "@xyflow/react";
import { memo, useMemo } from "react";
import NodeBase from "../../../components/react-flow/NodeBase.tsx";
import NodeHandle from "../../../components/react-flow/NodeHandle.tsx";
import NodeHandleContainer from "../../../components/react-flow/NodeHandleContainer.tsx";
import NodeHeader from "../../../components/react-flow/NodeHeader.tsx";
import usePipelineSettings from "../../../hooks/usePipelineSettings.ts";
import { useUpdateNodeState } from "../../../hooks/useUpdateNodeState.ts";
import useEmbeddingOptions from "../../../queries/useDocumentEmbeddingOptions.ts";
import useRAGOptions from "../../../stores/rag-options-store.ts";
import { type RFNode } from "../../../types/react-flow.ts";
import EmbeddingOptionsForm from "./EmbeddingOptionsForm.tsx";

export type DocumentChunkerNode = RFNode<"document-chunker">;

const DocumentChunkerNode = memo<NodeProps<DocumentChunkerNode>>(function DocumentChunkerNode() {
  const { strategy } = usePipelineSettings();
  const embeddingOptions = useRAGOptions(state => state[strategy].embeddingOptions);
  const embeddingOptionsQuery = useEmbeddingOptions(strategy);

  const nodeState = useMemo(
    function getNodeState() {
      return {
        locked: embeddingOptionsQuery.isError,
        active: !embeddingOptions,
        valid: !!embeddingOptions,
      };
    },
    [embeddingOptions, embeddingOptionsQuery.isError]
  );
  useUpdateNodeState(nodeState);

  return (
    <NodeBase>
      <NodeHeader title="Document Chunker" />
      <section className="flex min-w-[500px] max-w-[500px] flex-col gap-6 p-4">
        <EmbeddingOptionsForm embeddingOptionsQuery={embeddingOptionsQuery} />
      </section>
      <NodeHandleContainer>
        <NodeHandle position={Position.Left} type="target" id="document" />
        <NodeHandle position={Position.Right} type="source" id="chunks" payload="Chunks" />
        <NodeHandle position={Position.Right} type="source" id="metadata" payload="Metadata" />
      </NodeHandleContainer>
    </NodeBase>
  );
});

export default DocumentChunkerNode;
