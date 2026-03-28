import { api } from "./api";

export async function login({ email, password }) {
  const res = await api.post(
    "/login?useCookies=false&useSessionCookies=false",
    {
      email,
      password,
    }
  );

  return res.data;
}

export async function register({
  firstName,
  lastName,
  login: userLogin,
  email,
  password,
}) {
  const res = await api.post("/Account/registration", {
    firstName,
    lastName,
    login: userLogin,
    email,
    password,
  });

  return res.data;
}

export async function refresh(refreshToken) {
  const res = await api.post("/refresh", {
    refreshToken,
  });

  return res.data;
}

export function saveTokens({ accessToken, refreshToken }) {
  localStorage.setItem("accessToken", accessToken);
  localStorage.setItem("refreshToken", refreshToken);
}

export function clearTokens() {
  localStorage.removeItem("accessToken");
  localStorage.removeItem("refreshToken");
}

export function getAccessToken() {
  return localStorage.getItem("accessToken");
}

export function getRefreshToken() {
  return localStorage.getItem("refreshToken");
}

export function isAuthenticated() {
  return !!getAccessToken();
}