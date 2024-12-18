export type TutorialStep = "introduction" | "graph" | "information" | "close";

export type TutorialStepProps = {
  setStep: (string: TutorialStep) => void;
  seenTutorial: boolean;
};
