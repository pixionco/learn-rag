import { Position, type NodeProps } from "@xyflow/react";
import { memo, useMemo } from "react";
import ErrorMessage from "../../components/ErrorMessage";
import NodeBase from "../../components/react-flow/NodeBase";
import NodeHandle from "../../components/react-flow/NodeHandle";
import NodeHandleContainer from "../../components/react-flow/NodeHandleContainer";
import NodeHeader from "../../components/react-flow/NodeHeader";
import usePipelineSettings from "../../hooks/usePipelineSettings";
import { useUpdateNodeState } from "../../hooks/useUpdateNodeState";
import useGenerateAnswer from "../../queries/useGenerateAnswer";
import useSearch from "../../queries/useSearch";
import useRAGOptions from "../../stores/rag-options-store";
import { type RFNode } from "../../types/react-flow";

export type LLMAnswerNode = RFNode<"llm-answer">;

const LLMAnswerNode = memo<NodeProps<LLMAnswerNode>>(function LLMAnswerNode() {
  const { strategy } = usePipelineSettings();
  const userQuery = useRAGOptions(state => state[strategy].userQuery);

  const searchQuery = useSearch(strategy, userQuery!);
  const answerQuery = useGenerateAnswer(userQuery, searchQuery.data?.map(sr => sr.text).join("\n"));

  const nodeState = useMemo(
    function getNodeState() {
      return {
        locked: answerQuery.isError,
        active: answerQuery.isLoading,
        valid: answerQuery.isSuccess,
      };
    },
    [answerQuery.isError, answerQuery.isLoading, answerQuery.isSuccess]
  );
  useUpdateNodeState(nodeState);

  return (
    <NodeBase>
      <NodeHeader
        title="LLM"
        blogLinkHref="https://pixion.co/blog/rag-in-practice-answer-generation"
        blogTitle="RAG in practice - Answer Generation"
      />
      <section className="flex min-w-[500px] max-w-[500px] flex-col p-4">
        {answerQuery.isLoading && <p>Generating a response...</p>}
        {answerQuery.isSuccess && <p>{answerQuery.data}</p>}
        {answerQuery.isError && (
          <ErrorMessage
            message={
              answerQuery.error.status === 429
                ? "The ChatGPT server is overloaded. Please try again in few minutes."
                : "There is an error with the ChatGPT server."
            }
          ></ErrorMessage>
        )}
      </section>
      <NodeHandleContainer>
        <NodeHandle position={Position.Left} type="target" id="query" />
      </NodeHandleContainer>
    </NodeBase>
  );
});

export default LLMAnswerNode;
