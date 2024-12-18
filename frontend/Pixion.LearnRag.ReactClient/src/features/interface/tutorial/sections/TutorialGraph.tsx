import Back from "@/assets/icons/back.svg?react";
import { default as Next } from "@/assets/icons/next.svg?react";
import { Button } from "react-aria-components";
import DialogBody from "../../../../components/dialog/DialogBody.tsx";
import DialogFooter from "../../../../components/dialog/DialogFooter.tsx";
import DialogHeader from "../../../../components/dialog/DialogHeader.tsx";
import { type TutorialStepProps } from "../types.ts";

function TutorialGraph({ setStep, seenTutorial }: TutorialStepProps) {
  return (
    <>
      <DialogHeader closeable={seenTutorial}>How the Application Works</DialogHeader>
      <DialogBody>
        <p>Most interactions within the application occur through the pipeline graphs.</p>
        <p>
          The interaction flow starts with the first node. When it becomes active, indicated by
          green highlights, it enables interaction with subsequent nodes.
        </p>
        <p>
          The data retrieval pipeline cannot be accessed until the data ingestion pipeline is
          completed. This is because the application demonstrates a data flow: the data retrieval
          pipeline is restriced to searching only the data processed by the preceding data ingestion
          pipeline.
        </p>
        <p>
          Please note that no real-time data ingestion occurs. All data ingestion was pre-processed
          in advance.
        </p>
      </DialogBody>
      <DialogFooter>
        <Button
          className="flex w-full items-center justify-center gap-1 rounded-md bg-brand-500 p-2 text-white"
          onPress={() => setStep("introduction")}
        >
          <Back className="size-5 fill-white" aria-hidden />
          <span>Back</span>
        </Button>
        <Button
          className="flex w-full items-center justify-center gap-1 rounded-md bg-brand-500 p-2 text-white"
          onPress={() => setStep("information")}
        >
          <span>Next</span>
          <Next className="size-5 fill-white" aria-hidden />
        </Button>
      </DialogFooter>
    </>
  );
}

export default TutorialGraph;
