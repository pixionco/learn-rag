import { Position, type NodeProps } from "@xyflow/react";
import { memo, useMemo } from "react";
import NodeBase from "../../../components/react-flow/NodeBase";
import NodeHandle from "../../../components/react-flow/NodeHandle";
import NodeHandleContainer from "../../../components/react-flow/NodeHandleContainer";
import NodeHeader from "../../../components/react-flow/NodeHeader";
import usePipelineSettings from "../../../hooks/usePipelineSettings";
import { useUpdateNodeState } from "../../../hooks/useUpdateNodeState";
import useAnswerTemplate from "../../../queries/useAnswerTemplate";
import useSearch from "../../../queries/useSearch";
import useRAGOptions from "../../../stores/rag-options-store";
import { type RFNode } from "../../../types/react-flow";
import PrompteTemplatePreviewDialog from "./PrompteTemplatePreviewDialog";

export type PromptTemplateNode = RFNode<"prompt-template">;

const PromptTemplateNode = memo<NodeProps<PromptTemplateNode>>(function PromptTemplateNode() {
  const { strategy } = usePipelineSettings();
  const userQuery = useRAGOptions(state => state[strategy].userQuery)!;

  const searchQuery = useSearch(strategy, userQuery!);
  const templateQuery = useAnswerTemplate();

  const nodeState = useMemo(
    function getNodeState() {
      return {
        locked: searchQuery.isError,
        active: searchQuery.isLoading,
        valid: searchQuery.isSuccess,
      };
    },
    [searchQuery.isError, searchQuery.isLoading, searchQuery.isSuccess]
  );
  useUpdateNodeState(nodeState);

  return (
    <NodeBase>
      <NodeHeader
        title="Templating Engine"
        blogLinkHref="https://pixion.co/blog/llm-prompt-optimization"
        blogTitle="LLM Prompt Optimization"
      >
        <PrompteTemplatePreviewDialog
          template={templateQuery.data}
          searchResults={searchQuery.data}
          userQuery={userQuery}
        />
      </NodeHeader>
      <section className="flex min-w-[500px] max-w-[500px] flex-col divide-y p-4">
        <p className="whitespace-pre-wrap">
          {templateQuery.data?.templateString ?? "Loading template..."}
        </p>
      </section>
      <NodeHandleContainer>
        <NodeHandle position={Position.Left} type="target" id="embedding-records" />
        <NodeHandle position={Position.Right} type="source" id="query" payload="Query" />
      </NodeHandleContainer>
    </NodeBase>
  );
});

export default PromptTemplateNode;
