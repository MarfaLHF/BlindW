function Stat({ label, value }) {
  return (
    <div
      style={{
        border: "1px solid #333",
        borderRadius: 12,
        padding: "10px 12px",
        minWidth: 120,
        background: "#0f0f0f",
      }}
    >
      <div style={{ opacity: 0.7, fontSize: 12 }}>{label}</div>
      <div style={{ fontSize: 20, fontWeight: 700 }}>{value}</div>
    </div>
  );
}

export default function StatsBar({
  onNewText,
  typedIndex,
  total,
  status,
  metrics,
  elapsedMs,
}) {
  const seconds = Math.floor(elapsedMs / 1000);

  return (
    <>
      <div style={{ marginTop: 14, display: "flex", justifyContent: "center", gap: 12, flexWrap: "wrap" }}>
        <button
          onClick={onNewText}
          style={{
            padding: "10px 14px",
            borderRadius: 10,
            border: "1px solid #333",
            background: "#141414",
            color: "white",
            cursor: "pointer",
          }}
        >
          Новый текст
        </button>

        <div style={{ opacity: 0.7, alignSelf: "center" }}>
          Символ: {typedIndex}/{total}
        </div>

        <div style={{ opacity: 0.7, alignSelf: "center" }}>
          Статус: {status}
        </div>
      </div>

      <div style={{ marginTop: 14, display: "flex", justifyContent: "center", gap: 12, flexWrap: "wrap" }}>
        <Stat label="Время (с)" value={seconds} />
        <Stat label="WPM" value={metrics.wpm} />
        <Stat label="Точность" value={`${metrics.accuracy}%`} />
        <Stat label="Ошибки" value={metrics.wrong} />
      </div>

      <div style={{ marginTop: 10, opacity: 0.7, textAlign: "center" }}>
        Перезапуск: Tab + Enter
      </div>
    </>
  );
}