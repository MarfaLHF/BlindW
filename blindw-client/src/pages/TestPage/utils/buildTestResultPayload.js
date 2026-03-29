export function buildTestResultPayload({
  metrics,
  elapsedMs,
  targetText,
  user,
  testSettingId,
}) {
  return {
    userId: user?.id,
    testSettingId,
    countCharacters: targetText.length,
    totalTime: elapsedMs / 1000,
    wpm: metrics.wpm,
    accuracy: metrics.accuracy,
  };
}