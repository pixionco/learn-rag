import LongArrowRight from "@/assets/icons/long-arrow-right.svg?react";
import { memo } from "react";

type TextEmbeddingFlowProps = {
  text?: string;
  embedding?: number[];
};

const TextEmbeddingFlow = memo<TextEmbeddingFlowProps>(function TextEmbeddingFlow({
  text,
  embedding,
}) {
  return (
    <div className="flex w-full items-center justify-between gap-4">
      <p className="line-clamp-4 min-h-24 basis-full rounded-md border border-neutral-300 bg-white p-1">
        {text}
      </p>
      <LongArrowRight className="size-10 shrink-0 stroke-neutral-700 stroke-[1.5px]" />
      <p className="line-clamp-4 min-h-24 basis-full rounded-md border border-neutral-300 bg-white p-1">
        {embedding?.join(", ")}
      </p>
    </div>
  );
});

export default TextEmbeddingFlow;
