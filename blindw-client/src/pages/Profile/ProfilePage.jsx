import { useEffect, useMemo, useState } from "react";
import { getCurrentUser } from "../../services/userService";
import { getProfileResults } from "../../services/profileService";

function formatDate(value) {
  if (!value) return "—";
  const date = new Date(value);
  if (Number.isNaN(date.getTime())) return "—";
  return date.toLocaleString("ru-RU");
}

function formatAccuracy(value) {
  if (value === null || value === undefined) return "—";
  return `${Number(value).toFixed(1)}%`;
}

function formatTime(value) {
  if (value === null || value === undefined) return "—";
  return `${Number(value).toFixed(1)} сек`;
}

export default function ProfilePage() {
  const [user, setUser] = useState(null);
  const [results, setResults] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
  let cancelled = false;

  async function loadProfile() {
    try {
      setLoading(true);
      setError("");

      const userData = await getCurrentUser();
      const resultsData = await getProfileResults(userData.id);

      if (cancelled) return;

      setUser(userData);
      setResults(Array.isArray(resultsData) ? resultsData : []);
    } catch (err) {
      console.error("Ошибка загрузки профиля:", err);

      if (!cancelled) {
        setError("Не удалось загрузить профиль.");
      }
    } finally {
      if (!cancelled) {
        setLoading(false);
      }
    }
  }

  loadProfile();

  return () => {
    cancelled = true;
  };
}, []);

  const latestResults = useMemo(() => {
    return [...results]
      .sort((a, b) => new Date(b.testDateTime) - new Date(a.testDateTime))
      .slice(0, 10);
  }, [results]);

  const stats = useMemo(() => {
    if (!results.length) {
      return {
        total: 0,
        bestWpm: 0,
        bestAccuracy: 0,
      };
    }

    return {
      total: results.length,
      bestWpm: Math.max(...results.map((x) => Number(x.wpm) || 0)),
      bestAccuracy: Math.max(...results.map((x) => Number(x.accuracy) || 0)),
    };
  }, [results]);

  if (loading) {
    return (
      <div style={styles.page}>
        <div style={styles.container}>
          <div style={styles.card}>Загрузка профиля...</div>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div style={styles.page}>
        <div style={styles.container}>
          <div style={styles.errorCard}>{error}</div>
        </div>
      </div>
    );
  }

  return (
    <div style={styles.page}>
      <div style={styles.container}>
        <h1 style={styles.title}>Профиль</h1>

        <section style={styles.card}>
          <h2 style={styles.sectionTitle}>Информация о пользователе</h2>

          <div style={styles.grid}>
            <div style={styles.item}>
              <div style={styles.label}>ID</div>
              <div style={styles.value}>{user?.id || "—"}</div>
            </div>

            <div style={styles.item}>
              <div style={styles.label}>Email</div>
              <div style={styles.value}>{user?.email || "—"}</div>
            </div>

            <div style={styles.item}>
              <div style={styles.label}>Username</div>
              <div style={styles.value}>{user?.userName || "—"}</div>
            </div>

            <div style={styles.item}>
              <div style={styles.label}>Имя</div>
              <div style={styles.value}>{user?.firstName || "—"}</div>
            </div>

            <div style={styles.item}>
              <div style={styles.label}>Фамилия</div>
              <div style={styles.value}>{user?.lastName || "—"}</div>
            </div>

            <div style={styles.item}>
              <div style={styles.label}>Логин</div>
              <div style={styles.value}>{user?.login || "—"}</div>
            </div>
          </div>
        </section>

        <section style={styles.card}>
          <h2 style={styles.sectionTitle}>Статистика</h2>

          <div style={styles.statsGrid}>
            <div style={styles.statItem}>
              <div style={styles.statLabel}>Всего тестов</div>
              <div style={styles.statValue}>{stats.total}</div>
            </div>

            <div style={styles.statItem}>
              <div style={styles.statLabel}>Лучший WPM</div>
              <div style={styles.statValue}>{stats.bestWpm}</div>
            </div>

            <div style={styles.statItem}>
              <div style={styles.statLabel}>Лучшая точность</div>
              <div style={styles.statValue}>
                {stats.bestAccuracy ? `${stats.bestAccuracy.toFixed(1)}%` : "0%"}
              </div>
            </div>
          </div>
        </section>

        <section style={styles.card}>
          <h2 style={styles.sectionTitle}>Последние результаты</h2>

          {latestResults.length === 0 ? (
            <div style={styles.empty}>У пользователя пока нет результатов.</div>
          ) : (
            <div style={styles.tableWrap}>
              <table style={styles.table}>
                <thead>
                  <tr>
                    <th style={styles.th}>Дата</th>
                    <th style={styles.th}>WPM</th>
                    <th style={styles.th}>Точность</th>
                    <th style={styles.th}>Символы</th>
                    <th style={styles.th}>Время</th>
                  </tr>
                </thead>
                <tbody>
                  {latestResults.map((item) => (
                    <tr key={item.testResultId} style={styles.row}>
                      <td style={styles.td}>{formatDate(item.testDateTime)}</td>
                      <td style={styles.td}>{item.wpm ?? "—"}</td>
                      <td style={styles.td}>{formatAccuracy(item.accuracy)}</td>
                      <td style={styles.td}>{item.countCharacters ?? "—"}</td>
                      <td style={styles.td}>{formatTime(item.totalTime)}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}
        </section>
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
    maxWidth: "1100px",
    margin: "0 auto",
  },
  title: {
    fontSize: "32px",
    fontWeight: "700",
    marginBottom: "24px",
  },
  card: {
    background: "#151515",
    border: "1px solid #262626",
    borderRadius: "18px",
    padding: "24px",
    marginBottom: "20px",
  },
  errorCard: {
    background: "#2a1414",
    border: "1px solid #5a2323",
    borderRadius: "18px",
    padding: "24px",
    color: "#ffb4b4",
  },
  sectionTitle: {
    fontSize: "22px",
    fontWeight: "600",
    marginBottom: "18px",
  },
  grid: {
    display: "grid",
    gridTemplateColumns: "repeat(auto-fit, minmax(220px, 1fr))",
    gap: "16px",
  },
  item: {
    background: "#101010",
    border: "1px solid #222",
    borderRadius: "14px",
    padding: "16px",
  },
  label: {
    fontSize: "13px",
    color: "#9ca3af",
    marginBottom: "8px",
  },
  value: {
    fontSize: "18px",
    fontWeight: "600",
    wordBreak: "break-word",
  },
  statsGrid: {
    display: "grid",
    gridTemplateColumns: "repeat(auto-fit, minmax(220px, 1fr))",
    gap: "16px",
  },
  statItem: {
    background: "#101010",
    border: "1px solid #222",
    borderRadius: "14px",
    padding: "18px",
  },
  statLabel: {
    fontSize: "14px",
    color: "#9ca3af",
    marginBottom: "10px",
  },
  statValue: {
    fontSize: "28px",
    fontWeight: "700",
  },
  tableWrap: {
    overflowX: "auto",
  },
  table: {
    width: "100%",
    borderCollapse: "collapse",
  },
  th: {
    textAlign: "left",
    padding: "12px",
    borderBottom: "1px solid #2a2a2a",
    color: "#9ca3af",
    fontSize: "14px",
    fontWeight: "600",
  },
  td: {
    padding: "14px 12px",
    borderBottom: "1px solid #1f1f1f",
    fontSize: "15px",
  },
  row: {
    background: "transparent",
  },
  empty: {
    color: "#9ca3af",
    fontSize: "15px",
  },
};