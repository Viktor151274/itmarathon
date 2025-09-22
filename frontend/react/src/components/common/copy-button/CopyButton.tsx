import { useRef } from "react";
import IconButton from "../icon-button/IconButton";
import Toaster from "../toaster/Toaster";
import type { ToasterHandler, ShowToasterProps } from "../toaster/types";
import type { CopyButtonProps } from "./types";
import { copyToClipboard } from "./utils";
import "./CopyButton.scss";
import type { StatusTypes } from "../../../types/types";

const CopyButton = ({
  textToCopy,
  buttonColor,
  iconName = "copy",
  successMessage = "Link is copied!",
  errorMessage = "Link was not copied. Try again.",
}: CopyButtonProps) => {
  const toasterRef = useRef<ToasterHandler>(null);

  const showToaster: ShowToasterProps = (
    message: string,
    toasterType: StatusTypes,
  ) => {
    toasterRef.current?.show(message, toasterType, "small");
  };

  const handleClick = () => {
    copyToClipboard(textToCopy, showToaster, { successMessage, errorMessage });
  };

  return (
    <div className="copy-button">
      <IconButton
        iconName={iconName}
        color={buttonColor}
        onClick={handleClick}
      />
      <Toaster className="copy-button__toaster" ref={toasterRef} />
    </div>
  );
};

export default CopyButton;
