import Down from "@/assets/icons/chevron-down.svg?react";
import { useViewport } from "@xyflow/react";
import {
  Select as AriaSelect,
  Button,
  FieldError,
  Label,
  ListBox,
  ListBoxItem,
  Popover,
  SelectValue,
  Text,
  type SelectProps as AriaSelectProps,
  type ValidationResult,
} from "react-aria-components";
import { twMerge } from "tailwind-merge";
import { type ValueOption } from "../../types/form";
import Spinner from "../Spinner";

interface SelectProps<T extends ValueOption>
  extends Omit<AriaSelectProps<T>, "children" | "className"> {
  label: string;
  description?: string;
  errorMessage?: string | ((validation: ValidationResult) => string);
  items?: Iterable<T>;
  isPending?: boolean;
}

export function Select<T extends ValueOption>({
  label,
  description,
  errorMessage,
  items,
  placeholder = "",
  isPending = false,
  ...props
}: SelectProps<T>) {
  const { zoom } = useViewport();

  return (
    <AriaSelect {...props} className="nodrag flex w-full flex-col gap-2" placeholder={placeholder}>
      <Label className="font-medium text-neutral-800">{label}:</Label>
      <Button
        className="flex justify-between gap-4 rounded-md bg-white p-2 disabled:cursor-not-allowed"
        isPending={isPending}
      >
        {({ isPending }) => (
          <>
            <SelectValue
              className={({ isPlaceholder }) =>
                twMerge("text-neutral-700", isPlaceholder && "text-neutral-500")
              }
            >
              {({ defaultChildren, isPlaceholder }) =>
                isPlaceholder ? (placeholder ? placeholder : undefined) : defaultChildren
              }
            </SelectValue>
            {isPending ? (
              <Spinner className="fill-neutral-700" aria-hidden="true" />
            ) : (
              <Down aria-hidden="true" />
            )}
          </>
        )}
      </Button>
      {description && (
        <Text className="text-sm text-neutral-400" slot="description">
          ⓘ {description}
        </Text>
      )}
      <FieldError className="flex items-center gap-1 text-sm text-state-invalid before:content-['*']">
        {errorMessage}
      </FieldError>
      <Popover
        className="w-[var(--trigger-width)] rounded-md bg-white px-2 py-4 shadow-xl"
        style={{
          transform: `scale(${zoom})`, // Scale to inverse zoom
          transformOrigin: "top left", // Ensure scaling aligns from the top-left corner
        }}
      >
        <ListBox items={items} className="flex w-full flex-col gap-2">
          {item => (
            <ListBoxItem
              value={item}
              className="w-full cursor-pointer rounded-md bg-white p-2 text-neutral-700 hover:bg-neutral-50 selected:bg-brand-50 selected:font-medium selected:text-brand-700"
            >
              {item.name}
            </ListBoxItem>
          )}
        </ListBox>
      </Popover>
    </AriaSelect>
  );
}
