import type { KeyboardEvent } from "react";
import type { FieldValidation } from "../types/general";

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

export const phoneValidator = (phone: string): FieldValidation => {
  const trimmed = phone.trim();

  if (trimmed === "") {
    return {
      isValid: false,
      errorMessage: "",
    };
  }

  if (!/^\d{9}$/.test(trimmed)) {
    return {
      isValid: false,
      errorMessage: "Phone number must contain 9 digits",
    };
  }

  return {
    isValid: true,
    errorMessage: "",
  };
};
