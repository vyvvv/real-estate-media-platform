import { type ModalProps } from "../types/ModalProps";

const Modal = ({
  isOpen,
  onClose,
  onSave,
  title,
  subtitle,
  children,
}: ModalProps) => {
  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 bg-gray-900/20 flex items-center justify-center z-50 ">
      <div className="bg-white rounded-lg p-6 w-full max-w-2xl max-h-[90vh] overflow-y-auto">
        <div className="flex justify-between items-start mb-2">
          <div>
            <h2 className="text-xl font-semibold">{title}</h2>
            <p className="text-sm text-gray-500">{subtitle}</p>
          </div>
          <button
            type="button"
            onClick={onClose}
            className="text-gray-400 hover:text-gray-600 text-xl"
            aria-label="Close modal"
          >
            x
          </button>
        </div>

        <div className="mt-4">{children}</div>

        <div className="mt-6 flex justify-end gap-3">
          <button
            type="button"
            onClick={onClose}
            className="px-4 py-2 rounded-full border border-gray-300 text-sm hover:bg-gray-50"
          >
            Cancel
          </button>
          <button
            type="button"
            onClick={onSave}
            className="px-4 py-2 rounded-full bg-sky-600 text-white text-sm hover:bg-sky-700"
          >
            Save
          </button>
        </div>
      </div>
    </div>
  );
};

export default Modal;
