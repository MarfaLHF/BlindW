import { useEffect, useMemo, useRef, useState, useCallback } from "react";
import { useNavigate } from "react-router-dom";

import { getRandomText } from "../../services/textService";
import { findOrCreateTestSetting } from "../../services/testSettingService";
import { saveTestResult } from "../../services/testResultService";
import { useAuth } from "../../context/AuthContext";

import { buildTestResultPayload } from "./utils/buildTestResultPayload";
import { mapSettingsToApi } from "./utils/mapSettingsToApi";

import TextViewport from "./components/TextViewport";
import FocusOverlay from "./components/FocusOverlay";
import StatsBar from "./components/StatsBar";
import TestSettings from "./components/TestSettings";

import { useResizeWidth } from "./hooks/useResizeWidth";
import { useWrappedLines } from "./hooks/useWrappedLines";
import { useTypingEngine } from "./hooks/useTypingEngine";
import { useInputFocus } from "./hooks/useInputFocus";
import { useTestTimer } from "./hooks/useTestTimer";
import { useTypingMetrics } from "./hooks/useTypingMetrics";

import {
  LINES_VISIBLE,
  FONT_SIZE,
  FONT_FAMILY,
  LINE_GAP_PX,
} from "./styles";

export default function TestPage() {
  const navigate = useNavigate();
  const { isAuthenticated, user } = useAuth();

  const hasHandledFinishRef = useRef(false);
  const containerRef = useRef(null);
  const inputRef = useRef(null);

  const [words, setWords] = useState([]);
  const [loading, setLoading] = useState(true);
  const [err, setErr] = useState("");
  const [isSavingResult, setIsSavingResult] = useState(false);

  const [settings, setSettings] = useState({
    mode: "words",
    wordCount: 25,
    durationSec: 30,
    language: "en",
    isNumbersEnabled: false,
    isPunctuationEnabled: false,
  });

  const targetText = useMemo(() => {
    return words.length ? words.join(" ") : "";
  }, [words]);

  const { hasFocus, focusInput } = useInputFocus(inputRef);
  const containerWidth = useResizeWidth(containerRef, 800);

  const loadText = useCallback(async () => {
    setLoading(true);
    setErr("");

    try {
      const mapped = mapSettingsToApi(settings);

      const requestedWordCount =
        settings.mode === "time" ? 300 : settings.wordCount;

      const requestBody = {
        wordCount: requestedWordCount,
        languageCode: mapped.languageCode,
        isNumbersEnabled: settings.isNumbersEnabled,
        isPunctuationEnabled: settings.isPunctuationEnabled,
      };

      console.log("GET TEXT REQUEST BODY:", requestBody);

      const data = await getRandomText(requestBody);

      setWords(data);
      hasHandledFinishRef.current = false;

      requestAnimationFrame(() => {
        inputRef.current?.focus();
      });
    } catch (error) {
      console.error("Ошибка загрузки текста:", error);
      setErr("Не удалось получить текст с API.");
      setWords([]);
    } finally {
      setLoading(false);
    }
  }, [settings]);

  const typing = useTypingEngine(targetText, {
    onRestart: async () => {
      await loadText();
    },
  });

  const timer = useTestTimer(typing.status);

  const metrics = useTypingMetrics({
    typedMap: typing.typedMap,
    elapsedMs: timer.elapsedMs,
  });

  useEffect(() => {
    loadText();
  }, [loadText]);

  useEffect(() => {
    if (settings.mode !== "time") return;
    if (typing.status !== "running") return;

    if (timer.elapsedMs >= settings.durationSec * 1000) {
      typing.setStatus("finished");
    }
  }, [settings.mode, settings.durationSec, timer.elapsedMs, typing]);

  useEffect(() => {
    if (typing.status !== "finished") {
      hasHandledFinishRef.current = false;
      return;
    }

    if (hasHandledFinishRef.current) return;
    hasHandledFinishRef.current = true;

    async function handleFinishedTest() {
      const resultState = {
        wpm: metrics.wpm,
        accuracy: metrics.accuracy,
        wrong: metrics.wrong,
        elapsedMs: timer.elapsedMs,
        mode: settings.mode,
        wordCount: settings.wordCount,
        durationSec: settings.durationSec,
        language: settings.language,
        isNumbersEnabled: settings.isNumbersEnabled,
        isPunctuationEnabled: settings.isPunctuationEnabled,
        countCharacters: targetText.length,
      };

      if (isAuthenticated) {
        try {
          setIsSavingResult(true);

          const mapped = mapSettingsToApi(settings);

          const foundOrCreatedSetting = await findOrCreateTestSetting({
            isPunctuationEnabled: mapped.isPunctuationEnabled,
            isNumbersEnabled: mapped.isNumbersEnabled,
            testTypeId: mapped.testTypeId,
            wordCountId: mapped.wordCountId,
            testDurationId: mapped.testDurationId,
            languageId: mapped.languageId,
          });

          const payload = buildTestResultPayload({
            metrics,
            elapsedMs: timer.elapsedMs,
            targetText,
            user,
            testSettingId: foundOrCreatedSetting.testSettingId,
          });

          console.log("USER:", user);
          console.log("TEST SETTING:", foundOrCreatedSetting);
          console.log("SAVE PAYLOAD:", payload);

          if (!payload.userId) {
            console.error("Не найден userId для сохранения результата");
          } else {
            await saveTestResult(payload);
          }
        } catch (error) {
          console.error("Ошибка сохранения результата:", error);
        } finally {
          setIsSavingResult(false);
        }
      }

      navigate("/result", {
        state: resultState,
        replace: true,
      });
    }

    handleFinishedTest();
  }, [
    typing.status,
    metrics.wpm,
    metrics.accuracy,
    metrics.wrong,
    timer.elapsedMs,
    settings,
    targetText,
    isAuthenticated,
    user,
    navigate,
  ]);

  const lines = useWrappedLines({
    text: targetText,
    width: containerWidth,
    fontSize: FONT_SIZE,
    fontFamily: FONT_FAMILY,
  });

  const lineStarts = useMemo(() => {
    const starts = [];
    let pos = 0;

    for (let i = 0; i < lines.length; i++) {
      starts.push(pos);
      pos += lines[i].length;
    }

    return starts;
  }, [lines]);

  const currentLineIndex = useMemo(() => {
    for (let i = lineStarts.length - 1; i >= 0; i--) {
      if (typing.typedIndex >= lineStarts[i]) return i;
    }
    return 0;
  }, [typing.typedIndex, lineStarts]);

  const windowStart = Math.max(0, currentLineIndex - 1);
  const visibleLines = lines.slice(windowStart, windowStart + LINES_VISIBLE);

  const ensureFocus = () => {
    if (typing.status !== "finished") {
      focusInput();
    }
  };

  const handleNewText = async () => {
    timer.reset();
    await loadText();
  };

  if (loading) {
    return <div style={{ padding: 40, fontSize: 18 }}>Загрузка…</div>;
  }

  if (err) {
    return <div style={{ padding: 40, color: "tomato" }}>{err}</div>;
  }

  return (
    <div
      onMouseDown={ensureFocus}
      style={{
        minHeight: "100vh",
        background: "#0b0b0b",
        color: "white",
        display: "grid",
        placeItems: "center",
        padding: 24,
      }}
    >
      <div style={{ width: "min(980px, 92vw)" }}>
        <h1 style={{ margin: 0, marginBottom: 14, textAlign: "center" }}>
          BlindW — Тест
        </h1>

        <TestSettings settings={settings} setSettings={setSettings} />

        <div style={{ position: "relative" }}>
          <div
            style={{
              border: "1px solid #222",
              borderRadius: 14,
              padding: 18,
              fontSize: FONT_SIZE,
              fontFamily: FONT_FAMILY,
              lineHeight: `${FONT_SIZE + LINE_GAP_PX}px`,
              userSelect: "none",
              background: "#0f0f0f",
            }}
          >
            <TextViewport
              containerRef={containerRef}
              visibleLines={visibleLines}
              windowStart={windowStart}
              lineStarts={lineStarts}
              typedIndex={typing.typedIndex}
              typedMap={typing.typedMap}
            />
          </div>

          <FocusOverlay
            show={!hasFocus && typing.status !== "finished"}
            onFocus={focusInput}
          />
        </div>

        <input
          ref={inputRef}
          autoFocus
          onKeyDown={typing.onKeyDown}
          onKeyUp={typing.onKeyUp}
          onInput={typing.onInput}
          disabled={typing.status === "finished"}
          style={{
            position: "absolute",
            opacity: 0,
            width: 1,
            height: 1,
            left: -9999,
          }}
        />

        <StatsBar
          onNewText={handleNewText}
          typedIndex={typing.typedIndex}
          total={targetText.length}
          status={typing.status}
          metrics={metrics}
          elapsedMs={timer.elapsedMs}
        />

        {isAuthenticated && isSavingResult && (
          <div
            style={{
              marginTop: 12,
              textAlign: "center",
              opacity: 0.8,
              fontSize: 14,
            }}
          >
            Сохранение результата...
          </div>
        )}
      </div>
    </div>
  );
}