import { Cell, Column, Row, Table, TableBody, TableHeader } from "react-aria-components";
import { type BasicMetadataEmbeddingRecord } from "../../../api";
import { TableEmptyState } from "../../../components/TableEmptyState";
import { toPrettyJSON } from "../../../helpers/format";
import CodeBlock from "../../../lib/highlightjs/CodeBlock";
import { type EmbeddingRecords } from "../../../types/rag";
import { type TableColumn } from "../../../types/react-aria";

export type EmbeddingRecordTableProps = {
  loading: boolean;
  embeddingRecords?: EmbeddingRecords;
};

function EmbeddingRecordTable({ loading, embeddingRecords }: EmbeddingRecordTableProps) {
  return (
    <div className="nodrag nowheel grid w-full overflow-auto text-sm">
      <Table
        aria-label="Embedding Records"
        className="w-full table-auto border-separate border-spacing-0 bg-white"
      >
        <TableHeader columns={embeddingRecordColumns}>
          {column => (
            <Column className="bg-neutral-700 p-2 text-white" isRowHeader={column.isRowHeader}>
              {column.name}
            </Column>
          )}
        </TableHeader>
        <TableBody
          items={embeddingRecords}
          renderEmptyState={() => <TableEmptyState loading={loading} />}
        >
          {item => (
            <Row
              className="cursor-default overflow-hidden p-2 odd:bg-neutral-100 even:bg-neutral-50 even:text-neutral-700"
              columns={embeddingRecordColumns}
            >
              {column => {
                if (column.id === "metadata") {
                  const display = toPrettyJSON(item[column.id]!);

                  return (
                    <Cell className="p-2">
                      <CodeBlock code={display} language="json" />
                    </Cell>
                  );
                } else if (column.id === "embedding") {
                  const display = `[${item[column.id]!.join(", ")}]`;

                  return (
                    <Cell className="p-2">
                      <span className="line-clamp-5 max-w-36 text-ellipsis">{display}</span>
                    </Cell>
                  );
                } else if (column.id === "text") {
                  const display = item[column.id] as string;

                  return (
                    <Cell className="p-2">
                      <span title={display} className="line-clamp-5 text-ellipsis">
                        {display}
                      </span>
                    </Cell>
                  );
                }
                // id
                const display = item[column.id] as string;

                return (
                  <Cell className="p-2 text-xs">
                    <span>{display}</span>
                  </Cell>
                );
              }}
            </Row>
          )}
        </TableBody>
      </Table>
    </div>
  );
}

const embeddingRecordColumns: TableColumn<BasicMetadataEmbeddingRecord>[] = [
  {
    id: "id",
    name: "ID",
    isRowHeader: true,
  },
  {
    id: "text",
    name: "Text",
  },
  {
    id: "embedding",
    name: "Embedding",
  },
  {
    id: "metadata",
    name: "Metadata",
  },
] as const;

export default EmbeddingRecordTable;
