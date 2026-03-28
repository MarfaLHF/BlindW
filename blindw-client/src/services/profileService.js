import { api } from "./api";

export async function getProfileResults(userId) {
  const { data } = await api.get(`/api/TestResults/user/${userId}`);
  return data;
}