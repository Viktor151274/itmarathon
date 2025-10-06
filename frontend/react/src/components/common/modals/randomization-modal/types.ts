import type { ReactNode } from "react";

export interface RandomizationModalProps {
  isOpen?: boolean;
  onClose: () => void;
  children: ReactNode;
}
