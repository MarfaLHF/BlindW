function ResultItem({ label, value }) {
  return (
    <div
      style={{
        border: "1px solid #333",
        borderRadius: 14,
        padding: "14px 16px",
        minWidth: 140,
        background: "#101010",
      }}
    >
      <div style={{ fontSize: 12, opacity: 0.7 }}>{label}</div>
      <div style={{ fontSize: 28, fontWeight: 700, marginTop: 4 }}>{value}</div>
    </div>
  );
}

export default function ResultCard({
  show,
  metrics,
  elapsedMs,
  onNewText,
  onRetry,
}) {
  if (!show) return null;

  const seconds = Math.max(1, Math.floor(elapsedMs / 1000));

  return (
    <div
      style={{
        marginTop: 18,
        border: "1px solid #2a2a2a",
        borderRadius: 16,
        padding: 18,
        background: "#0f0f0f",
      }}
    >
      <h2 style={{ margin: 0, marginBottom: 14, textAlign: "center" }}>
        Результат
      </h2>

      <div
        style={{
          display: "flex",
          gap: 12,
          flexWrap: "wrap",
          justifyContent: "center",
        }}
      >
        <ResultItem label="WPM" value={metrics.wpm} />
        <ResultItem label="Точность" value={`${metrics.accuracy}%`} />
        <ResultItem label="Ошибки" value={metrics.wrong} />
        <ResultItem label="Время" value={`${seconds}с`} />
      </div>

      <div
        style={{
          marginTop: 16,
          display: "flex",
          gap: 10,
          justifyContent: "center",
          flexWrap: "wrap",
        }}
      >
        <button
          onClick={onRetry}
          style={{
            padding: "10px 14px",
            borderRadius: 10,
            border: "1px solid #333",
            background: "#1a1a1a",
            color: "#fff",
            cursor: "pointer",
          }}
        >
          Повторить
        </button>

        <button
          onClick={onNewText}
          style={{
            padding: "10px 14px",
            borderRadius: 10,
            border: "1px solid #333",
            background: "#ffffff",
            color: "#000",
            cursor: "pointer",
          }}
        >
          Новый текст
        </button>
      </div>
    </div>
  );
}