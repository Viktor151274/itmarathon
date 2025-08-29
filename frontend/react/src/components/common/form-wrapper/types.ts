import type { HTMLAttributes, ReactNode } from "react";
import type { ButtonProps } from "../button/types";
import type { ICON_NAMES, FORM_WRAPPER_CONTENT_PROPS } from "./utils";

export type IconName = (typeof ICON_NAMES)[keyof typeof ICON_NAMES];

export interface FormWrapperProps extends HTMLAttributes<HTMLDivElement> {
  title: string;
  description: string;
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
