import { BrowserRouter as Router, Routes, Route } from "react-router-dom"
import { HomePage } from "@/pages/HomePage"
import { StudyPage } from "@/pages/StudyPage"
import { ProgressPage } from "@/pages/ProgressPage"
import { AchievementsPage } from "@/pages/AchievementsPage"
import { SettingsPage } from "@/pages/SettingsPage"
import { LoginPage } from "@/pages/LoginPage"
import { RegisterPage } from "@/pages/RegisterPage"
import { ForgotPasswordPage } from "@/pages/ForgotPasswordPage"
import { InstallPrompt } from "@/components/PWA/InstallPrompt"
import { AuthProvider } from "@/contexts/auth-context"
import { ProtectedRoute } from "@/components/auth/ProtectedRoute"
import { GuestRoute } from "@/components/auth/GuestRoute"
import "./index.css"

function App() {
  return (
    <Router>
      <AuthProvider>
        <div className="App">
          <Routes>
            {/* Trang chủ có thể truy cập cho tất cả người dùng */}
            <Route path="/" element={<HomePage />} />

            {/* Các trang yêu cầu đăng nhập */}
            <Route element={<ProtectedRoute />}>
              <Route path="/study" element={<StudyPage />} />
              <Route path="/progress" element={<ProgressPage />} />
              <Route path="/achievements" element={<AchievementsPage />} />
              <Route path="/settings" element={<SettingsPage />} />
            </Route>

            {/* Các trang dành cho khách */}
            <Route element={<GuestRoute />}>
              <Route path="/auth/login" element={<LoginPage />} />
              <Route path="/auth/register" element={<RegisterPage />} />
              <Route path="/auth/forgot-password" element={<ForgotPasswordPage />} />
            </Route>
          </Routes>
          <InstallPrompt />
        </div>
      </AuthProvider>
    </Router>
  )
}

export default App
