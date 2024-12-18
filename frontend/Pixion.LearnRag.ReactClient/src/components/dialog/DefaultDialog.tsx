import { type PropsWithChildren, memo } from "react";
import { type ModalOverlayProps, Dialog, Modal, ModalOverlay } from "react-aria-components";
import { twJoin } from "tailwind-merge";

type DefaultDialogProps = PropsWithChildren<
  Omit<ModalOverlayProps, "className"> & { overlay?: boolean; proseWidth?: boolean }
>;

const DefaultDialog = memo<DefaultDialogProps>(function DefaultDialog({
  children,
  overlay = false,
  proseWidth = false,
  ...props
}) {
  if (overlay) {
    return (
      <ModalOverlay
        {...props}
        className="fixed inset-0 flex h-screen w-screen flex-col items-center bg-neutral-700/30 px-2 py-16 backdrop-blur-sm md:px-4 md:py-36"
      >
        <Modal className={twJoin("w-full max-h-full", proseWidth ? "max-w-prose" : "max-w-full")}>
          <Dialog className="flex size-full flex-col  overflow-hidden rounded-xl border border-neutral-900 bg-white text-neutral-700 shadow-xl">
            {children}
          </Dialog>
        </Modal>
      </ModalOverlay>
    );
  }

  return (
    <Modal
      {...props}
      className="pointer-events-none fixed inset-0 flex h-screen w-screen flex-col items-center px-2 py-16 md:px-4 md:py-36"
    >
      <Dialog
        className={twJoin(
          "pointer-events-auto flex size-fit flex-col overflow-hidden rounded-xl border border-neutral-900 bg-white text-neutral-700 shadow-xl",
          proseWidth ? "max-w-prose" : "max-w-full"
        )}
      >
        {children}
      </Dialog>
    </Modal>
  );
});

export default DefaultDialog;
