import type { DatePickerProps } from "antd";

export const InputNames = {
  ROOM_NAME: "roomName",
  ROOM_DESCRIPTION: "roomDescription",
  GIFT_BUDGET: "giftBudget",
} as const;

export type InputName = (typeof InputNames)[keyof typeof InputNames];

type DatePickerOnChange = NonNullable<DatePickerProps["onChange"]>;
export type DateType = Parameters<DatePickerOnChange>[0];

export interface FormData {
  roomName: string;
  roomDescription: string;
  giftBudget: string;
  giftExchangeDate: DateType | null;
}
