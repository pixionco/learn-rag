import { BaseEdge, getBezierPath, useNodesData, useReactFlow, type EdgeProps } from "@xyflow/react";
import { memo, useEffect, useMemo } from "react";
import { twJoin } from "tailwind-merge";
import { type RAGEdge, type RAGNode, type RFEdge } from "../../types/react-flow";
import { type SVGRIcon } from "../../types/svgr";

export type PayloadEdge = RFEdge<
  "payload",
  {
    PayloadIcon?: SVGRIcon;
  }
>;

const PayloadEdge = memo<EdgeProps<PayloadEdge>>(function PayloadEdge({
  id,
  sourceX,
  sourceY,
  targetX,
  targetY,
  data,
  source,
}: EdgeProps<PayloadEdge>) {
  const [edgePath] = getBezierPath({ sourceX, sourceY, targetX, targetY });
  const { updateEdgeData } = useReactFlow<RAGNode, RAGEdge>();
  const sourceNode = useNodesData<RAGNode>(source);
  const selector = useMemo(() => id + "__payload", [id]);

  useEffect(() => {
    updateEdgeData(id, { state: sourceNode?.data.state });
  }, [id, sourceNode?.data.state, updateEdgeData]);

  useEffect(() => {
    const payload = document.getElementById(selector) as HTMLElement;
    if (!payload) return;
    if (!data || data.state !== "valid") return;

    const keyframes: Keyframe[] = [
      { offsetDistance: "0%" },
      { offsetDistance: "0%", offset: 0.1 },
      { offsetDistance: "100%", offset: 0.8 },
      { offsetDistance: "100%" },
    ];
    const animation = payload.animate(keyframes, {
      duration: 3000,
      direction: "normal",
      iterations: Infinity,
    });

    return () => {
      animation.cancel();
    };
  }, [data, selector]);

  return (
    <>
      <BaseEdge
        id={id}
        path={edgePath}
        className={twJoin(
          "stroke-2",
          data?.state === "valid" && "stroke-state-valid",
          data?.state === "active" && "stroke-state-active",
          data?.state === "locked" && "stroke-state-locked"
        )}
      />
      <g id={selector} style={{ offsetPath: `path('${edgePath}')` }}>
        {data?.PayloadIcon && data?.state === "valid" && (
          <>
            <circle className="fill-neutral-700" r={22} x={0} y={0}></circle>
            <circle className="fill-neutral-100" r={20} x={0} y={0}></circle>
            <g className=" -translate-x-3 -translate-y-3">
              <data.PayloadIcon className="fill-neutral-700 stroke-white" width={24} height={24} />
            </g>
          </>
        )}
      </g>
    </>
  );
});

export default PayloadEdge;
