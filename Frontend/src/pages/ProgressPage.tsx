"use client"

import { useState } from "react"
import { Layout } from "@/components/Layout/Layout"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Progress } from "@/components/ui/progress"
import { Badge } from "@/components/ui/badge"
import { Button } from "@/components/ui/button"
import { useAppStore } from "@/store/useAppStore"
import { calculateLevel, formatTime } from "@/lib/utils"
import { vocabularyCategories } from "@/data/vocabulary"
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer, PieChart, Pie, Cell } from "recharts"
import {
  Calendar,
  TrendingUp,
  BookOpen,
  Target,
  Flame,
  Trophy,
  Clock,
  Award,
  BarChart3,
  PieChartIcon,
} from "lucide-react"

export function ProgressPage() {
  const { userProgress, studySessions, vocabulary } = useAppStore()
  const [selectedPeriod, setSelectedPeriod] = useState<"week" | "month" | "year">("week")

  const currentLevel = calculateLevel(userProgress.experience)
  const nextLevelExp = currentLevel * 100
  const currentLevelProgress = ((userProgress.experience % 100) / 100) * 100

  // Calculate statistics
  const totalStudyTime = studySessions.reduce((total, session) => total + session.duration, 0)
  const averageAccuracy =
    studySessions.length > 0
      ? studySessions.reduce((total, session) => total + (session.correctAnswers / session.wordsStudied) * 100, 0) /
        studySessions.length
      : 0

  // Weekly progress data
  const weeklyData = Array.from({ length: 7 }, (_, i) => {
    const date = new Date()
    date.setDate(date.getDate() - (6 - i))
    const dayName = date.toLocaleDateString("vi-VN", { weekday: "short" })

    // Find sessions for this day
    const daySessions = studySessions.filter((session) => {
      const sessionDate = new Date(session.date)
      return sessionDate.toDateString() === date.toDateString()
    })

    const wordsLearned = daySessions.reduce((total, session) => total + session.correctAnswers, 0)
    const timeSpent = daySessions.reduce((total, session) => total + session.duration, 0)

    return {
      day: dayName,
      words: wordsLearned,
      time: Math.round(timeSpent / 60), // Convert to minutes
    }
  })

  // Category distribution
  const categoryData = vocabularyCategories.map((category) => {
    const categoryWords = vocabulary.filter((word) => word.category === category)
    const learnedWords = categoryWords.filter((word) => word.learned).length

    return {
      name: category,
      total: categoryWords.length,
      learned: learnedWords,
      percentage: categoryWords.length > 0 ? Math.round((learnedWords / categoryWords.length) * 100) : 0,
    }
  })

  // Difficulty distribution
  const difficultyData = [
    {
      name: "Cơ bản",
      value: vocabulary.filter((word) => word.difficulty === "beginner" && word.learned).length,
      color: "#10B981",
    },
    {
      name: "Trung bình",
      value: vocabulary.filter((word) => word.difficulty === "intermediate" && word.learned).length,
      color: "#F59E0B",
    },
    {
      name: "Nâng cao",
      value: vocabulary.filter((word) => word.difficulty === "advanced" && word.learned).length,
      color: "#EF4444",
    },
  ]

  const achievements = [
    {
      title: "Streak Master",
      description: `${userProgress.currentStreak} ngày liên tiếp`,
      icon: "🔥",
      progress: Math.min((userProgress.currentStreak / 30) * 100, 100),
      target: "30 ngày",
    },
    {
      title: "Word Collector",
      description: `${userProgress.learnedWords} từ đã học`,
      icon: "📚",
      progress: Math.min((userProgress.learnedWords / 100) * 100, 100),
      target: "100 từ",
    },
    {
      title: "Point Hunter",
      description: `${userProgress.totalPoints} điểm`,
      icon: "⭐",
      progress: Math.min((userProgress.totalPoints / 1000) * 100, 100),
      target: "1000 điểm",
    },
  ]

  return (
    <Layout>
      <div className="space-y-8">
        {/* Header */}
        <div className="flex flex-col md:flex-row md:items-center md:justify-between gap-4">
          <div>
            <h1 className="text-3xl font-bold text-gray-800 flex items-center gap-2">
              <BarChart3 className="w-8 h-8 text-purple-600" />
              Tiến độ học tập
            </h1>
            <p className="text-gray-600 mt-1">Theo dõi quá trình học từ vựng của bạn</p>
          </div>

          <div className="flex gap-2">
            {(["week", "month", "year"] as const).map((period) => (
              <Button
                key={period}
                variant={selectedPeriod === period ? "default" : "outline"}
                size="sm"
                onClick={() => setSelectedPeriod(period)}
              >
                {period === "week" ? "Tuần" : period === "month" ? "Tháng" : "Năm"}
              </Button>
            ))}
          </div>
        </div>

        {/* Overview Stats */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
          <Card className="animate-fade-in">
            <CardContent className="p-6">
              <div className="flex items-center justify-between">
                <div>
                  <p className="text-sm text-gray-600">Tổng thời gian học</p>
                  <p className="text-2xl font-bold text-gray-800">{formatTime(totalStudyTime)}</p>
                </div>
                <Clock className="w-8 h-8 text-blue-500" />
              </div>
            </CardContent>
          </Card>

          <Card className="animate-fade-in" style={{ animationDelay: "0.1s" }}>
            <CardContent className="p-6">
              <div className="flex items-center justify-between">
                <div>
                  <p className="text-sm text-gray-600">Độ chính xác</p>
                  <p className="text-2xl font-bold text-gray-800">{Math.round(averageAccuracy)}%</p>
                </div>
                <Target className="w-8 h-8 text-green-500" />
              </div>
            </CardContent>
          </Card>

          <Card className="animate-fade-in" style={{ animationDelay: "0.2s" }}>
            <CardContent className="p-6">
              <div className="flex items-center justify-between">
                <div>
                  <p className="text-sm text-gray-600">Sessions hoàn thành</p>
                  <p className="text-2xl font-bold text-gray-800">{studySessions.length}</p>
                </div>
                <Award className="w-8 h-8 text-purple-500" />
              </div>
            </CardContent>
          </Card>

          <Card className="animate-fade-in" style={{ animationDelay: "0.3s" }}>
            <CardContent className="p-6">
              <div className="flex items-center justify-between">
                <div>
                  <p className="text-sm text-gray-600">Streak dài nhất</p>
                  <p className="text-2xl font-bold text-gray-800">{userProgress.longestStreak}</p>
                </div>
                <Flame className="w-8 h-8 text-orange-500" />
              </div>
            </CardContent>
          </Card>
        </div>

        {/* Level Progress */}
        <Card className="animate-fade-in" style={{ animationDelay: "0.4s" }}>
          <CardHeader>
            <CardTitle className="flex items-center gap-2">
              <Trophy className="w-5 h-5" />
              Tiến độ cấp độ
            </CardTitle>
          </CardHeader>
          <CardContent>
            <div className="space-y-4">
              <div className="flex items-center justify-between">
                <div className="flex items-center gap-3">
                  <div className="w-12 h-12 bg-gradient-to-br from-purple-500 to-blue-500 rounded-full flex items-center justify-center text-white font-bold text-lg">
                    {currentLevel}
                  </div>
                  <div>
                    <h3 className="font-semibold">Cấp độ {currentLevel}</h3>
                    <p className="text-sm text-gray-600">
                      {userProgress.experience % 100}/{100} XP
                    </p>
                  </div>
                </div>
                <Badge variant="secondary">Còn {100 - (userProgress.experience % 100)} XP</Badge>
              </div>
              <Progress value={currentLevelProgress} className="h-3" />
            </div>
          </CardContent>
        </Card>

        {/* Charts Section */}
        <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
          {/* Weekly Activity */}
          <Card className="animate-fade-in" style={{ animationDelay: "0.5s" }}>
            <CardHeader>
              <CardTitle className="flex items-center gap-2">
                <Calendar className="w-5 h-5" />
                Hoạt động 7 ngày qua
              </CardTitle>
            </CardHeader>
            <CardContent>
              <ResponsiveContainer width="100%" height={300}>
                <BarChart data={weeklyData}>
                  <CartesianGrid strokeDasharray="3 3" />
                  <XAxis dataKey="day" />
                  <YAxis />
                  <Tooltip formatter={(value, name) => [value, name === "words" ? "Từ đã học" : "Phút học"]} />
                  <Bar dataKey="words" fill="#8B5CF6" name="words" />
                </BarChart>
              </ResponsiveContainer>
            </CardContent>
          </Card>

          {/* Difficulty Distribution */}
          <Card className="animate-fade-in" style={{ animationDelay: "0.6s" }}>
            <CardHeader>
              <CardTitle className="flex items-center gap-2">
                <PieChartIcon className="w-5 h-5" />
                Phân bố theo độ khó
              </CardTitle>
            </CardHeader>
            <CardContent>
              <ResponsiveContainer width="100%" height={300}>
                <PieChart>
                  <Pie
                    data={difficultyData}
                    cx="50%"
                    cy="50%"
                    outerRadius={80}
                    fill="#8884d8"
                    dataKey="value"
                    label={({ name, value }) => `${name}: ${value}`}
                  >
                    {difficultyData.map((entry, index) => (
                      <Cell key={`cell-${index}`} fill={entry.color} />
                    ))}
                  </Pie>
                  <Tooltip />
                </PieChart>
              </ResponsiveContainer>
            </CardContent>
          </Card>
        </div>

        {/* Category Progress */}
        <Card className="animate-fade-in" style={{ animationDelay: "0.7s" }}>
          <CardHeader>
            <CardTitle className="flex items-center gap-2">
              <BookOpen className="w-5 h-5" />
              Tiến độ theo danh mục
            </CardTitle>
          </CardHeader>
          <CardContent>
            <div className="space-y-4">
              {categoryData.map((category, index) => (
                <div key={category.name} className="space-y-2">
                  <div className="flex justify-between items-center">
                    <span className="font-medium">{category.name}</span>
                    <div className="flex items-center gap-2">
                      <span className="text-sm text-gray-600">
                        {category.learned}/{category.total}
                      </span>
                      <Badge variant="outline">{category.percentage}%</Badge>
                    </div>
                  </div>
                  <Progress value={category.percentage} className="h-2" />
                </div>
              ))}
            </div>
          </CardContent>
        </Card>

        {/* Achievements Progress */}
        <Card className="animate-fade-in" style={{ animationDelay: "0.8s" }}>
          <CardHeader>
            <CardTitle className="flex items-center gap-2">
              <Trophy className="w-5 h-5" />
              Tiến độ thành tích
            </CardTitle>
          </CardHeader>
          <CardContent>
            <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
              {achievements.map((achievement, index) => (
                <div key={achievement.title} className="space-y-3">
                  <div className="flex items-center gap-3">
                    <div className="text-2xl">{achievement.icon}</div>
                    <div>
                      <h4 className="font-semibold">{achievement.title}</h4>
                      <p className="text-sm text-gray-600">{achievement.description}</p>
                    </div>
                  </div>
                  <div className="space-y-1">
                    <div className="flex justify-between text-sm">
                      <span>Tiến độ</span>
                      <span>{Math.round(achievement.progress)}%</span>
                    </div>
                    <Progress value={achievement.progress} className="h-2" />
                    <p className="text-xs text-gray-500">Mục tiêu: {achievement.target}</p>
                  </div>
                </div>
              ))}
            </div>
          </CardContent>
        </Card>

        {/* Recent Sessions */}
        <Card className="animate-fade-in" style={{ animationDelay: "0.9s" }}>
          <CardHeader>
            <CardTitle className="flex items-center gap-2">
              <TrendingUp className="w-5 h-5" />
              Sessions gần đây
            </CardTitle>
          </CardHeader>
          <CardContent>
            <div className="space-y-4">
              {studySessions.slice(0, 5).map((session, index) => (
                <div key={session.id} className="flex items-center justify-between p-3 bg-gray-50 rounded-lg">
                  <div className="flex items-center gap-3">
                    <div className="w-10 h-10 bg-purple-100 rounded-full flex items-center justify-center">
                      <BookOpen className="w-5 h-5 text-purple-600" />
                    </div>
                    <div>
                      <p className="font-medium">{new Date(session.date).toLocaleDateString("vi-VN")}</p>
                      <p className="text-sm text-gray-600">
                        {session.wordsStudied} từ • {formatTime(session.duration)}
                      </p>
                    </div>
                  </div>
                  <div className="text-right">
                    <p className="font-semibold text-green-600">+{session.pointsEarned}</p>
                    <p className="text-sm text-gray-600">
                      {Math.round((session.correctAnswers / session.wordsStudied) * 100)}% chính xác
                    </p>
                  </div>
                </div>
              ))}

              {studySessions.length === 0 && (
                <div className="text-center py-8 text-gray-500">
                  <BookOpen className="w-12 h-12 mx-auto mb-3 opacity-50" />
                  <p>Chưa có session học tập nào</p>
                  <p className="text-sm">Bắt đầu học để xem thống kê!</p>
                </div>
              )}
            </div>
          </CardContent>
        </Card>
      </div>
    </Layout>
  )
}
