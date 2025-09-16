import { forwardRef, useEffect, useImperativeHandle, useState } from "react";
import { ICONS_PATH } from "../icon-button/utils";
import type { ToasterProps, ToasterHandler } from "./types";
import "./Toaster.scss";

const Toaster = forwardRef<ToasterHandler, ToasterProps>(
  ({ duration = 3000, isGlobal = false }, ref) => {
    const [isVisible, setIsVisible] = useState(false);
    const [type, setType] = useState("");
    const [size, setSize] = useState("small");
    const [contentText, setContentText] = useState("");
    const iconPath = `${ICONS_PATH}${type}`;
    const onClose = () => setIsVisible(false);

    useImperativeHandle(ref, () => ({
      show(text, type, size) {
        setType(type);
        setContentText(text);
        setIsVisible(true);
        setSize(size);
      },
      close() {
        onClose();
      },
    }));

    useEffect(() => {
      if (!isVisible) return;

      const timerId = setTimeout(onClose, duration);
      return () => clearTimeout(timerId);
    }, [isVisible, duration]);

    return isVisible ? (
      <div
        className={`toaster toaster--${size} ${
          isGlobal ? "toaster--global" : null
        }`}
      >
        <svg className="toaster__icon">
          <use href={iconPath} />
        </svg>
        <p className="toaster__text">{contentText}</p>
      </div>
    ) : null;
  },
);

Toaster.displayName = "Toaster";

export default Toaster;
