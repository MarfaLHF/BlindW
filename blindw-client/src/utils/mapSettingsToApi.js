export function mapSettingsToApi(settings) {
  const testTypeId = settings.mode === "words" ? 1 : 2;

  const wordCountIdMap = {
    25: 1,
    50: 2,
    100: 3,
  };

  const testDurationIdMap = {
    15: 1,
    30: 2,
    60: 3,
  };

  const languageIdMap = {
    en: 1,
    ru: 2,
  };

  const languageCode = settings.language ?? "en";
  const languageId = languageIdMap[languageCode];

  if (!languageId) {
    throw new Error(`Неизвестный language: ${languageCode}`);
  }

  let wordCountId = null;
  let testDurationId = null;

  if (settings.mode === "words") {
    wordCountId = wordCountIdMap[settings.wordCount];
    if (!wordCountId) {
      throw new Error(`Неизвестный wordCount: ${settings.wordCount}`);
    }
  }

  if (settings.mode === "time") {
    testDurationId = testDurationIdMap[settings.durationSec];
    if (!testDurationId) {
      throw new Error(`Неизвестный durationSec: ${settings.durationSec}`);
    }
  }

  return {
    testTypeId,
    wordCountId,
    testDurationId,
    languageId,
    languageCode,
    isNumbersEnabled: settings.isNumbersEnabled,
    isPunctuationEnabled: settings.isPunctuationEnabled,
  };
}