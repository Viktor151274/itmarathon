export interface InfoCardProps {
  title: string;
  description: string;
  iconName: IconName;
  width?: string;
}

export const ICON_NAMES = {
  STAR: "star",
  PRESENTS: "presents",
} as const;

export type IconName = (typeof ICON_NAMES)[keyof typeof ICON_NAMES];
