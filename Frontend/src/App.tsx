import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom'
import { ThemeProvider } from './components/theme-provider'
import { AppLayout } from './components/Layout/app-layout'
import { ProtectedRoute } from './components/auth/ProtectedRoute'
import { GuestRoute } from './components/auth/GuestRoute'
import { Toaster } from './components/ui/toaster'
import { AuthProvider } from './contexts/auth-context'

// Pages
import HomePage from './app/page'
import LoginPage from './app/auth/login/page'
import RegisterPage from './app/auth/register/page'
import ForgotPasswordPage from './app/auth/forgot-password/page'
import ProfilePage from './app/profile/page'
import EditProfilePage from './app/edit-profile/page'
import SettingsPage from './app/settings/page'
import StudyPage from './app/study/page'
import ProgressPage from './app/progress/page'
import AchievementsPage from './app/achievements/page'

function App() {
  return (
    <ThemeProvider defaultTheme="light" storageKey="vite-ui-theme">
      <Toaster />
      <BrowserRouter>
      <AuthProvider>
          <Routes>
            {/* Các trang không yêu cầu đăng nhập */}
            <Route path="/" element={<AppLayout />}>
              <Route index element={<HomePage />} />
              
              {/* Trang xác thực */}
              <Route path="auth">
                <Route path="login" element={
                  <GuestRoute>
                    <LoginPage />
                  </GuestRoute>
                } />
                <Route path="register" element={
                  <GuestRoute>
                    <RegisterPage />
                  </GuestRoute>
                } />
                <Route path="forgot-password" element={
                  <GuestRoute>
                    <ForgotPasswordPage />
                  </GuestRoute>
                } />
              </Route>

            {/* Các trang yêu cầu đăng nhập */}
              <Route path="profile" element={
                <ProtectedRoute>
                  <ProfilePage />
                </ProtectedRoute>
              } />
              <Route path="edit-profile" element={
                <ProtectedRoute>
                  <EditProfilePage />
                </ProtectedRoute>
              } />
              <Route path="settings" element={
                <ProtectedRoute>
                  <SettingsPage />
                </ProtectedRoute>
              } />
              <Route path="study" element={
                <ProtectedRoute>
                  <StudyPage />
                </ProtectedRoute>
              } />
              <Route path="progress" element={
                <ProtectedRoute>
                  <ProgressPage />
                </ProtectedRoute>
              } />
              <Route path="achievements" element={
                <ProtectedRoute>
                  <AchievementsPage />
                </ProtectedRoute>
              } />
              
              {/* Trang không tồn tại */}
              <Route path="*" element={<Navigate to="/" replace />} />
            </Route>
          </Routes>
      </AuthProvider>
      </BrowserRouter>
    </ThemeProvider>
  )
}

export default App
