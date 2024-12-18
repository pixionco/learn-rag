import Back from "@/assets/icons/back.svg?react";
import Eye from "@/assets/icons/eye.svg?react";
import Next from "@/assets/icons/next.svg?react";
import { Button } from "react-aria-components";
import BlogLinkIcon from "../../../../components/blog-link/BlogLinkIcon.tsx";
import DialogBody from "../../../../components/dialog/DialogBody.tsx";
import DialogFooter from "../../../../components/dialog/DialogFooter.tsx";
import DialogHeader from "../../../../components/dialog/DialogHeader.tsx";
import { type TutorialStepProps } from "../types.ts";

function TutorialInformation({ setStep, seenTutorial }: TutorialStepProps) {
  return (
    <>
      <DialogHeader closeable={seenTutorial}>Acessing Additional information</DialogHeader>
      <DialogBody>
        <p>
          The
          <BlogLinkIcon
            className="mx-2"
            href="https://pixion.co/blog/introducing-pixion-blog-series-on-rag-llms"
          />
          icon, found in the application, indicates an external link to one of our blog posts, where
          the topic is discussed in greater detail.
        </p>
        <p>
          Additionally, the
          <span className="mx-1 rounded-full bg-brand-100 px-2 py-1">
            <Eye className="inline size-5 fill-brand-600" />
          </span>
          button, located on nodes, allows access to additional information about those nodes. This
          information becomes available once the node is valid.
        </p>
      </DialogBody>
      <DialogFooter>
        <Button
          className="flex w-full items-center justify-center gap-1 rounded-md bg-brand-500 p-2 text-white"
          onPress={() => setStep("graph")}
        >
          <Back className="size-5 fill-white" aria-hidden />
          Back
        </Button>
        <Button
          className="flex w-full items-center justify-center gap-1 rounded-md bg-brand-500 p-2 text-white"
          onPress={() => setStep("close")}
        >
          Next
          <Next className="size-5 fill-white" aria-hidden />
        </Button>
      </DialogFooter>
    </>
  );
}

export default TutorialInformation;
