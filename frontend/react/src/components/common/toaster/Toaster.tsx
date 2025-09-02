import { forwardRef, useEffect, useImperativeHandle, useState } from "react";
import { ICONS_PATH } from "../icon-button/utils";
import type { ToasterProps, ToasterHandler } from "./types";
import "./Toaster.scss";

const Toaster = forwardRef<ToasterHandler, ToasterProps>(
  ({ duration = 3000 }, ref) => {
    const [isVisible, setIsVisible] = useState(false);
    const [iconName, setIconName] = useState("");
    const [contentText, setContentText] = useState("");

    const iconPath = `${ICONS_PATH}${iconName}`;
    const onClose = () => setIsVisible(false);

    useImperativeHandle(ref, () => ({
      show(icon, text) {
        setIconName(icon);
        setContentText(text);
        setIsVisible(true);
      },
    }));

    useEffect(() => {
      if (!isVisible) {
        return;
      }

      const timerId = setTimeout(onClose, duration);

      return () => clearTimeout(timerId);
    }, [isVisible, duration]);

    return isVisible ? (
      <div className="toaster">
        <svg width={16} height={16}>
          <use href={iconPath} />
        </svg>
        <p className="toaster__text">{contentText}</p>
      </div>
    ) : null;
  },
);

Toaster.displayName = "Toaster";

export default Toaster;
