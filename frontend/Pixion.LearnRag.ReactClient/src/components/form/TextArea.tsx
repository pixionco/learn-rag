import {
  TextArea as AriaTextArea,
  FieldError,
  Label,
  Text,
  TextField,
  type TextFieldProps,
  type ValidationResult,
} from "react-aria-components";

interface TextAreaProps extends Omit<TextFieldProps, "children" | "className"> {
  label: string;
  description?: string;
  errorMessage?: string | ((validation: ValidationResult) => string);
  rows?: number;
  placeholder?: string;
}

export function TextArea({
  label,
  description,
  errorMessage,
  rows = 4,
  placeholder,
  ...props
}: TextAreaProps) {
  return (
    <TextField {...props} className="nodrag flex w-full flex-col gap-2">
      <Label className="font-medium text-neutral-800">{label}:</Label>
      <AriaTextArea
        placeholder={placeholder}
        className="nodrag nowheel w-full rounded-md px-2 py-1 text-neutral-700 placeholder:text-neutral-500 disabled:cursor-not-allowed disabled:bg-white disabled:placeholder:text-neutral-300"
        rows={rows}
      />
      {description && (
        <Text className="text-sm text-neutral-400" slot="description">
          ⓘ {description}
        </Text>
      )}
      <FieldError className="flex items-center gap-1 text-sm text-state-invalid before:content-['*']">
        {errorMessage}
      </FieldError>
    </TextField>
  );
}
