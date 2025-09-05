import type { HTMLAttributes, ReactNode } from "react";
import type { ButtonProps } from "../button/types";
import type {
  ICON_NAMES,
  FORM_WRAPPER_CONTENT_PROPS,
  FORM_CONTENT_MAP,
} from "./utils";

export type IconName = (typeof ICON_NAMES)[keyof typeof ICON_NAMES];

export interface FormWrapperProps extends HTMLAttributes<HTMLDivElement> {
  formKey: FormKey;
  subDescription?: ReactNode;
  iconName: IconName;
  buttonProps: ButtonProps;
  isBackButtonVisible?: boolean;
  onBack?: () => void;
  children: ReactNode;
}

export type FormWrapperContentKeys =
  (typeof FORM_WRAPPER_CONTENT_PROPS)[keyof typeof FORM_WRAPPER_CONTENT_PROPS];

export type FormWrapperContentProps = Pick<
  FormWrapperProps,
  FormWrapperContentKeys
>;

export type FormKey = keyof typeof FORM_CONTENT_MAP;
