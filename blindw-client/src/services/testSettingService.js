import { api } from "./api";

export async function findOrCreateTestSetting({
  isPunctuationEnabled,
  isNumbersEnabled,
  testTypeId,
  wordCountId,
  testDurationId,
  languageId,
}) {
  const response = await api.post("/api/TestSettings/find-or-create", {
    isPunctuationEnabled,
    isNumbersEnabled,
    testTypeId,
    wordCountId,
    testDurationId,
    languageId,
  });

  return response.data;
}