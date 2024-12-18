import { Panel, ReactFlow, useEdgesState, useNodesState } from "@xyflow/react";
import DefaultBackground from "../../components/react-flow/DefaultBackground.tsx";
import { edgeTypes } from "../../constants/react-flows.ts";
import usePipelineSettings from "../../hooks/usePipelineSettings.ts";
import useApplicationSettings from "../../stores/application-settings-store.ts";
import useReactFlowSettings from "../../stores/react-flow-settings-store.ts";
import { type RAGEdge } from "../../types/react-flow.ts";
import { initialRetrievalEdges, initialRetrievalNodes, retrievalNodeTypes } from "./constants.ts";
import { type RetrievalNode } from "./types.ts";

export default function RetrievalPipeline() {
  const { strategy } = usePipelineSettings();
  const canvasControls = useApplicationSettings(state => state.canvasControls);
  const pipelineData = useReactFlowSettings(state => state[strategy].retrievalPipelineData);
  const setRFInstance = useReactFlowSettings(state => state.setRetrievalRFInstance);

  const [nodes, , onNodesChange] = useNodesState<RetrievalNode>(
    pipelineData?.nodes || initialRetrievalNodes
  );
  const [edges, , onEdgesChange] = useEdgesState<RAGEdge>(
    pipelineData?.edges || initialRetrievalEdges
  );

  return (
    <ReactFlow
      id="retrieval"
      className="size-full bg-white"
      // Fit view and defaultViewport are exclusive
      fitView={!pipelineData?.viewport}
      defaultViewport={pipelineData?.viewport}
      panOnScroll={canvasControls === "design"}
      selectionOnDrag={canvasControls === "design"}
      onInit={instance => {
        setRFInstance(strategy, instance);
      }}
      proOptions={{ hideAttribution: true }}
      // node
      nodeTypes={retrievalNodeTypes}
      nodes={nodes}
      onNodesChange={onNodesChange}
      // edge
      edgeTypes={edgeTypes}
      edges={edges}
      onEdgesChange={onEdgesChange}
      edgesFocusable={false}
    >
      <Panel
        position="top-left"
        className="m-1 rounded-lg border border-neutral-700 bg-white p-2 text-xs"
      >
        Data Retrieval Pipeline
      </Panel>
      <DefaultBackground />
    </ReactFlow>
  );
}
