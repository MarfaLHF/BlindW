import { useEffect, useRef, useState } from "react";

export function useTestTimer(status) {
  const [elapsedMs, setElapsedMs] = useState(0);
  const startRef = useRef(null);
  const rafRef = useRef(null);

  const reset = () => {
    setElapsedMs(0);
    startRef.current = null;
  };

  const start = () => {
    if (startRef.current != null) return;
    startRef.current = performance.now() - elapsedMs;
    tick();
  };

  const stop = () => {
    if (rafRef.current) cancelAnimationFrame(rafRef.current);
    rafRef.current = null;
  };

  const tick = () => {
    rafRef.current = requestAnimationFrame(() => {
      if (startRef.current == null) return;
      setElapsedMs(performance.now() - startRef.current);
      tick();
    });
  };

  useEffect(() => {
    if (status === "running") start();
    else stop();
    return () => stop();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [status]);

  return { elapsedMs, reset };
}