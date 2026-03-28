import { Link, useLocation, useNavigate } from "react-router-dom";
import { clearTokens, isAuthenticated } from "../../services/authService";

function NavLink({ to, children, pathname }) {
  const active = pathname === to;

  return (
    <Link
      to={to}
      style={{
        padding: "8px 12px",
        borderRadius: 10,
        textDecoration: "none",
        color: active ? "#000" : "#fff",
        background: active ? "#fff" : "transparent",
        border: "1px solid #333",
        fontSize: 14,
      }}
    >
      {children}
    </Link>
  );
}

export default function Header() {
  const { pathname } = useLocation();
  const navigate = useNavigate();

  const authed = isAuthenticated();

  const logout = () => {
    clearTokens();
    navigate("/login");
  };

  return (
    <header
      style={{
        padding: 16,
        borderBottom: "1px solid #1f1f1f",
        background: "#0b0b0b",
        position: "sticky",
        top: 0,
        zIndex: 10,
      }}
    >
      <div
        style={{
          width: "min(1100px, 92vw)",
          margin: "0 auto",
          display: "flex",
          alignItems: "center",
          justifyContent: "space-between",
          gap: 16,
          flexWrap: "wrap",
        }}
      >
        <div style={{ color: "white", fontWeight: 700 }}>BlindW</div>

        <nav style={{ display: "flex", gap: 8, flexWrap: "wrap", alignItems: "center" }}>
          <NavLink to="/test" pathname={pathname}>Тест</NavLink>
          <NavLink to="/profile" pathname={pathname}>Профиль</NavLink>
          <NavLink to="/leaderboard" pathname={pathname}>Лидеры</NavLink>
          <NavLink to="/lessons" pathname={pathname}>Уроки</NavLink>

          {!authed ? (
            <>
              <NavLink to="/login" pathname={pathname}>Войти</NavLink>
              <NavLink to="/register" pathname={pathname}>Регистрация</NavLink>
            </>
          ) : (
            <button
              onClick={logout}
              style={{
                padding: "8px 12px",
                borderRadius: 10,
                border: "1px solid #333",
                background: "transparent",
                color: "#fff",
                cursor: "pointer",
                fontSize: 14,
              }}
            >
              Выйти
            </button>
          )}
        </nav>
      </div>
    </header>
  );
}