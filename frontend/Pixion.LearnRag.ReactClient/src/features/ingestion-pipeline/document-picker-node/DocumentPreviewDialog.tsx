import Help from "@/assets/icons/eye.svg?react";
import { memo } from "react";
import DialogBody from "../../../components/dialog/DialogBody.tsx";
import DialogHeader from "../../../components/dialog/DialogHeader.tsx";
import NodeDialog from "../../../components/react-flow/NodeDialog.tsx";

type DocumentPreviewDialogProps = {
  preview?: string;
};

const DocumentPreviewDialog = memo<DocumentPreviewDialogProps>(function DocumentPreviewDialog({
  preview,
}) {
  return (
    <NodeDialog tooltip="Preview the document" Icon={Help} disabled={!preview}>
      <DialogHeader closeable>Document Preview</DialogHeader>
      <DialogBody>
        <p className="nowheel max-w-prose overflow-y-scroll whitespace-pre-wrap">{preview}</p>
      </DialogBody>
    </NodeDialog>
  );
});

export default DocumentPreviewDialog;
