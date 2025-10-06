import { useEffect } from "react";
import { createPortal } from "react-dom";
import IconButton from "@components/common/icon-button/IconButton";
import Button from "@components/common/button/Button";
import type { RandomizationModalProps } from "./types";
import "./RandomizationModal.scss";

const RandomizationModal = ({
  isOpen = false,
  onClose,
  children,
}: RandomizationModalProps) => {
  useEffect(() => {
    document.body.style.overflow = isOpen ? "hidden" : "";

    return () => {
      document.body.style.overflow = "";
    };
  }, [isOpen]);

  if (!isOpen) return null;

  const randomizationModalElement = (
    <div className="randomization-modal-container">
      <div className="randomization-modal">
        <h3 className="randomization-modal__title">Look Who You Got!</h3>

        <div className="randomization-modal__close-button">
          <IconButton iconName="cross" onClick={onClose} />
        </div>

        {children}

        <div className="randomization-modal__back-button">
          <Button width={228} size="medium" onClick={onClose}>
            Go Back To Room
          </Button>
        </div>
      </div>
    </div>
  );

  return createPortal(randomizationModalElement, document.body);
};

export default RandomizationModal;
