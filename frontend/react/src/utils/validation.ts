import type { KeyboardEvent } from "react";

export const validateNonNegativeNumber = (value: string): boolean => {
  return /^\d*$/.test(value);
};

export const INVALID_NUMBER_KEYS = ["-", "+", "e", "E", ".", ","];

export const blockInvalidNumberKeys = (
  e: KeyboardEvent<HTMLInputElement | HTMLTextAreaElement>,
) => {
  if (INVALID_NUMBER_KEYS.includes(e.key)) e.preventDefault();
};

export const isRequiredFieldsFilled = <T>(
  formData: T,
  requiredFields: (keyof T)[],
): boolean => {
  return requiredFields.every((requiredField) => {
    const fieldValue = formData[requiredField];

    return typeof fieldValue === "string" ? !!fieldValue?.trim() : !!fieldValue;
  });
};
