import { type NodeProps, Position } from "@xyflow/react";
import { memo, useMemo } from "react";
import NodeBase from "../../../components/react-flow/NodeBase";
import NodeHandle from "../../../components/react-flow/NodeHandle";
import NodeHandleContainer from "../../../components/react-flow/NodeHandleContainer";
import NodeHeader from "../../../components/react-flow/NodeHeader";
import usePipelineSettings from "../../../hooks/usePipelineSettings";
import { useUpdateNodeState } from "../../../hooks/useUpdateNodeState";
import useDocuments from "../../../queries/useDocuments";
import useRAGOptions from "../../../stores/rag-options-store";
import { type RFNode } from "../../../types/react-flow";
import DocumentForm from "./DocumentForm";
import DocumentPreviewDialog from "./DocumentPreviewDialog";

export type DocumentPickerNode = RFNode<"document-picker">;

const DocumentPickerNode = memo<NodeProps<DocumentPickerNode>>(
  function DocumentPickerNodeContent() {
    const { strategy } = usePipelineSettings();
    const documentId = useRAGOptions(state => state[strategy].documentId);
    const documentQuery = useDocuments();

    const preview = useMemo<string | undefined>(
      () => documentQuery.data?.find(option => option.id === documentId)?.text ?? undefined,
      [documentId, documentQuery.data]
    );

    const nodeState = useMemo(
      function getNodeState() {
        return {
          locked: documentQuery.isError,
          active: !documentId,
          valid: !!documentId,
        };
      },
      [documentId, documentQuery.isError]
    );
    useUpdateNodeState(nodeState);

    return (
      <NodeBase>
        <NodeHeader title="Document Picker">
          <DocumentPreviewDialog preview={preview} />
        </NodeHeader>
        <section className="min-w-[300px] max-w-[300px] p-4">
          <DocumentForm documentQuery={documentQuery} />
        </section>
        <NodeHandleContainer>
          <NodeHandle position={Position.Right} type="source" id="document" payload="Document" />
        </NodeHandleContainer>
      </NodeBase>
    );
  }
);

export default DocumentPickerNode;
