import { InputNames } from "./types";
import type { FormData, InputName } from "./types";
import { validateNonNegativeNumber } from "../../../utils/validation";
export const LABEL_DATE_PICKER = "Gift Exchange date";
export const INPUT_ID_DATE_PICKER = "input-gift-exchange-date";

export const requiredFields: (keyof FormData)[] = [
  ...Object.values(InputNames),
  "giftExchangeDate",
];

export const FieldValidators: Record<InputName, (value: string) => boolean> = {
  [InputNames.ROOM_NAME]: () => true,
  [InputNames.ROOM_DESCRIPTION]: () => true,
  [InputNames.GIFT_BUDGET]: validateNonNegativeNumber,
};

export const isValidInputField = (name: string, value: string): boolean => {
  if (!Object.values(InputNames).includes(name as InputName)) return false;

  if (name === InputNames.GIFT_BUDGET) {
    return validateNonNegativeNumber(value);
  }

  return true;
};
