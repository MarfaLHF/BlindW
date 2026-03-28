import axios from "axios";

export const api = axios.create({
  baseURL: "http://localhost:5273",
  withCredentials: true,
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem("accessToken");

  console.log("REQUEST URL:", config.url);
  console.log("ACCESS TOKEN:", token);

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
});