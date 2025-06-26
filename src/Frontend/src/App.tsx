import { BrowserRouter as Router, Routes, Route } from "react-router-dom"
import { HomePage } from "@/pages/HomePage"
import { StudyPage } from "@/pages/StudyPage"
import { ProgressPage } from "@/pages/ProgressPage"
import { AchievementsPage } from "@/pages/AchievementsPage"
import { SettingsPage } from "@/pages/SettingsPage"
import { InstallPrompt } from "@/components/PWA/InstallPrompt"
import { useNotification } from "@/hooks/useNotification"
import "./index.css"

function App() {
  useNotification()

  return (
    <Router>
      <div className="App">
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/study" element={<StudyPage />} />
          <Route path="/progress" element={<ProgressPage />} />
          <Route path="/achievements" element={<AchievementsPage />} />
          <Route path="/settings" element={<SettingsPage />} />
        </Routes>
        <InstallPrompt />
      </div>
    </Router>
  )
}

export default App
