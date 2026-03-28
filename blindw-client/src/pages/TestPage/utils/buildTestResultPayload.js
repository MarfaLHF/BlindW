export function buildTestResultPayload({
  metrics,
  elapsedMs,
  mode,
  wordCount,
  durationSec,
  targetText,
  user,
}) {
  return {
    testResultId: 0,
    userId: user?.id || user?.userId,
    testSettingId: resolveTestSettingId({
      mode,
      wordCount,
      durationSec,
    }),
    countCharacters: targetText?.length ?? 0,
    totalTime: Number((elapsedMs / 1000).toFixed(2)),
    wpm: Number((metrics?.wpm ?? 0).toFixed(2)),
    accuracy: Number((metrics?.accuracy ?? 0).toFixed(2)),
  };
}

function resolveTestSettingId({ mode, wordCount, durationSec }) {
  if (mode === "words") {
    if (wordCount === 25) return 3;
    if (wordCount === 50) return 4;
    if (wordCount === 100) return 5;
  }

  if (mode === "time") {
    if (durationSec === 15) return 6;
    if (durationSec === 30) return 7;
    if (durationSec === 60) return 10;
  }

  return 3;
}