import type { DatePickerProps } from "antd";

export const InputNames = {
  ROOM_NAME: "name",
  ROOM_DESCRIPTION: "description",
  GIFT_BUDGET: "giftMaximumBudget",
} as const;

export type InputName = (typeof InputNames)[keyof typeof InputNames];

type DatePickerOnChange = NonNullable<DatePickerProps["onChange"]>;
export type DateType = Parameters<DatePickerOnChange>[0];

export interface FormData {
  name: string;
  description: string;
  giftMaximumBudget: string;
  giftExchangeDate: DateType | null;
}
