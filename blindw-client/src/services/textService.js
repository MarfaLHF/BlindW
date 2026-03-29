import { api } from "./api";

export async function getRandomText({
  wordCount,
  languageCode = "en",
  isNumbersEnabled = false,
  isPunctuationEnabled = false,
}) {
  const response = await api.post("/api/GetText/randomText", {
    wordCount,
    languageCode,
    isNumbersEnabled,
    isPunctuationEnabled,
  });

  return response.data;
}