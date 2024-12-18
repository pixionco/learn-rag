import { memo, useCallback } from "react";
import { Form } from "react-aria-components";
import { Button } from "../../../components/Button";
import { TextArea } from "../../../components/form/TextArea";
import { checkEmbeddingOptionsValid } from "../../../helpers/rag";
import usePipelineSettings from "../../../hooks/usePipelineSettings";
import useRAGOptions from "../../../stores/rag-options-store";

const UserQueryForm = memo(function UserQueryForm() {
  const { strategy } = usePipelineSettings();
  const userQuery = useRAGOptions(state => state[strategy].userQuery);
  const embeddingOptions = useRAGOptions(state => state[strategy].embeddingOptions);
  const setUserQuery = useRAGOptions(state => state.setUserQuery);
  const areEmbeddingOptionsValid = checkEmbeddingOptionsValid(strategy, embeddingOptions);

  const handleSubmit = useCallback(
    function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
      e.preventDefault();
      const data = Object.fromEntries(new FormData(e.currentTarget));

      if (data.userQuery) {
        setUserQuery(strategy, data.userQuery.toString());
      }
    },
    [setUserQuery, strategy]
  );

  return (
    <Form onSubmit={handleSubmit} className="flex flex-col items-end gap-6">
      <TextArea
        name="userQuery"
        label="User query"
        description="The question that is provided to the LLM"
        placeholder="Ask the LLM a question..."
        defaultValue={userQuery}
        isRequired
        isDisabled={!areEmbeddingOptionsValid}
      />
      <Button isDisabled={!areEmbeddingOptionsValid} type="submit">
        Save
      </Button>
    </Form>
  );
});

export default UserQueryForm;
