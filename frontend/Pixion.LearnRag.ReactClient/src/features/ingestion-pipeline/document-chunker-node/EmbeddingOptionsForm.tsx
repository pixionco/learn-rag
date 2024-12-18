import { type UseQueryResult } from "@tanstack/react-query";
import { memo, useCallback } from "react";
import { Form } from "react-aria-components";
import {
  type AutoMergingEmbeddingOptions,
  type BasicEmbeddingOptions,
  type HierarchicalEmbeddingOptions,
  type HypotheticalQuestionEmbeddingOptions,
  type SentenceWindowEmbeddingOptions,
} from "../../../api/index.ts";
import { Button } from "../../../components/Button.tsx";
import { Select } from "../../../components/form/Select.tsx";
import usePipelineSettings from "../../../hooks/usePipelineSettings.ts";
import useRAGOptions from "../../../stores/rag-options-store.ts";
import { type ValueOption } from "../../../types/form.ts";
import { type EmbeddingOptions } from "../../../types/rag.ts";

type EmbeddingOptionsFormValues = {
  embeddingOptions: string;
};

type EmbeddingOptionsFormProps = {
  embeddingOptionsQuery: UseQueryResult<
    | BasicEmbeddingOptions[]
    | SentenceWindowEmbeddingOptions[]
    | AutoMergingEmbeddingOptions[]
    | HierarchicalEmbeddingOptions[]
    | HypotheticalQuestionEmbeddingOptions[]
  >;
};

const EmbeddingOptionsForm = memo<EmbeddingOptionsFormProps>(function EmbeddingOptionsForm({
  embeddingOptionsQuery,
}) {
  const { strategy } = usePipelineSettings();
  const embeddingOptions = useRAGOptions(state => state[strategy].embeddingOptions);
  const documentId = useRAGOptions(state => state[strategy].documentId);
  const setEmbeddingOptions = useRAGOptions(state => state.setEmbeddingOptions);

  const embeddingOptionsOptions: ValueOption[] | undefined = embeddingOptionsQuery.data?.map(
    options => ({
      id: JSON.stringify(options),
      name: Object.entries(options)
        .map(([key, value]) => `${key}: ${value}`)
        .join(", "),
    })
  );

  const handleSubmit = useCallback(
    function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
      e.preventDefault();
      const data = Object.fromEntries(new FormData(e.currentTarget)) as EmbeddingOptionsFormValues;
      const options = JSON.parse(data.embeddingOptions) as Required<EmbeddingOptions>;
      setEmbeddingOptions(strategy, options);
    },
    [setEmbeddingOptions, strategy]
  );

  return (
    <Form onSubmit={handleSubmit} className="flex flex-col items-end gap-6">
      <Select
        isRequired
        name="embeddingOptions"
        label="Embedding Options"
        placeholder="Select an embedding options combination"
        items={embeddingOptionsOptions}
        defaultSelectedKey={JSON.stringify(embeddingOptions)}
        isPending={embeddingOptionsQuery.isLoading}
        isDisabled={embeddingOptionsQuery.isPending}
        description="Embedding options represent a unique combination that configures how the document is transformed into the chunks"
      />
      <Button type="submit" isDisabled={!documentId}>
        Save
      </Button>
    </Form>
  );
});

export default EmbeddingOptionsForm;
