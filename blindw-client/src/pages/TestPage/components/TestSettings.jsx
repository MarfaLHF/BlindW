export default function TestSettings({ settings, setSettings }) {
  const setMode = (mode) => {
    setSettings((prev) => ({
      ...prev,
      mode,
    }));
  };

  const setWordCount = (wordCount) => {
    setSettings((prev) => ({
      ...prev,
      wordCount,
    }));
  };

  const setDurationSec = (durationSec) => {
    setSettings((prev) => ({
      ...prev,
      durationSec,
    }));
  };

  const setLanguage = (language) => {
    setSettings((prev) => ({
      ...prev,
      language,
    }));
  };

  const toggleNumbers = () => {
    setSettings((prev) => ({
      ...prev,
      isNumbersEnabled: !prev.isNumbersEnabled,
    }));
  };

  const togglePunctuation = () => {
    setSettings((prev) => ({
      ...prev,
      isPunctuationEnabled: !prev.isPunctuationEnabled,
    }));
  };

  const baseButtonStyle = {
    padding: "8px 14px",
    borderRadius: 10,
    border: "1px solid #2a2a2a",
    background: "#151515",
    color: "white",
    cursor: "pointer",
    fontSize: 14,
  };

  const activeButtonStyle = {
    ...baseButtonStyle,
    background: "#2a2a2a",
    border: "1px solid #4a4a4a",
  };

  return (
    <div
      style={{
        display: "grid",
        gap: 14,
        marginBottom: 18,
        padding: 16,
        border: "1px solid #222",
        borderRadius: 14,
        background: "#101010",
      }}
    >
      <div
        style={{
          display: "flex",
          flexWrap: "wrap",
          gap: 10,
          alignItems: "center",
          justifyContent: "center",
        }}
      >
        <span style={{ opacity: 0.8 }}>Режим:</span>

        <button
          type="button"
          onClick={() => setMode("words")}
          style={settings.mode === "words" ? activeButtonStyle : baseButtonStyle}
        >
          По словам
        </button>

        <button
          type="button"
          onClick={() => setMode("time")}
          style={settings.mode === "time" ? activeButtonStyle : baseButtonStyle}
        >
          По времени
        </button>
      </div>

      <div
        style={{
          display: "flex",
          flexWrap: "wrap",
          gap: 10,
          alignItems: "center",
          justifyContent: "center",
        }}
      >
        <span style={{ opacity: 0.8 }}>Язык:</span>

        <button
          type="button"
          onClick={() => setLanguage("en")}
          style={settings.language === "en" ? activeButtonStyle : baseButtonStyle}
        >
          EN
        </button>

        <button
          type="button"
          onClick={() => setLanguage("ru")}
          style={settings.language === "ru" ? activeButtonStyle : baseButtonStyle}
        >
          RU
        </button>
      </div>

      {settings.mode === "words" && (
        <div
          style={{
            display: "flex",
            flexWrap: "wrap",
            gap: 10,
            alignItems: "center",
            justifyContent: "center",
          }}
        >
          <span style={{ opacity: 0.8 }}>Количество слов:</span>

          {[25, 50, 100].map((count) => (
            <button
              key={count}
              type="button"
              onClick={() => setWordCount(count)}
              style={settings.wordCount === count ? activeButtonStyle : baseButtonStyle}
            >
              {count}
            </button>
          ))}
        </div>
      )}

      {settings.mode === "time" && (
        <div
          style={{
            display: "flex",
            flexWrap: "wrap",
            gap: 10,
            alignItems: "center",
            justifyContent: "center",
          }}
        >
          <span style={{ opacity: 0.8 }}>Время:</span>

          {[15, 30, 60].map((sec) => (
            <button
              key={sec}
              type="button"
              onClick={() => setDurationSec(sec)}
              style={settings.durationSec === sec ? activeButtonStyle : baseButtonStyle}
            >
              {sec} сек
            </button>
          ))}
        </div>
      )}

      <div
        style={{
          display: "flex",
          flexWrap: "wrap",
          gap: 10,
          alignItems: "center",
          justifyContent: "center",
        }}
      >
        <span style={{ opacity: 0.8 }}>Дополнительно:</span>

        <button
          type="button"
          onClick={toggleNumbers}
          style={settings.isNumbersEnabled ? activeButtonStyle : baseButtonStyle}
        >
          Цифры {settings.isNumbersEnabled ? "вкл" : "выкл"}
        </button>

        <button
          type="button"
          onClick={togglePunctuation}
          style={settings.isPunctuationEnabled ? activeButtonStyle : baseButtonStyle}
        >
          Пунктуация {settings.isPunctuationEnabled ? "вкл" : "выкл"}
        </button>
      </div>
    </div>
  );
}