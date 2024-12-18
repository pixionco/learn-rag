import {
  Button,
  FieldError,
  Group,
  Input,
  Label,
  NumberField,
  type NumberFieldProps,
  Text,
  type ValidationResult,
} from "react-aria-components";

interface NumberInputProps extends Omit<NumberFieldProps, "children" | "className"> {
  label?: string;
  description?: string;
  errorMessage?: string | ((validation: ValidationResult) => string);
}

export function NumberInput({ label, description, errorMessage, ...props }: NumberInputProps) {
  return (
    <NumberField {...props} className="nodrag flex w-full flex-col gap-2">
      <Label className="font-medium text-neutral-800">{label}:</Label>
      <Group className="flex w-full items-center ">
        <Button
          className="rounded-l-md border-r border-neutral-100 bg-white p-2 text-neutral-800 disabled:cursor-not-allowed disabled:text-neutral-400"
          slot="decrement"
        >
          -
        </Button>
        <Input className="flex w-full gap-4 bg-white p-2 text-neutral-700 disabled:cursor-not-allowed" />
        <Button
          className="rounded-r-md border-l border-neutral-100 bg-white p-2 text-neutral-800 disabled:cursor-not-allowed disabled:text-neutral-400"
          slot="increment"
        >
          +
        </Button>
      </Group>
      {description && (
        <Text className="text-sm text-neutral-400" slot="description">
          ⓘ {description}
        </Text>
      )}
      <FieldError className="flex items-center gap-1 text-sm text-state-invalid before:content-['*']">
        {errorMessage}
      </FieldError>
    </NumberField>
  );
}
