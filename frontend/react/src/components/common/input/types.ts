import type {
  ChangeEvent,
  InputHTMLAttributes,
  TextareaHTMLAttributes,
} from "react";

export type InputType = "text" | "number" | "password" | "email";

type InputTextareaProps = InputHTMLAttributes<HTMLInputElement> &
  TextareaHTMLAttributes<HTMLTextAreaElement>;

export interface InputProps extends InputTextareaProps {
  type?: InputType;
  value: string | number;
  onChange: (
    event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
  ) => void;
  withoutCounter?: boolean;
  withSuffix?: boolean;
  placeholder: string;
  required?: boolean;
  width?: string;
  caption?: string;
  label: string;
  hasError?: boolean;
  maxLength?: number;
  multiline?: boolean;
}
