import { useMemo } from "react";

export function useWrappedLines({ text, width, fontSize, fontFamily }) {
  return useMemo(() => {
    if (!text) return [];

    const canvas = document.createElement("canvas");
    const ctx = canvas.getContext("2d");
    ctx.font = `${fontSize}px ${fontFamily}`;

    const tokens = text.split(" ");
    const result = [];

    let current = "";
    for (let i = 0; i < tokens.length; i++) {
      const token = tokens[i];
      const next = current ? `${current} ${token}` : token;

      if (ctx.measureText(next).width <= width) {
        current = next;
      } else {
        if (current) result.push(current + " "); // важный пробел в конце строки
        current = token;
      }
    }
    if (current) result.push(current);

    return result;
  }, [text, width, fontSize, fontFamily]);
}