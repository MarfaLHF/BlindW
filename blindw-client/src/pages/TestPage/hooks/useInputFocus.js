import { useEffect, useState } from "react";

export function useInputFocus(inputRef) {
  const [hasFocus, setHasFocus] = useState(true);

  const focusInput = () => {
    inputRef.current?.focus({ preventScroll: true });
  };

  useEffect(() => {
    const el = inputRef.current;
    if (!el) return;

    const onFocus = () => setHasFocus(true);
    const onBlur = () => setHasFocus(false);

    el.addEventListener("focus", onFocus);
    el.addEventListener("blur", onBlur);

    return () => {
      el.removeEventListener("focus", onFocus);
      el.removeEventListener("blur", onBlur);
    };
  }, [inputRef]);

  return { hasFocus, focusInput };
}