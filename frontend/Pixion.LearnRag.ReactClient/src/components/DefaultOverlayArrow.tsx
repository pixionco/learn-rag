import { memo } from "react";
import { OverlayArrow } from "react-aria-components";
import { twJoin } from "tailwind-merge";

type DefaultOverlayArrowProps = {
  className: string;
};

const DefaultOverlayArrow = memo<DefaultOverlayArrowProps>(function DefaultOverlayArrow({
  className: fill,
}) {
  return (
    <OverlayArrow className={({ placement }) => twJoin(placement === "bottom" && "*:rotate-180")}>
      <svg width={12} height={12} viewBox="0 0 8 8" className={fill}>
        <path d="M0 0 L4 4 L8 0" />
      </svg>
    </OverlayArrow>
  );
});

export default DefaultOverlayArrow;
