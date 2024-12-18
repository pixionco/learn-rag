import Back from "@/assets/icons/back.svg?react";
import Help from "@/assets/icons/help.svg?react";
import Settings from "@/assets/icons/settings.svg?react";
import { Button } from "react-aria-components";
import DialogBody from "../../../../components/dialog/DialogBody.tsx";
import DialogFooter from "../../../../components/dialog/DialogFooter.tsx";
import DialogHeader from "../../../../components/dialog/DialogHeader.tsx";
import { type TutorialStepProps } from "../types.ts";

function TutorialClose({
  setStep,
  seenTutorial,
  close,
}: TutorialStepProps & { close: () => void }) {
  return (
    <>
      <DialogHeader closeable={seenTutorial}>Menu</DialogHeader>
      <DialogBody>
        <p>
          The application configuration and tutorial can be found in the side menu, hidden on the
          left side. The menu can be opened by pressing the arrow button on the left side of the
          screen.
        </p>
        <p>
          Settings can be accessed via the&nbsp;
          <Settings className="inline size-5 fill-neutral-900" /> icon at the top of the side menu.
          The tutorial can be reopened at any time by pressing the&nbsp;
          <Help className="inline size-5 fill-neutral-900" /> icon bottom right corner.
        </p>
        <p>Happy experimenting!</p>
      </DialogBody>
      <DialogFooter>
        <Button
          className="flex w-full items-center justify-center gap-1 rounded-md bg-brand-500 p-2 text-white"
          onPress={() => setStep("information")}
        >
          <Back className="size-5 fill-white" aria-hidden />
          Back
        </Button>
        <Button
          className="flex w-full items-center justify-center gap-1 rounded-md bg-brand-500 p-2 text-white"
          onPress={close}
          slot="close"
        >
          Got it!
        </Button>
      </DialogFooter>
    </>
  );
}

export default TutorialClose;
