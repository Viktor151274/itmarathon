import { CreateRoomFormInputNames } from "./types";
import type { FormData, InputName } from "./types";
import { validateNonNegativeNumber } from "../../../utils/validation";
export const LABEL_DATE_PICKER = "Gift Exchange date";
export const INPUT_ID_DATE_PICKER = "input-gift-exchange-date";

export const requiredFields: (keyof FormData)[] = [
  ...Object.values(CreateRoomFormInputNames),
  "giftExchangeDate",
];

export const FieldValidators: Record<InputName, (value: string) => boolean> = {
  [CreateRoomFormInputNames.ROOM_NAME]: () => true,
  [CreateRoomFormInputNames.ROOM_DESCRIPTION]: () => true,
  [CreateRoomFormInputNames.GIFT_BUDGET]: validateNonNegativeNumber,
};

export const isValidInputField = (name: string, value: string): boolean => {
  if (!Object.values(CreateRoomFormInputNames).includes(name as InputName))
    return false;

  if (name === CreateRoomFormInputNames.GIFT_BUDGET) {
    return validateNonNegativeNumber(value);
  }

  return true;
};
