import { renderChar } from "../../../utils/text";

export default function TextViewport({
  containerRef,
  visibleLines,
  windowStart,
  lineStarts,
  typedIndex,
  typedMap,
}) {
  const renderLine = (lineText, lineIdxInWindow) => {
    const globalLineIndex = windowStart + lineIdxInWindow;
    const lineStart = lineStarts[globalLineIndex] ?? 0;

    const spans = [];
    for (let i = 0; i < lineText.length; i++) {
      const g = lineStart + i;

      const typed = typedMap.get(g);
      const isCurrent = g === typedIndex;

      let style = { opacity: 0.78 };
      if (typed === true) style = { color: "#7CFC00" };
      if (typed === false) style = { color: "tomato" };
      if (typed === undefined) style = { opacity: 0.78 };
      if (isCurrent) style = { ...style, background: "rgba(255,255,255,0.15)" };

      spans.push(
        <span key={g} style={style}>
          {renderChar(lineText[i])}
        </span>
      );
    }

    return <div style={{ whiteSpace: "nowrap" }}>{spans}</div>;
  };

  return (
    <div ref={containerRef}>
      {visibleLines.map((ln, i) => (
        <div key={windowStart + i}>{renderLine(ln, i)}</div>
      ))}
    </div>
  );
}