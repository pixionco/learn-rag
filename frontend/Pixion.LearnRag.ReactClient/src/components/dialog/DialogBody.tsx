import { memo, type PropsWithChildren } from "react";
import { twJoin } from "tailwind-merge";

type DialogBodyProps = PropsWithChildren<{
  unpadded?: boolean;
}>;

const DialogBody = memo<DialogBodyProps>(function DialogBody({ children, unpadded = false }) {
  return (
    <div className={twJoin("flex flex-col gap-6 overflow-auto", !unpadded && "px-4 py-8 ")}>
      {children}
    </div>
  );
});

export default DialogBody;
