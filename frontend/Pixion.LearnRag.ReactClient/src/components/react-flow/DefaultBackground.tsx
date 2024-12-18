import { Background, BackgroundVariant } from "@xyflow/react";
import { memo } from "react";
import { customColors } from "../../../tailwind.config";

const DefaultBackground = memo(function DefaultBackground() {
  return (
    <Background
      variant={BackgroundVariant.Dots}
      gap={24}
      size={2}
      color={customColors.brand[500]}
      className="opacity-80"
    />
  );
});

export default DefaultBackground;
