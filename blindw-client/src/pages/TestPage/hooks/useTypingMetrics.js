import { useMemo } from "react";

export function useTypingMetrics({ typedMap, elapsedMs }) {
  return useMemo(() => {
    let correct = 0;
    let wrong = 0;

    for (const v of typedMap.values()) {
      if (v === true) correct++;
      else if (v === false) wrong++;
    }

    const totalTyped = correct + wrong;
    const accuracy = totalTyped === 0 ? 100 : Math.round((correct / totalTyped) * 100);

    const minutes = elapsedMs / 60000;
    // классика: 5 символов = 1 слово
    const wpm = minutes > 0 ? Math.round((correct / 5) / minutes) : 0;

    return { correct, wrong, totalTyped, accuracy, wpm };
  }, [typedMap, elapsedMs]);
}