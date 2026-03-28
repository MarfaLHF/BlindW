import { useRef, useState } from "react";

export function useTypingEngine(targetText, { onRestart } = {}) {
  const [typedIndex, setTypedIndex] = useState(0);
  const [typedMap, setTypedMap] = useState(() => new Map());
  const [status, setStatus] = useState("idle"); // idle | running | finished

  const tabHeldRef = useRef(false);

  const reset = () => {
    setTypedIndex(0);
    setTypedMap(new Map());
    setStatus("idle");
    tabHeldRef.current = false;
  };

  const finishIfNeeded = (nextIndex) => {
    if (!targetText) return;
    if (nextIndex >= targetText.length) {
      setStatus("finished");
    }
  };

  const onKeyDown = (e) => {
    if (!targetText) return;

    // не обрабатываем комбинации (Ctrl/Alt/Meta)
    if (e.ctrlKey || e.altKey || e.metaKey) return;

    // если тест закончен — разрешаем только перезапуск
    if (status === "finished") {
      if (e.key === "Tab") {
        e.preventDefault();
        tabHeldRef.current = true;
        return;
      }
      if (e.key === "Enter") {
        e.preventDefault();
        if (tabHeldRef.current) onRestart?.();
        return;
      }
      return;
    }

    // Tab удерживаем (не уводим фокус)
    if (e.key === "Tab") {
      e.preventDefault();
      tabHeldRef.current = true;
      return;
    }

    // Tab + Enter = рестарт
    if (e.key === "Enter") {
      e.preventDefault();
      if (tabHeldRef.current) onRestart?.();
      return;
    }

    if (e.key === "Backspace") {
      e.preventDefault();
      if (typedIndex <= 0) return;

      const newIdx = typedIndex - 1;
      setTypedIndex(newIdx);
      setTypedMap((prev) => {
        const next = new Map(prev);
        next.delete(newIdx);
        return next;
      });

      // если пользователь стирает — возвращаемся в running/idle
      setStatus((s) => (s === "finished" ? "running" : s));
      return;
    }

    // системные клавиши игнор
    if (e.key.length !== 1) return;

    e.preventDefault();

    // старт теста по первому символу
    if (status === "idle") setStatus("running");

    const expected = targetText[typedIndex];
    const actual = e.key;

    const isCorrect = actual === expected;

    setTypedMap((prev) => {
      const next = new Map(prev);
      next.set(typedIndex, isCorrect);
      return next;
    });

    const nextIndex = Math.min(typedIndex + 1, targetText.length);
    setTypedIndex(nextIndex);
    finishIfNeeded(nextIndex);
  };

  const onKeyUp = (e) => {
    if (e.key === "Tab") tabHeldRef.current = false;
  };

  const onInput = (e) => {
    e.target.value = "";
  };

  return {
    typedIndex,
    typedMap,
    status,
    reset,
    onKeyDown,
    onKeyUp,
    onInput,
    setStatus, // на будущее, если понадобится
  };
}