import type { GiftIdeaField } from "../gift-idea/types";

export interface GiftIdeaType extends GiftIdeaField {
  id: number;
}
export const GiftTypeValue = {
  IDEAS: "ideas",
  SURPRISE: "surprise",
} as const;

export type GiftType = (typeof GiftTypeValue)[keyof typeof GiftTypeValue];

export interface WishesFormProps {
  budget?: number;
}
