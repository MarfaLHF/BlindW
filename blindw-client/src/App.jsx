import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import Header from "./components/layout/Header";
import TestPage from "./pages/TestPage/TestPage";
import LoginPage from "./pages/Auth/LoginPage";
import RegisterPage from "./pages/Auth/RegisterPage";
import ProfilePage from "./pages/Profile/ProfilePage";
import ResultPage from "./pages/ResultPage/ResultPage";
import LeaderboardPage from "./pages/Leaderboard/LeaderboardPage";

function LessonsPage() {
  return <div style={{ padding: 40, color: "white" }}>Уроки — позже</div>;
}

export default function App() {
  return (
    <BrowserRouter>
      <div style={{ minHeight: "100vh", background: "#0b0b0b" }}>
        <Header />

        <Routes>
          <Route path="/" element={<Navigate to="/test" replace />} />
          <Route path="/test" element={<TestPage />} />
          <Route path="/result" element={<ResultPage />} />
          <Route path="/profile" element={<ProfilePage />} />
          <Route path="/leaderboard" element={<LeaderboardPage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />
          <Route path="/lessons" element={<LessonsPage />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}