import { useEffect, useMemo, useState } from "react";
import { getTopLeaderboard } from "../../services/leaderboardService";

function getRankStyle(index) {
  if (index === 0) return { color: "#f5c542" };
  if (index === 1) return { color: "#c0c0c0" };
  if (index === 2) return { color: "#cd7f32" };
  return { color: "#ffffff" };
}

function formatDate(value) {
  if (!value) return "—";

  const date = new Date(value);

  if (Number.isNaN(date.getTime())) {
    return "—";
  }

  return date.toLocaleDateString("ru-RU");
}

export default function LeaderboardPage() {
  const [leaders, setLeaders] = useState([]);
  const [count, setCount] = useState(10);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    let cancelled = false;

    async function loadLeaderboard() {
      try {
        setLoading(true);
        setError("");

        const data = await getTopLeaderboard(count);

        if (cancelled) return;

        setLeaders(Array.isArray(data) ? data : []);
      } catch (err) {
        console.error("Ошибка загрузки таблицы лидеров:", err);

        if (!cancelled) {
          setError("Не удалось загрузить таблицу лидеров.");
        }
      } finally {
        if (!cancelled) {
          setLoading(false);
        }
      }
    }

    loadLeaderboard();

    return () => {
      cancelled = true;
    };
  }, [count]);

  const preparedLeaders = useMemo(() => {
    return leaders.map((item, index) => ({
      rank: index + 1,
      id: item.id ?? item.leaderboardId ?? `${index}-${item.userName ?? "user"}`,
      userName: item.userName ?? item.login ?? item.email ?? "Неизвестный",
      firstName: item.firstName ?? "",
      lastName: item.lastName ?? "",
      wpm: item.wpm ?? item.bestWpm ?? 0,
      accuracy: item.accuracy ?? item.bestAccuracy ?? 0,
      totalTests: item.totalTests ?? item.testsCount ?? 0,
      date: item.date ?? item.createdAt ?? item.testDateTime ?? null,
    }));
  }, [leaders]);

  return (
    <div style={styles.page}>
      <div style={styles.container}>
        <div style={styles.headerRow}>
          <div>
            <h1 style={styles.title}>Таблица лидеров</h1>
            <p style={styles.subtitle}>Лучшие результаты пользователей BlindW</p>
          </div>

          <div style={styles.controls}>
            <label style={styles.label}>Показать</label>
            <select
              value={count}
              onChange={(e) => setCount(Number(e.target.value))}
              style={styles.select}
            >
              <option value={10}>Топ 10</option>
              <option value={25}>Топ 25</option>
              <option value={50}>Топ 50</option>
            </select>
          </div>
        </div>

        {loading ? (
          <div style={styles.card}>Загрузка лидеров...</div>
        ) : error ? (
          <div style={styles.errorCard}>{error}</div>
        ) : preparedLeaders.length === 0 ? (
          <div style={styles.card}>Пока нет данных для таблицы лидеров.</div>
        ) : (
          <div style={styles.card}>
            <div style={styles.tableWrap}>
              <table style={styles.table}>
                <thead>
                  <tr>
                    <th style={styles.th}>Место</th>
                    <th style={styles.th}>Пользователь</th>
                    <th style={styles.th}>Имя</th>
                    <th style={styles.th}>WPM</th>
                    <th style={styles.th}>Точность</th>
                    <th style={styles.th}>Тестов</th>
                    <th style={styles.th}>Дата</th>
                  </tr>
                </thead>
                <tbody>
                  {preparedLeaders.map((item, index) => (
                    <tr key={item.id} style={styles.row}>
                      <td style={styles.td}>
                        <span style={{ ...styles.rank, ...getRankStyle(index) }}>
                          #{item.rank}
                        </span>
                      </td>

                      <td style={styles.td}>{item.userName}</td>

                      <td style={styles.td}>
                        {[item.firstName, item.lastName].filter(Boolean).join(" ") || "—"}
                      </td>

                      <td style={styles.td}>{item.wpm}</td>
                      <td style={styles.td}>
                        {item.accuracy ? `${Number(item.accuracy).toFixed(1)}%` : "—"}
                      </td>
                      <td style={styles.td}>{item.totalTests || "—"}</td>
                      <td style={styles.td}>{formatDate(item.date)}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </div>
        )}
      </div>
    </div>
  );
}

const styles = {
  page: {
    minHeight: "100vh",
    background: "#0b0b0b",
    color: "#ffffff",
    padding: "32px 16px",
  },
  container: {
    maxWidth: "1200px",
    margin: "0 auto",
  },
  headerRow: {
    display: "flex",
    justifyContent: "space-between",
    alignItems: "flex-end",
    gap: "16px",
    marginBottom: "24px",
    flexWrap: "wrap",
  },
  title: {
    fontSize: "32px",
    fontWeight: "700",
    margin: 0,
    marginBottom: "8px",
  },
  subtitle: {
    margin: 0,
    color: "#9ca3af",
    fontSize: "15px",
  },
  controls: {
    display: "flex",
    alignItems: "center",
    gap: "10px",
  },
  label: {
    color: "#9ca3af",
    fontSize: "14px",
  },
  select: {
    background: "#151515",
    color: "#ffffff",
    border: "1px solid #262626",
    borderRadius: "10px",
    padding: "10px 14px",
    outline: "none",
    cursor: "pointer",
  },
  card: {
    background: "#151515",
    border: "1px solid #262626",
    borderRadius: "18px",
    padding: "24px",
  },
  errorCard: {
    background: "#2a1414",
    border: "1px solid #5a2323",
    borderRadius: "18px",
    padding: "24px",
    color: "#ffb4b4",
  },
  tableWrap: {
    overflowX: "auto",
  },
  table: {
    width: "100%",
    borderCollapse: "collapse",
    minWidth: "900px",
  },
  th: {
    textAlign: "left",
    padding: "14px 12px",
    borderBottom: "1px solid #2a2a2a",
    color: "#9ca3af",
    fontSize: "14px",
    fontWeight: "600",
  },
  td: {
    padding: "16px 12px",
    borderBottom: "1px solid #1f1f1f",
    fontSize: "15px",
    verticalAlign: "middle",
  },
  row: {
    background: "transparent",
  },
  rank: {
    fontWeight: "700",
    fontSize: "16px",
  },
};