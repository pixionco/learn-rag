import { type NodeProps, Position } from "@xyflow/react";
import { memo, useMemo } from "react";
import NodeBase from "../../../components/react-flow/NodeBase.tsx";
import NodeHandle from "../../../components/react-flow/NodeHandle.tsx";
import NodeHandleContainer from "../../../components/react-flow/NodeHandleContainer.tsx";
import NodeHeader from "../../../components/react-flow/NodeHeader.tsx";
import { checkRetrievalOptionsValid } from "../../../helpers/rag.ts";
import usePipelineSettings from "../../../hooks/usePipelineSettings.ts";
import { useUpdateNodeState } from "../../../hooks/useUpdateNodeState.ts";
import useSearch from "../../../queries/useSearch.ts";
import useRAGOptions from "../../../stores/rag-options-store.ts";
import { type RFNode } from "../../../types/react-flow.ts";
import RetrievalOptionsForm from "./RetrievalOptionsForm.tsx";
import VectorDatabaseSearchDialog from "./VectorDatabaseSearchDialog.tsx";

export type VectorDatabaseSearchNode = RFNode<"vector-search">;

const VectorDatabaseSearchNode = memo<NodeProps<VectorDatabaseSearchNode>>(
  function VectorDatabaseSearchNode() {
    const { strategy } = usePipelineSettings();
    const userQuery = useRAGOptions(state => state[strategy].userQuery);
    const retrievalOptions = useRAGOptions(state => state[strategy].retrievalOptions);
    const areRetrievalOptionsValid = checkRetrievalOptionsValid(strategy, retrievalOptions);

    const searchQuery = useSearch(strategy, userQuery!);

    const nodeState = useMemo(
      function getNodeState() {
        return {
          locked: searchQuery.isError,
          active: searchQuery.isLoading || !areRetrievalOptionsValid,
          valid: searchQuery.isSuccess,
        };
      },
      [areRetrievalOptionsValid, searchQuery.isError, searchQuery.isLoading, searchQuery.isSuccess]
    );
    useUpdateNodeState(nodeState);

    return (
      <NodeBase>
        <NodeHeader
          title="Vector Database"
          blogLinkHref="https://pixion.co/blog/choosing-your-index-with-pg-vector-flat-vs-hnsw-vs-ivfflat"
          blogTitle="Choosing your Index with PGVector: Flat vs HNSW vs IVFFlat"
        >
          <VectorDatabaseSearchDialog
            searchResults={searchQuery.data}
            loading={searchQuery.isLoading}
          />
        </NodeHeader>
        <section className="flex min-w-[400px] max-w-[400px] flex-col gap-6">
          <RetrievalOptionsForm isSearching={searchQuery.isFetching} />
        </section>
        <NodeHandleContainer>
          <NodeHandle position={Position.Left} type="target" id="embedding" />
          <NodeHandle
            position={Position.Right}
            type="source"
            id="embedding-records"
            payload="Embedding Records"
          />
        </NodeHandleContainer>
      </NodeBase>
    );
  }
);

export default VectorDatabaseSearchNode;
