import { api } from "./api";

export async function getRandomText(wordCount) {
  const res = await api.get("/api/GetText/randomText", {
    params: { wordCount },
  });
  return res.data; // массив слов
}