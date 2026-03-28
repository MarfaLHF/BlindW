const modes = [
  { key: "words", label: "Слова" },
  { key: "time", label: "Время" },
];

const wordOptions = [10, 25, 50, 120];
const timeOptions = [15, 30, 60, 120];

function Pill({ active, children, onClick }) {
  return (
    <button
      onClick={onClick}
      style={{
        padding: "8px 12px",
        borderRadius: 999,
        border: "1px solid #333",
        background: active ? "#ffffff" : "#141414",
        color: active ? "#000000" : "#ffffff",
        cursor: "pointer",
        fontSize: 14,
      }}
    >
      {children}
    </button>
  );
}

export default function TestSettings({
  mode,
  setMode,
  wordCount,
  setWordCount,
  durationSec,
  setDurationSec,
}) {
  return (
    <div
      style={{
        marginBottom: 14,
        display: "flex",
        flexDirection: "column",
        gap: 10,
        alignItems: "center",
      }}
    >
      <div style={{ display: "flex", gap: 8, flexWrap: "wrap", justifyContent: "center" }}>
        {modes.map((item) => (
          <Pill
            key={item.key}
            active={mode === item.key}
            onClick={() => setMode(item.key)}
          >
            {item.label}
          </Pill>
        ))}
      </div>

      {mode === "words" && (
        <div style={{ display: "flex", gap: 8, flexWrap: "wrap", justifyContent: "center" }}>
          {wordOptions.map((value) => (
            <Pill
              key={value}
              active={wordCount === value}
              onClick={() => setWordCount(value)}
            >
              {value} слов
            </Pill>
          ))}
        </div>
      )}

      {mode === "time" && (
        <div style={{ display: "flex", gap: 8, flexWrap: "wrap", justifyContent: "center" }}>
          {timeOptions.map((value) => (
            <Pill
              key={value}
              active={durationSec === value}
              onClick={() => setDurationSec(value)}
            >
              {value} сек
            </Pill>
          ))}
        </div>
      )}
    </div>
  );
}