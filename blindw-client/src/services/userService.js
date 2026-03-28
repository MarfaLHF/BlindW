import { api } from "./api";

export async function getCurrentUser() {
  const { data } = await api.get("/Account/me");
  return data;
}