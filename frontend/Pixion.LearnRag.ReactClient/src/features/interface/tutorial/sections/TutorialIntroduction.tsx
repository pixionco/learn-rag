import Next from "@/assets/icons/next.svg?react";
import { Button } from "react-aria-components";
import DialogBody from "../../../../components/dialog/DialogBody.tsx";
import DialogFooter from "../../../../components/dialog/DialogFooter.tsx";
import DialogHeader from "../../../../components/dialog/DialogHeader.tsx";
import { type TutorialStepProps } from "../types.ts";

function TutorialIntroduction({ setStep, seenTutorial }: TutorialStepProps) {
  return (
    <>
      <DialogHeader closeable={seenTutorial}>Introductory Tutorial</DialogHeader>
      <DialogBody>
        <p>Hello, welcome to the Pixion&apos;s Learn RAG Application.</p>
        <p>
          This application was developed for educational purposes. It provides a high-level
          visualization of how RAG pipelines operate and demonstrates how various strategies impact
          different aspects of the process.
        </p>
      </DialogBody>
      <DialogFooter>
        <Button
          className="flex w-full items-center justify-center gap-1 rounded-md bg-brand-500 p-2 text-white"
          onPress={() => setStep("graph")}
        >
          Next
          <Next className="size-5 fill-white" aria-hidden />
        </Button>
      </DialogFooter>
    </>
  );
}

export default TutorialIntroduction;
