import { type UseQueryResult } from "@tanstack/react-query";
import { memo, useCallback } from "react";
import { Form } from "react-aria-components";
import { type Document } from "../../../api";
import { Button } from "../../../components/Button";
import { Select } from "../../../components/form/Select";
import usePipelineSettings from "../../../hooks/usePipelineSettings";
import useRAGOptions from "../../../stores/rag-options-store";
import { type ValueOption } from "../../../types/form";

type DocumentFormValues = {
  document: string;
};

type DocumentFormProps = {
  documentQuery: UseQueryResult<Document[]>;
};

const DocumentForm = memo<DocumentFormProps>(function DocumentForm({ documentQuery }) {
  const { strategy } = usePipelineSettings();
  const setDocumentId = useRAGOptions(state => state.setDocumentId);
  const documentId = useRAGOptions(state => state[strategy].documentId);

  const documentOptions: ValueOption[] | undefined = documentQuery.data?.map(doc => ({
    id: doc.id!,
    name: doc.name!,
  }));

  const handleSubmit = useCallback(
    function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
      e.preventDefault();
      const data = Object.fromEntries(new FormData(e.currentTarget)) as DocumentFormValues;
      setDocumentId(strategy, data.document);
    },
    [setDocumentId, strategy]
  );

  return (
    <Form onSubmit={handleSubmit} className="flex flex-col items-end gap-6">
      <Select
        isRequired
        defaultSelectedKey={documentId}
        name="document"
        placeholder="Select a document"
        label="Document"
        items={documentOptions}
        isPending={documentQuery.isLoading}
        isDisabled={documentQuery.isPending}
        description="The document that will be used by the RAG pipeline"
      />
      <Button type="submit">Save</Button>
    </Form>
  );
});

export default DocumentForm;
