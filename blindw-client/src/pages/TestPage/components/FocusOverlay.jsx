export default function FocusOverlay({ show, onFocus }) {
  if (!show) return null;

  return (
    <div
      onMouseDown={(e) => {
        e.preventDefault();
        onFocus?.();
      }}
      style={{
        position: "absolute",
        inset: 0,
        borderRadius: 14,
        background: "rgba(0,0,0,0.55)",
        display: "grid",
        placeItems: "center",
        cursor: "pointer",
        backdropFilter: "blur(2px)",
      }}
    >
      <div
        style={{
          padding: "10px 14px",
          borderRadius: 12,
          border: "1px solid #333",
          background: "rgba(20,20,20,0.9)",
          fontSize: 16,
          opacity: 0.95,
        }}
      >
        Нажми, чтобы продолжить (вернём фокус)
      </div>
    </div>
  );
}