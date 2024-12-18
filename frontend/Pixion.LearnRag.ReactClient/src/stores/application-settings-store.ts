import { create } from "zustand";

export type CanvasDirection = "row" | "col";
export type CanvasControls = "slippy-map" | "design";
export type Pipeline = "ingestion" | "retrieval";

type ApplicationSettingsState = {
  layoutDirection: CanvasDirection;
  canvasControls: CanvasControls;
} & Record<
  Pipeline,
  {
    minimized: boolean;
  }
>;

type ApplicationSettingsActions = {
  setLayoutDirection: (direction: CanvasDirection) => void;
  setCanvasControls: (controls: CanvasControls) => void;
  toggleMinimized: (pipeline: Pipeline) => void;
};

type ApplicationSettingsStore = ApplicationSettingsState & ApplicationSettingsActions;

const useApplicationSettings = create<ApplicationSettingsStore>((set, get) => ({
  layoutDirection: "col",
  canvasControls: "slippy-map",
  ingestion: {
    minimized: false,
  },
  retrieval: {
    minimized: false,
  },

  setLayoutDirection(direction) {
    set({ layoutDirection: direction });
  },
  setCanvasControls(controls) {
    set({ canvasControls: controls });
  },
  toggleMinimized(currentPipeline) {
    set(({ [currentPipeline]: { minimized } }) => {
      const otherPipeline = getOtherPipeline(currentPipeline);
      const currentValue = get()[currentPipeline].minimized;
      const otherValue = get()[otherPipeline].minimized;

      // dont allow both pipelines minimized
      if (!currentValue && otherValue) {
        return {
          [currentPipeline]: {
            minimized: true,
          },
          [otherPipeline]: {
            minimized: false,
          },
        };
      }

      return {
        [currentPipeline]: {
          minimized: !minimized,
        },
      };
    });
  },
}));

function getOtherPipeline(pipeline: Pipeline): Pipeline {
  if (pipeline === "ingestion") return "retrieval";
  return "ingestion";
}

export default useApplicationSettings;
