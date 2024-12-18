import Help from "@/assets/icons/eye.svg?react";
import { memo } from "react";
import DialogBody from "../../../components/dialog/DialogBody.tsx";
import DialogHeader from "../../../components/dialog/DialogHeader.tsx";
import NodeDialog from "../../../components/react-flow/NodeDialog.tsx";
import SearchResultTable, {
  type SearchResultTableProps,
} from "../user-query-input-node/SearchResultTable.tsx";

type VectorDatabaseSearchDialogProps = SearchResultTableProps;

const VectorDatabaseSearchDialog = memo<VectorDatabaseSearchDialogProps>(
  function VectorDatabaseSearchDialog(props) {
    return (
      <NodeDialog
        tooltip="View the result of vector database search"
        Icon={Help}
        disabled={!props.searchResults}
      >
        <DialogHeader closeable>Vector database search results</DialogHeader>
        <DialogBody unpadded>
          <SearchResultTable {...props} />
        </DialogBody>
      </NodeDialog>
    );
  }
);

export default VectorDatabaseSearchDialog;
