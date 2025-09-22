import type { StatusTypes } from "../../../types/types";

export const TOASTER_SIZES = {
  SMALL: "small",
  LARGE: "large",
};

export type ToasterSize = (typeof TOASTER_SIZES)[keyof typeof TOASTER_SIZES];

export type ShowToasterProps = (
  contentText: string,
  type: StatusTypes,
  size: ToasterSize,
) => void;

export interface ToasterProps {
  duration?: number;
  isSticky?: boolean;
  isGlobal?: boolean;
  className?: string;
}

export interface ToasterHandler {
  show: ShowToasterProps;
  close: () => void;
}
