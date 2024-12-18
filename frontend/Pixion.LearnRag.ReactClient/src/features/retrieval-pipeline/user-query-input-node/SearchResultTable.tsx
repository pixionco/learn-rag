import { Cell, Column, Row, Table, TableBody, TableHeader } from "react-aria-components";
import { type BasicMetadataSearchResult } from "../../../api";
import { TableEmptyState } from "../../../components/TableEmptyState";
import { toPrettyJSON } from "../../../helpers/format";
import CodeBlock from "../../../lib/highlightjs/CodeBlock";
import { type SearchResults } from "../../../types/rag";
import { type TableColumn } from "../../../types/react-aria";

export type SearchResultTableProps = {
  loading: boolean;
  searchResults?: SearchResults;
};

function SearchResultTable({ loading, searchResults }: SearchResultTableProps) {
  return (
    <div className="nodrag nowheel grid w-full overflow-auto text-sm">
      <Table
        aria-label="Search Results"
        className="w-full table-auto border-separate border-spacing-0 bg-white"
      >
        <TableHeader columns={searchResultColumns}>
          {column => (
            <Column className="bg-neutral-700 p-2 text-white" isRowHeader={column.isRowHeader}>
              {column.name}
            </Column>
          )}
        </TableHeader>
        <TableBody
          items={searchResults}
          renderEmptyState={() => <TableEmptyState loading={loading} />}
        >
          {item => (
            <Row
              className="cursor-default overflow-hidden p-2 odd:bg-neutral-100 even:bg-neutral-50 even:text-neutral-700"
              columns={searchResultColumns}
            >
              {column => {
                if (column.id === "metadata") {
                  const display = toPrettyJSON(item[column.id]!);

                  return (
                    <Cell className="p-2">
                      <CodeBlock code={display} language="json" />
                    </Cell>
                  );
                } else if (column.id === "text") {
                  const display = item[column.id]! as string;

                  return (
                    <Cell className="p-2">
                      <span title={display.toString()} className="line-clamp-5 text-ellipsis">
                        {display}
                      </span>
                    </Cell>
                  );
                } else if (column.id === "relevance") {
                  const display = item[column.id]! as number;

                  return (
                    <Cell className="p-2">
                      <span>{display.toFixed(5)}</span>
                    </Cell>
                  );
                }
                // id
                const display = item[column.id]! as string;

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

const searchResultColumns: TableColumn<BasicMetadataSearchResult>[] = [
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
    id: "relevance",
    name: "Relevance",
  },
  {
    id: "metadata",
    name: "Metadata",
  },
];

export default SearchResultTable;
