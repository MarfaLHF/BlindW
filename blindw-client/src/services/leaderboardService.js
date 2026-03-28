import { api } from "./api";

export async function getTopLeaderboard(count = 10) {
  const { data } = await api.get(`/api/Leaderboards/top?count=${count}`);
  return data;
}