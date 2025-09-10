import { useRef } from "react";
import IconButton from "../icon-button/IconButton";
import Toaster from "../toaster/Toaster";
import type { IconName, ToasterHandler } from "../toaster/types";
import type { CopyButtonProps } from "./types";
import { copyToClipboard } from "./utils";
import "./CopyButton.scss";

const CopyButton = ({ textToCopy, buttonColor }: CopyButtonProps) => {
  const toasterRef = useRef<ToasterHandler>(null);

  const showToaster = (iconName: IconName, message: string) => {
    toasterRef.current?.show(iconName, message);
  };

  const handleClick = () => {
    copyToClipboard(textToCopy, showToaster, {
      successMessage: "Link is copied!",
      errorMessage: "Link was not copied. Try again.",
    });
  };

  return (
    <div className="copy-button">
      <IconButton iconName="copy" color={buttonColor} onClick={handleClick} />
      <Toaster ref={toasterRef} />
    </div>
  );
};

export default CopyButton;
