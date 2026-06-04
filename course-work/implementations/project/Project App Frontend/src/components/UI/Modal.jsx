import { createPortal } from "react-dom";
import { useImperativeHandle } from "react";
import { useRef } from "react";
import Button from "./Button";
export default function Modal({ children, ref, buttonCaption, onClick }) {
  const dialog = useRef();

  useImperativeHandle(ref, () => {
    return {
      open() {
        dialog.current.showModal();
      },
    };
  });

  return createPortal(
    <dialog
      ref={dialog}
      className="backdrop:bg-stone-900/90 p-4 rounded-md shadow-md"
    >
      {children}

      <Button onClick={() => dialog.current.close()}>{buttonCaption}</Button>
    </dialog>,
    document.getElementById("modal-root")
  );
}
