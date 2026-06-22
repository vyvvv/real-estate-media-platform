import { type ReactNode } from "react";

export type ModalProps = {
  isOpen: boolean;
  onClose: () => void;
  onSave: () => void;
  title: string;
  subtitle: string;
  children: ReactNode;
};
