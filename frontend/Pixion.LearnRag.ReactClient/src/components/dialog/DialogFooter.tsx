import { memo, type PropsWithChildren } from "react";

const DialogFooter = memo<PropsWithChildren>(function DialogFooter({ children }) {
  return (
    <footer className="flex shrink-0 items-center gap-4 border-t border-t-brand-950 p-4">
      {children}
    </footer>
  );
});

export default DialogFooter;
