import { api } from "./api";

export async function saveTestResult(payload) {
  const { data } = await api.post("/api/TestResults", payload);
  return data;
}