import type { ChangeEvent, HTMLAttributes } from "react";

export type InputType = "text" | "number" | "password" | "email";

export interface InputProps
  extends HTMLAttributes<HTMLInputElement | HTMLTextAreaElement> {
  type?: InputType;
  value: string | number;
  onChange: (
    event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
  ) => void;
  withCounter?: boolean;
  placeholder: string;
  required?: boolean;
  width?: string;
  caption?: string;
  label: string;
  hasError?: boolean;
  maxLength?: number;
  multiline?: boolean;
}
