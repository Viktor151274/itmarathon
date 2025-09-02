import { ICON_NAMES } from "../copy-button/utils";

export type IconName = (typeof ICON_NAMES)[keyof typeof ICON_NAMES];
export type ShowToasterProps = (
  iconName: IconName,
  contentText: string,
) => void;

export interface ToasterProps {
  duration?: number;
}

export interface ToasterHandler {
  show: ShowToasterProps;
}
