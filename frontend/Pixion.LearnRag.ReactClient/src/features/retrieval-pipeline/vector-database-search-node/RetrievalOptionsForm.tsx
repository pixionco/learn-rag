import { memo, useCallback } from "react";
import { Form } from "react-aria-components";
import { Button } from "../../../components/Button";
import { NumberInput } from "../../../components/form/NumberInput";
import usePipelineSettings from "../../../hooks/usePipelineSettings";
import useRAGOptions from "../../../stores/rag-options-store";
import {
  type AutoMergingRetrievalOptions,
  type HierarchicalRetrievalOptions,
  type SentenceWindowRetrievalOptions,
} from "../../../types/rag";

type RetrievalConfigurationFormProps = {
  isSearching: boolean;
};

type RetrievalConfigurationFormValues = {
  limit: number;
  range?: number;
  childLimit?: number;
  childParentPrevalenceFactor?: number;
};

const RetrievalConfigurationForm = memo<RetrievalConfigurationFormProps>(
  function RetrievalConfigurationForm({ isSearching }) {
    const { strategy } = usePipelineSettings();
    const retrievalOptions = useRAGOptions(state => state[strategy].retrievalOptions);
    const userQuery = useRAGOptions(state => state[strategy].userQuery);
    const setRetrievalOptions = useRAGOptions(state => state.setRetrievalOptions);

    const handleSubmit = useCallback(
      function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
        e.preventDefault();
        const data = Object.fromEntries(
          new FormData(e.currentTarget)
        ) as unknown as RetrievalConfigurationFormValues;
        setRetrievalOptions(strategy, data);
      },
      [setRetrievalOptions, strategy]
    );

    return (
      <Form className="flex flex-col items-end gap-6 p-4" onSubmit={handleSubmit}>
        <NumberInput
          name="limit"
          label="Limit"
          defaultValue={retrievalOptions?.limit}
          minValue={1}
          maxValue={6}
          isRequired
          isDisabled={!userQuery}
          description={
            strategy === "hierarchical"
              ? "The number of retrieved chunks on the first level of hierarchy."
              : strategy === "auto-merging"
                ? "The number of retrieved chunks on the last level of hierarchy."
                : "The number of retrieved chunks."
          }
        />
        {strategy === "sentence-window" && (
          <NumberInput
            minValue={1}
            maxValue={6}
            name="range"
            label="Range"
            isRequired
            isDisabled={!userQuery}
            defaultValue={(retrievalOptions as Partial<SentenceWindowRetrievalOptions>)?.range}
            description="The range of the sentence window. It describes the number of neighbouring sentences that are added the the original sentence's content."
          />
        )}
        {strategy === "hierarchical" && (
          <NumberInput
            name="childLimit"
            label="Child Limit"
            minValue={1}
            maxValue={2}
            isRequired
            isDisabled={!userQuery}
            defaultValue={(retrievalOptions as Partial<HierarchicalRetrievalOptions>)?.childLimit}
            description="The number of the retrived chunks on the second level of hierarchy per parent on the first level of the hierarchy."
          />
        )}
        {strategy === "auto-merging" && (
          <NumberInput
            formatOptions={{
              minimumFractionDigits: 2,
              maximumFractionDigits: 2,
            }}
            step={0.05}
            minValue={0.4}
            maxValue={1.0}
            isRequired
            isDisabled={!userQuery}
            name="childParentPrevalenceFactor"
            label="Child Parent Prevalence Factor"
            defaultValue={
              (retrievalOptions as Partial<AutoMergingRetrievalOptions>)
                ?.childParentPrevalenceFactor
            }
            description="Child parent prevalence factor represents the threshold needed that the child chunks merge into the parent chunk."
          />
        )}
        <Button type="submit" isDisabled={isSearching || !userQuery} loading={isSearching}>
          Save
        </Button>
      </Form>
    );
  }
);

export default RetrievalConfigurationForm;
