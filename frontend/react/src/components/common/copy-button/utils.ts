import type { ShowToasterProps } from "../toaster/types";

export const ICON_NAMES = {
  SUCCESS: "success",
  ERROR: "error",
} as const;

export const BUTTON_COLORS = {
  GREEN: "green",
  WHITE: "white",
} as const;

export const copyToClipboard = (
  contentToCopy: string,
  showToaster: ShowToasterProps,
  messageConfig: { successMessage: string; errorMessage: string },
) =>
  navigator.clipboard
    .writeText(contentToCopy)
    .then(() => showToaster("success", messageConfig.successMessage))
    .catch(() => showToaster("error", messageConfig.errorMessage));
