import Help from "@/assets/icons/help.svg?react";
import { memo, useCallback, useState } from "react";
import { DialogTrigger } from "react-aria-components";
import DefaultDialog from "../../../components/dialog/DefaultDialog.tsx";
import { IconButton } from "../../../components/IconButton.tsx";
import TutorialClose from "./sections/TutorialClose.tsx";
import TutorialGraph from "./sections/TutorialGraph.tsx";
import TutorialInformation from "./sections/TutorialInformation.tsx";
import TutorialIntroduction from "./sections/TutorialIntroduction.tsx";
import { type TutorialStep } from "./types.ts";

const TutorialDialog = memo(function TutorialDialog() {
  const [seenTutorial, setSeenTutorial] = useState(!!localStorage.getItem("seenTutorial"));
  const [step, setStep] = useState<TutorialStep>("introduction");

  const closeTutorial = useCallback(function closeTutorial() {
    setStep("introduction");
    localStorage.setItem("seenTutorial", "true");
    setSeenTutorial(true);
  }, []);

  function tutorialStep() {
    switch (step) {
      case "introduction":
        return <TutorialIntroduction seenTutorial={seenTutorial} setStep={setStep} />;
      case "graph":
        return <TutorialGraph seenTutorial={seenTutorial} setStep={setStep} />;
      case "information":
        return <TutorialInformation seenTutorial={seenTutorial} setStep={setStep} />;
      case "close":
        return (
          <TutorialClose seenTutorial={seenTutorial} setStep={setStep} close={closeTutorial} />
        );
    }
  }

  return (
    <DialogTrigger defaultOpen={!seenTutorial}>
      <IconButton Icon={Help} />
      <DefaultDialog overlay isKeyboardDismissDisabled={!seenTutorial} proseWidth>
        {tutorialStep()}
      </DefaultDialog>
    </DialogTrigger>
  );
});

export default TutorialDialog;
