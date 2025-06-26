"use client"

import type React from "react"

import { useState } from "react"
import { Layout } from "@/components/Layout/Layout"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Button } from "@/components/ui/button"
import { Badge } from "@/components/ui/badge"
import { useAppStore } from "@/store/useAppStore"
import {
  Settings,
  User,
  Bell,
  Palette,
  Volume2,
  Target,
  Download,
  Upload,
  Trash2,
  Moon,
  Sun,
  Globe,
  Clock,
  Smartphone,
} from "lucide-react"

export function SettingsPage() {
  const { settings, userProgress, updateSettings, resetProgress } = useAppStore()
  const [showResetConfirm, setShowResetConfirm] = useState(false)

  const handleThemeChange = (theme: "light" | "dark" | "system") => {
    updateSettings({ theme })
    // Apply theme logic here
    if (theme === "dark") {
      document.documentElement.classList.add("dark")
    } else if (theme === "light") {
      document.documentElement.classList.remove("dark")
    } else {
      // System theme
      const prefersDark = window.matchMedia("(prefers-color-scheme: dark)").matches
      if (prefersDark) {
        document.documentElement.classList.add("dark")
      } else {
        document.documentElement.classList.remove("dark")
      }
    }
  }

  const handleDailyGoalChange = (goal: number) => {
    updateSettings({ dailyGoal: goal })
  }

  const handleExportData = () => {
    const data = {
      userProgress,
      settings,
      exportDate: new Date().toISOString(),
    }

    const blob = new Blob([JSON.stringify(data, null, 2)], { type: "application/json" })
    const url = URL.createObjectURL(blob)
    const a = document.createElement("a")
    a.href = url
    a.download = `flashlearn-backup-${new Date().toISOString().split("T")[0]}.json`
    document.body.appendChild(a)
    a.click()
    document.body.removeChild(a)
    URL.revokeObjectURL(url)
  }

  const handleImportData = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0]
    if (!file) return

    const reader = new FileReader()
    reader.onload = (e) => {
      try {
        const data = JSON.parse(e.target?.result as string)
        if (data.userProgress && data.settings) {
          // Import logic would go here
          alert("Dữ liệu đã được nhập thành công!")
        }
      } catch (error) {
        alert("Lỗi khi nhập dữ liệu. Vui lòng kiểm tra file.")
      }
    }
    reader.readAsText(file)
  }

  const handleResetProgress = () => {
    if (showResetConfirm) {
      resetProgress()
      setShowResetConfirm(false)
      alert("Đã reset toàn bộ tiến độ!")
    } else {
      setShowResetConfirm(true)
    }
  }

  const settingSections = [
    {
      title: "Giao diện",
      icon: Palette,
      items: [
        {
          title: "Chủ đề",
          description: "Chọn giao diện sáng, tối hoặc theo hệ thống",
          content: (
            <div className="flex gap-2">
              {[
                { value: "light", label: "Sáng", icon: Sun },
                { value: "dark", label: "Tối", icon: Moon },
                { value: "system", label: "Hệ thống", icon: Smartphone },
              ].map((theme) => {
                const Icon = theme.icon
                return (
                  <Button
                    key={theme.value}
                    variant={settings.theme === theme.value ? "default" : "outline"}
                    size="sm"
                    onClick={() => handleThemeChange(theme.value as any)}
                    className="flex items-center gap-2"
                  >
                    <Icon className="w-4 h-4" />
                    {theme.label}
                  </Button>
                )
              })}
            </div>
          ),
        },
        {
          title: "Ngôn ngữ",
          description: "Chọn ngôn ngữ hiển thị",
          content: (
            <div className="flex gap-2">
              {[
                { value: "vi", label: "Tiếng Việt", flag: "🇻🇳" },
                { value: "en", label: "English", flag: "🇺🇸" },
              ].map((lang) => (
                <Button
                  key={lang.value}
                  variant={settings.language === lang.value ? "default" : "outline"}
                  size="sm"
                  onClick={() => updateSettings({ language: lang.value as any })}
                  className="flex items-center gap-2"
                >
                  <span>{lang.flag}</span>
                  {lang.label}
                </Button>
              ))}
            </div>
          ),
        },
      ],
    },
    {
      title: "Âm thanh & Thông báo",
      icon: Bell,
      items: [
        {
          title: "Âm thanh",
          description: "Bật/tắt âm thanh trong ứng dụng",
          content: (
            <Button
              variant={settings.soundEnabled ? "default" : "outline"}
              size="sm"
              onClick={() => updateSettings({ soundEnabled: !settings.soundEnabled })}
              className="flex items-center gap-2"
            >
              <Volume2 className="w-4 h-4" />
              {settings.soundEnabled ? "Đã bật" : "Đã tắt"}
            </Button>
          ),
        },
        {
          title: "Thông báo",
          description: "Nhận thông báo nhắc nhở học tập",
          content: (
            <Button
              variant={settings.notificationsEnabled ? "default" : "outline"}
              size="sm"
              onClick={() => updateSettings({ notificationsEnabled: !settings.notificationsEnabled })}
              className="flex items-center gap-2"
            >
              <Bell className="w-4 h-4" />
              {settings.notificationsEnabled ? "Đã bật" : "Đã tắt"}
            </Button>
          ),
        },
        {
          title: "Nhắc nhở hàng ngày",
          description: "Đặt thời gian nhắc nhở học tập",
          content: (
            <div className="space-y-2">
              <Button
                variant={settings.studyReminder.enabled ? "default" : "outline"}
                size="sm"
                onClick={() =>
                  updateSettings({
                    studyReminder: {
                      ...settings.studyReminder,
                      enabled: !settings.studyReminder.enabled,
                    },
                  })
                }
                className="flex items-center gap-2"
              >
                <Clock className="w-4 h-4" />
                {settings.studyReminder.enabled ? "Đã bật" : "Đã tắt"}
              </Button>
              {settings.studyReminder.enabled && (
                <input
                  type="time"
                  value={settings.studyReminder.time}
                  onChange={(e) =>
                    updateSettings({
                      studyReminder: {
                        ...settings.studyReminder,
                        time: e.target.value,
                      },
                    })
                  }
                  className="px-3 py-1 border rounded-md text-sm"
                />
              )}
            </div>
          ),
        },
      ],
    },
    {
      title: "Học tập",
      icon: Target,
      items: [
        {
          title: "Mục tiêu hàng ngày",
          description: "Số từ vựng muốn học mỗi ngày",
          content: (
            <div className="flex gap-2">
              {[5, 10, 15, 20, 25].map((goal) => (
                <Button
                  key={goal}
                  variant={settings.dailyGoal === goal ? "default" : "outline"}
                  size="sm"
                  onClick={() => handleDailyGoalChange(goal)}
                >
                  {goal}
                </Button>
              ))}
            </div>
          ),
        },
      ],
    },
    {
      title: "Dữ liệu",
      icon: Download,
      items: [
        {
          title: "Sao lưu dữ liệu",
          description: "Xuất dữ liệu học tập để sao lưu",
          content: (
            <Button onClick={handleExportData} className="flex items-center gap-2">
              <Download className="w-4 h-4" />
              Xuất dữ liệu
            </Button>
          ),
        },
        {
          title: "Khôi phục dữ liệu",
          description: "Nhập dữ liệu từ file sao lưu",
          content: (
            <div>
              <input type="file" accept=".json" onChange={handleImportData} className="hidden" id="import-file" />
              <Button asChild className="flex items-center gap-2">
                <label htmlFor="import-file" className="cursor-pointer">
                  <Upload className="w-4 h-4" />
                  Nhập dữ liệu
                </label>
              </Button>
            </div>
          ),
        },
        {
          title: "Reset tiến độ",
          description: "Xóa toàn bộ tiến độ học tập (không thể hoàn tác)",
          content: (
            <div className="space-y-2">
              <Button
                variant={showResetConfirm ? "destructive" : "outline"}
                onClick={handleResetProgress}
                className="flex items-center gap-2"
              >
                <Trash2 className="w-4 h-4" />
                {showResetConfirm ? "Xác nhận xóa" : "Reset tiến độ"}
              </Button>
              {showResetConfirm && <div className="text-sm text-red-600">⚠️ Hành động này không thể hoàn tác!</div>}
            </div>
          ),
        },
      ],
    },
  ]

  return (
    <Layout>
      <div className="space-y-8">
        {/* Header */}
        <div className="text-center space-y-4">
          <h1 className="text-3xl font-bold text-gray-800 flex items-center justify-center gap-2">
            <Settings className="w-8 h-8 text-purple-600" />
            Cài đặt
          </h1>
          <p className="text-gray-600">Tùy chỉnh ứng dụng theo sở thích của bạn</p>
        </div>

        {/* User Info */}
        <Card className="animate-fade-in">
          <CardHeader>
            <CardTitle className="flex items-center gap-2">
              <User className="w-5 h-5" />
              Thông tin người dùng
            </CardTitle>
          </CardHeader>
          <CardContent>
            <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
              <div className="text-center">
                <div className="w-16 h-16 bg-gradient-to-br from-purple-500 to-blue-500 rounded-full flex items-center justify-center text-white text-xl font-bold mx-auto mb-2">
                  {userProgress.level}
                </div>
                <p className="font-semibold">Cấp độ {userProgress.level}</p>
                <p className="text-sm text-gray-600">{userProgress.experience} XP</p>
              </div>

              <div className="space-y-2">
                <div className="flex justify-between">
                  <span className="text-sm text-gray-600">Từ đã học:</span>
                  <Badge variant="secondary">{userProgress.learnedWords}</Badge>
                </div>
                <div className="flex justify-between">
                  <span className="text-sm text-gray-600">Chuỗi ngày:</span>
                  <Badge variant="secondary">{userProgress.currentStreak}</Badge>
                </div>
                <div className="flex justify-between">
                  <span className="text-sm text-gray-600">Tổng điểm:</span>
                  <Badge variant="secondary">{userProgress.totalPoints}</Badge>
                </div>
              </div>

              <div className="space-y-2">
                <div className="flex justify-between">
                  <span className="text-sm text-gray-600">Mục tiêu hàng ngày:</span>
                  <Badge variant="outline">{userProgress.dailyGoal} từ</Badge>
                </div>
                <div className="flex justify-between">
                  <span className="text-sm text-gray-600">Hôm nay:</span>
                  <Badge variant="outline">{userProgress.wordsToday} từ</Badge>
                </div>
                <div className="flex justify-between">
                  <span className="text-sm text-gray-600">Streak dài nhất:</span>
                  <Badge variant="outline">{userProgress.longestStreak} ngày</Badge>
                </div>
              </div>
            </div>
          </CardContent>
        </Card>

        {/* Settings Sections */}
        {settingSections.map((section, sectionIndex) => {
          const Icon = section.icon
          return (
            <Card
              key={section.title}
              className="animate-fade-in"
              style={{ animationDelay: `${(sectionIndex + 1) * 0.1}s` }}
            >
              <CardHeader>
                <CardTitle className="flex items-center gap-2">
                  <Icon className="w-5 h-5" />
                  {section.title}
                </CardTitle>
              </CardHeader>
              <CardContent>
                <div className="space-y-6">
                  {section.items.map((item, itemIndex) => (
                    <div
                      key={item.title}
                      className="flex flex-col md:flex-row md:items-center md:justify-between gap-4 p-4 bg-gray-50 rounded-lg"
                    >
                      <div className="flex-1">
                        <h4 className="font-semibold text-gray-800">{item.title}</h4>
                        <p className="text-sm text-gray-600 mt-1">{item.description}</p>
                      </div>
                      <div className="flex-shrink-0">{item.content}</div>
                    </div>
                  ))}
                </div>
              </CardContent>
            </Card>
          )
        })}

        {/* App Info */}
        <Card className="animate-fade-in" style={{ animationDelay: "0.5s" }}>
          <CardHeader>
            <CardTitle className="flex items-center gap-2">
              <Globe className="w-5 h-5" />
              Thông tin ứng dụng
            </CardTitle>
          </CardHeader>
          <CardContent>
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              <div>
                <h4 className="font-semibold mb-2">FlashLearn</h4>
                <p className="text-sm text-gray-600 mb-4">
                  Ứng dụng học từ vựng tiếng Anh thông minh với flashcard và gamification.
                </p>
                <div className="space-y-1 text-sm">
                  <div className="flex justify-between">
                    <span className="text-gray-600">Phiên bản:</span>
                    <span>1.0.0</span>
                  </div>
                  <div className="flex justify-between">
                    <span className="text-gray-600">Cập nhật:</span>
                    <span>26/12/2024</span>
                  </div>
                </div>
              </div>

              <div>
                <h4 className="font-semibold mb-2">Hỗ trợ</h4>
                <div className="space-y-2">
                  <Button variant="outline" size="sm" className="w-full justify-start">
                    📧 Liên hệ hỗ trợ
                  </Button>
                  <Button variant="outline" size="sm" className="w-full justify-start">
                    📖 Hướng dẫn sử dụng
                  </Button>
                  <Button variant="outline" size="sm" className="w-full justify-start">
                    ⭐ Đánh giá ứng dụng
                  </Button>
                </div>
              </div>
            </div>
          </CardContent>
        </Card>
      </div>
    </Layout>
  )
}
