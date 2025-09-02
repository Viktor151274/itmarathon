import type { BUTTON_COLORS } from "./utils";

export type ButtonColor = (typeof BUTTON_COLORS)[keyof typeof BUTTON_COLORS];

export interface CopyButtonProps {
  textToCopy: string;
  buttonColor?: ButtonColor;
}
