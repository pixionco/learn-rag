import Help from "@/assets/icons/eye.svg?react";
import { memo } from "react";
import DialogBody from "../../../components/dialog/DialogBody.tsx";
import DialogHeader from "../../../components/dialog/DialogHeader.tsx";
import NodeDialog from "../../../components/react-flow/NodeDialog.tsx";
import EmbeddingRecordTable, { type EmbeddingRecordTableProps } from "./EmbeddingRecordTable.tsx";

type VectorDatabasePreviewDialogProps = EmbeddingRecordTableProps;

const VectorDatabasePreviewDialog = memo<VectorDatabasePreviewDialogProps>(
  function VectorDatabasePreviewDialog(props) {
    return (
      <NodeDialog
        tooltip="Preview the vector database"
        Icon={Help}
        disabled={!props.embeddingRecords}
      >
        <DialogHeader closeable>Vector Database Preview </DialogHeader>
        <DialogBody unpadded>
          <EmbeddingRecordTable {...props} />
        </DialogBody>
      </NodeDialog>
    );
  }
);

export default VectorDatabasePreviewDialog;
