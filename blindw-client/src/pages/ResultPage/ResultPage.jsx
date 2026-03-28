import { Link, useLocation, Navigate } from "react-router-dom";

function formatSeconds(ms) {
  return (ms / 1000).toFixed(2);
}

export default function ResultPage() {
  const location = useLocation();
  const state = location.state;

  if (!state) {
    return <Navigate to="/test" replace />;
  }

  return (
    <div
      style={{
        maxWidth: "900px",
        margin: "0 auto",
        padding: "40px 20px",
      }}
    >
      <h1 style={{ marginBottom: "24px" }}>Результат теста</h1>

      <div
        style={{
          display: "grid",
          gridTemplateColumns: "repeat(auto-fit, minmax(180px, 1fr))",
          gap: "16px",
        }}
      >
        <div style={cardStyle}>
          <div style={labelStyle}>WPM</div>
          <div style={valueStyle}>{Math.round(state.wpm ?? 0)}</div>
        </div>

        <div style={cardStyle}>
          <div style={labelStyle}>Точность</div>
          <div style={valueStyle}>{Number(state.accuracy ?? 0).toFixed(2)}%</div>
        </div>

        <div style={cardStyle}>
          <div style={labelStyle}>Ошибки</div>
          <div style={valueStyle}>{state.wrong ?? 0}</div>
        </div>

        <div style={cardStyle}>
          <div style={labelStyle}>Время</div>
          <div style={valueStyle}>{formatSeconds(state.elapsedMs ?? 0)} c</div>
        </div>

        <div style={cardStyle}>
          <div style={labelStyle}>Символы</div>
          <div style={valueStyle}>{state.countCharacters ?? 0}</div>
        </div>

        <div style={cardStyle}>
          <div style={labelStyle}>Режим</div>
          <div style={valueStyle}>
            {state.mode === "time"
              ? `Время: ${state.durationSec}с`
              : `Слова: ${state.wordCount}`}
          </div>
        </div>
      </div>

      <div style={{ marginTop: "24px", display: "flex", gap: "12px" }}>
        <Link to="/test" style={linkBtnStyle}>
          Пройти снова
        </Link>

        <Link to="/profile" style={linkBtnStyle}>
          Профиль
        </Link>
      </div>
    </div>
  );
}

const cardStyle = {
  border: "1px solid #2a2a2a",
  borderRadius: "16px",
  padding: "20px",
  background: "#111",
};

const labelStyle = {
  fontSize: "14px",
  opacity: 0.8,
  marginBottom: "10px",
};

const valueStyle = {
  fontSize: "28px",
  fontWeight: 700,
};

const linkBtnStyle = {
  display: "inline-flex",
  alignItems: "center",
  justifyContent: "center",
  padding: "10px 16px",
  borderRadius: "10px",
  border: "1px solid #2a2a2a",
  color: "#fff",
  textDecoration: "none",
};