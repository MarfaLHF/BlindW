import { useEffect, useState } from "react";

export function useResizeWidth(ref, fallback = 800) {
  const [width, setWidth] = useState(fallback);

  useEffect(() => {
    const el = ref.current;
    if (!el) return;

    const ro = new ResizeObserver(() => setWidth(el.clientWidth || fallback));
    ro.observe(el);
    setWidth(el.clientWidth || fallback);

    return () => ro.disconnect();
  }, [ref, fallback]);

  return width;
}