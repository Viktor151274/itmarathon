import type { ShowToasterProps } from "../toaster/types";

export const copyToClipboard = (
  contentToCopy: string,
  showToaster: ShowToasterProps,
  messageConfig: { successMessage: string; errorMessage: string },
) =>
  navigator.clipboard
    .writeText(contentToCopy)
    .then(() => showToaster("success", messageConfig.successMessage))
    .catch(() => showToaster("error", messageConfig.errorMessage));
