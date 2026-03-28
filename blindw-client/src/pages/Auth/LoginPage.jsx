import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { login, saveTokens } from "../../services/authService";

export default function LoginPage() {
  const navigate = useNavigate();

  const [form, setForm] = useState({
    email: "",
    password: "",
  });

  const [loading, setLoading] = useState(false);
  const [err, setErr] = useState("");

  const onChange = (e) => {
    setForm((prev) => ({
      ...prev,
      [e.target.name]: e.target.value,
    }));
  };

  const onSubmit = async (e) => {
    e.preventDefault();
    setErr("");
    setLoading(true);

    try {
      const data = await login(form);
      console.log("LOGIN RESPONSE:", data);
      
      saveTokens(data);
      navigate("/test");
    } catch (error) {
      console.error(error);
      setErr("Не удалось войти. Проверь данные.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div
      style={{
        minHeight: "calc(100vh - 73px)",
        display: "grid",
        placeItems: "center",
        padding: 24,
      }}
    >
      <form
        onSubmit={onSubmit}
        style={{
          width: "min(420px, 92vw)",
          background: "#111",
          border: "1px solid #222",
          borderRadius: 16,
          padding: 24,
          color: "white",
        }}
      >
        <h1 style={{ marginTop: 0, marginBottom: 18 }}>Вход</h1>

        <label style={{ display: "block", marginBottom: 12 }}>
          <div style={{ marginBottom: 6 }}>Email</div>
          <input
            name="email"
            type="email"
            value={form.email}
            onChange={onChange}
            required
            style={inputStyle}
          />
        </label>

        <label style={{ display: "block", marginBottom: 16 }}>
          <div style={{ marginBottom: 6 }}>Пароль</div>
          <input
            name="password"
            type="password"
            value={form.password}
            onChange={onChange}
            required
            style={inputStyle}
          />
        </label>

        {err && <div style={{ color: "tomato", marginBottom: 12 }}>{err}</div>}

        <button type="submit" disabled={loading} style={primaryButtonStyle}>
          {loading ? "Входим..." : "Войти"}
        </button>

        <div style={{ marginTop: 14, opacity: 0.8 }}>
          Нет аккаунта?{" "}
          <Link to="/register" style={{ color: "#fff" }}>
            Зарегистрироваться
          </Link>
        </div>
      </form>
    </div>
  );
}

const inputStyle = {
  width: "100%",
  padding: "12px 14px",
  borderRadius: 10,
  border: "1px solid #333",
  background: "#0b0b0b",
  color: "#fff",
  outline: "none",
  fontSize: 16,
  boxSizing: "border-box",
};

const primaryButtonStyle = {
  width: "100%",
  padding: "12px 14px",
  borderRadius: 10,
  border: "1px solid #333",
  background: "#fff",
  color: "#000",
  cursor: "pointer",
  fontSize: 16,
  fontWeight: 600,
};