import { createContext, useContext, useEffect, useState } from "react";
import { getCurrentUser } from "../services/userService";

const AuthContext = createContext(null);

export function AuthProvider({ children }) {
  const [user, setUser] = useState(null);
  const [isAuthenticated, setIsAuthenticated] = useState(
    !!localStorage.getItem("accessToken")
  );
  const [isAuthReady, setIsAuthReady] = useState(false);

  useEffect(() => {
    async function loadUser() {
      const token = localStorage.getItem("accessToken");

      if (!token) {
        setUser(null);
        setIsAuthenticated(false);
        setIsAuthReady(true);
        return;
      }

      try {
        const data = await getCurrentUser();
        setUser(data);
        setIsAuthenticated(true);
      } catch (e) {
        console.error("Не удалось получить текущего пользователя:", e);
        setUser(null);
        setIsAuthenticated(false);
      } finally {
        setIsAuthReady(true);
      }
    }

    loadUser();
  }, []);

  function login(tokens, userData = null) {
    localStorage.setItem("accessToken", tokens.accessToken);
    localStorage.setItem("refreshToken", tokens.refreshToken);

    setIsAuthenticated(true);

    if (userData) {
      setUser(userData);
    }
  }

  function logout() {
    localStorage.removeItem("accessToken");
    localStorage.removeItem("refreshToken");
    setUser(null);
    setIsAuthenticated(false);
  }

  return (
    <AuthContext.Provider
      value={{
        user,
        setUser,
        isAuthenticated,
        isAuthReady,
        login,
        logout,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const context = useContext(AuthContext);

  if (!context) {
    throw new Error("useAuth должен использоваться внутри AuthProvider");
  }

  return context;
}