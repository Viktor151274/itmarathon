import type { ShowToasterProps } from "../toaster/types";

export const copyToClipboard = (
  contentToCopy: string,
  showToaster: ShowToasterProps,
  messageConfig: { successMessage: string; errorMessage: string },
) => {
  navigator.clipboard
    .writeText(contentToCopy)
    .then(() => showToaster(messageConfig.successMessage, "success", "small"))
    .catch(() => showToaster(messageConfig.errorMessage, "error", "small"));
};
