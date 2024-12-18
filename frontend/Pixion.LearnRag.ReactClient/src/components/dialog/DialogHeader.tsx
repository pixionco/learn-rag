import Close from "@/assets/icons/close.svg?react";
import { memo, type PropsWithChildren } from "react";
import { Button, Header, Heading } from "react-aria-components";

type DialogHeaderProps = PropsWithChildren<{
  closeable?: boolean;
}>;

const DialogHeader = memo<DialogHeaderProps>(function DialogHeader({ closeable, children }) {
  return (
    <Header className="flex shrink-0 items-center justify-between border-b border-b-neutral-900 p-4 pb-2 text-lg font-semibold text-neutral-800">
      <Heading slot="title">{children}</Heading>
      {closeable && (
        <Button slot="close" className="flex items-center justify-center">
          <Close className="fill-neutral-800" />
        </Button>
      )}
    </Header>
  );
});

export default DialogHeader;
