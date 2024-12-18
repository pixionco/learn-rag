import Help from "@/assets/icons/eye.svg?react";
import { memo, useMemo } from "react";
import { type AnswerPromptTemplate } from "../../../api/index.ts";
import DialogBody from "../../../components/dialog/DialogBody.tsx";
import DialogHeader from "../../../components/dialog/DialogHeader.tsx";
import NodeDialog from "../../../components/react-flow/NodeDialog.tsx";
import { type SearchResults } from "../../../types/rag.ts";

type PrompteTemplatePreviewDialogProps = {
  userQuery: string;
  searchResults?: SearchResults;
  template?: AnswerPromptTemplate;
};

const PrompteTemplatePreviewDialog = memo<PrompteTemplatePreviewDialogProps>(
  function PrompteTemplatePreviewDialog({ template, userQuery, searchResults }) {
    const interpolatedTemplate = useMemo(() => {
      if (template && template.templateString && searchResults) {
        const strings = [];

        const contextMarker = `{{$${template.context!}}}`;
        const contextIndex = template.templateString.indexOf(contextMarker);

        const questionMarker = `{{$${template.question!}}}`;
        const questionIndex = template.templateString.indexOf(questionMarker);

        strings.push(
          <span key="before-context">{template.templateString.slice(0, contextIndex)}</span>
        );
        strings.push(
          <span className="text-brand-700" key="context">
            {searchResults.map(sr => sr.text).join("\n")}
          </span>
        );
        strings.push(
          <span key="after-context-before-question">
            {template.templateString.slice(contextIndex + contextMarker.length, questionIndex)}
          </span>
        );
        strings.push(
          <span key="question" className="text-brand-700">
            {userQuery}
          </span>
        );
        strings.push(
          <span key="after-question">
            {template.templateString.slice(questionIndex + questionMarker.length)}
          </span>
        );

        return strings;
      }
    }, [template, searchResults, userQuery]);

    return (
      <NodeDialog
        tooltip="View the full template with interpolated values"
        Icon={Help}
        disabled={!interpolatedTemplate}
      >
        <DialogHeader closeable>Interpolated prompt template preview</DialogHeader>
        <DialogBody>
          <p className="nowheel max-w-prose overflow-y-scroll whitespace-pre-wrap">
            {interpolatedTemplate}
          </p>
        </DialogBody>
      </NodeDialog>
    );
  }
);

export default PrompteTemplatePreviewDialog;
