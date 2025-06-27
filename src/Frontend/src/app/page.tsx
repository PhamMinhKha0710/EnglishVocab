"use client"

import { useState, useEffect } from "react"
import { Navigation } from "@/components/navigation"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Progress } from "@/components/ui/progress"
import { BookOpen, Target, Flame, Trophy, TrendingUp, Play, LogIn, UserPlus } from "lucide-react"
import Link from "next/link"
import { useAuth } from "@/contexts/auth-context"

export default function HomePage() {
  const [currentTime, setCurrentTime] = useState(new Date())
  const { isAuthenticated, user, isLoading } = useAuth()

  useEffect(() => {
    const timer = setInterval(() => setCurrentTime(new Date()), 1000)
    return () => clearInterval(timer)
  }, [])

  const userStats = {
    wordsLearned: 45,
    totalWords: 200,
    currentStreak: 7,
    dailyGoal: 10,
    todayProgress: 5,
    level: 3,
    experience: 280,
    nextLevelExp: 300,
  }

  const recentAchievements = [
    { id: 1, title: "Tuần đầu tiên", description: "Hoàn thành 7 ngày liên tiếp", icon: "🔥" },
    { id: 2, title: "Người mới bắt đầu", description: "Học 25 từ vựng đầu tiên", icon: "🌟" },
    { id: 3, title: "Chăm chỉ", description: "Học 50 từ trong tuần", icon: "💪" },
  ]

  // Loading state
  if (isLoading) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gradient-to-br from-purple-50 to-blue-50">
        <div className="text-center">
          <div className="w-16 h-16 border-4 border-purple-500 border-t-transparent rounded-full animate-spin mx-auto"></div>
          <p className="mt-4 text-gray-600">Đang tải...</p>
        </div>
      </div>
    )
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-purple-50 to-blue-50">
      <Navigation isAuthenticated={isAuthenticated} />

      <main className="md:ml-64 p-6">
        {/* Header */}
        <div className="mb-8">
          <div className="flex flex-col md:flex-row md:items-center md:justify-between gap-4">
            <div>
              {isAuthenticated ? (
                <>
                  <h1 className="text-3xl font-bold text-gray-800">
                    Chào mừng trở lại, {user?.firstName || "Bạn"}! 👋
                  </h1>
                  <p className="text-gray-600 mt-1">
                    {currentTime.toLocaleDateString("vi-VN", {
                      weekday: "long",
                      year: "numeric",
                      month: "long",
                      day: "numeric",
                    })}
                  </p>
                </>
              ) : (
                <>
                  <h1 className="text-3xl font-bold text-gray-800">
                    Chào mừng đến với FlashLearn! 👋
                  </h1>
                  <p className="text-gray-600 mt-1">
                    Học từ vựng tiếng Anh hiệu quả với thẻ ghi nhớ thông minh
                  </p>
                </>
              )}
            </div>
            {isAuthenticated ? (
              <Link href="/study">
                <Button size="lg" className="bg-purple-600 hover:bg-purple-700">
                  <Play className="w-4 h-4 mr-2" />
                  Bắt đầu học
                </Button>
              </Link>
            ) : (
              <div className="flex gap-3">
                <Link href="/login">
                  <Button variant="outline" size="lg">
                    <LogIn className="w-4 h-4 mr-2" />
                    Đăng nhập
                  </Button>
                </Link>
                <Link href="/register">
                  <Button size="lg" className="bg-purple-600 hover:bg-purple-700">
                    <UserPlus className="w-4 h-4 mr-2" />
                    Đăng ký
                  </Button>
                </Link>
              </div>
            )}
          </div>
        </div>

        {isAuthenticated ? (
          <>
            {/* Stats Grid - Only shown for authenticated users */}
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
              <Card>
                <CardContent className="p-6">
                  <div className="flex items-center justify-between">
                    <div>
                      <p className="text-sm text-gray-600">Từ đã học</p>
                      <p className="text-2xl font-bold text-gray-800">{userStats.wordsLearned}</p>
                    </div>
                    <BookOpen className="w-8 h-8 text-purple-500" />
                  </div>
                </CardContent>
              </Card>

              <Card>
                <CardContent className="p-6">
                  <div className="flex items-center justify-between">
                    <div>
                      <p className="text-sm text-gray-600">Chuỗi ngày</p>
                      <p className="text-2xl font-bold text-orange-600">{userStats.currentStreak}</p>
                    </div>
                    <Flame className="w-8 h-8 text-orange-500" />
                  </div>
                </CardContent>
              </Card>

              <Card>
                <CardContent className="p-6">
                  <div className="flex items-center justify-between">
                    <div>
                      <p className="text-sm text-gray-600">Cấp độ</p>
                      <p className="text-2xl font-bold text-blue-600">{userStats.level}</p>
                    </div>
                    <Trophy className="w-8 h-8 text-yellow-500" />
                  </div>
                </CardContent>
              </Card>

              <Card>
                <CardContent className="p-6">
                  <div className="flex items-center justify-between">
                    <div>
                      <p className="text-sm text-gray-600">Tiến độ hôm nay</p>
                      <p className="text-2xl font-bold text-green-600">
                        {userStats.todayProgress}/{userStats.dailyGoal}
                      </p>
                    </div>
                    <Target className="w-8 h-8 text-green-500" />
                  </div>
                </CardContent>
              </Card>
            </div>

            {/* Progress Section - Only shown for authenticated users */}
            <div className="grid grid-cols-1 lg:grid-cols-2 gap-6 mb-8">
              <Card>
                <CardHeader>
                  <CardTitle className="flex items-center gap-2">
                    <Target className="w-5 h-5" />
                    Mục tiêu hàng ngày
                  </CardTitle>
                </CardHeader>
                <CardContent>
                  <div className="space-y-4">
                    <div className="flex justify-between text-sm">
                      <span>Tiến độ hôm nay</span>
                      <span>
                        {userStats.todayProgress}/{userStats.dailyGoal} từ
                      </span>
                    </div>
                    <Progress value={(userStats.todayProgress / userStats.dailyGoal) * 100} className="h-3" />
                    <p className="text-sm text-gray-600">
                      Còn {userStats.dailyGoal - userStats.todayProgress} từ nữa để hoàn thành mục tiêu!
                    </p>
                  </div>
                </CardContent>
              </Card>

              <Card>
                <CardHeader>
                  <CardTitle className="flex items-center gap-2">
                    <TrendingUp className="w-5 h-5" />
                    Tiến độ tổng thể
                  </CardTitle>
                </CardHeader>
                <CardContent>
                  <div className="space-y-4">
                    <div className="flex justify-between text-sm">
                      <span>Từ vựng đã học</span>
                      <span>
                        {userStats.wordsLearned}/{userStats.totalWords}
                      </span>
                    </div>
                    <Progress value={(userStats.wordsLearned / userStats.totalWords) * 100} className="h-3" />
                    <div className="flex justify-between text-sm">
                      <span>Kinh nghiệm</span>
                      <span>
                        {userStats.experience}/{userStats.nextLevelExp} XP
                      </span>
                    </div>
                    <Progress value={(userStats.experience / userStats.nextLevelExp) * 100} className="h-2" />
                  </div>
                </CardContent>
              </Card>
            </div>

            {/* Recent Achievements - Only shown for authenticated users */}
            <Card>
              <CardHeader>
                <CardTitle className="flex items-center gap-2">
                  <Trophy className="w-5 h-5" />
                  Thành tích gần đây
                </CardTitle>
              </CardHeader>
              <CardContent>
                <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                  {recentAchievements.map((achievement) => (
                    <div key={achievement.id} className="flex items-center gap-3 p-3 bg-gray-50 rounded-lg">
                      <div className="text-2xl">{achievement.icon}</div>
                      <div>
                        <h4 className="font-semibold text-sm">{achievement.title}</h4>
                        <p className="text-xs text-gray-600">{achievement.description}</p>
                      </div>
                    </div>
                  ))}
                </div>
              </CardContent>
            </Card>

            {/* Quick Actions - Only shown for authenticated users */}
            <div className="mt-8 grid grid-cols-1 md:grid-cols-3 gap-4">
              <Link href="/study">
                <Card className="cursor-pointer hover:shadow-lg transition-shadow">
                  <CardContent className="p-6 text-center">
                    <BookOpen className="w-8 h-8 text-purple-500 mx-auto mb-2" />
                    <h3 className="font-semibold">Học từ mới</h3>
                    <p className="text-sm text-gray-600">Bắt đầu session học tập</p>
                  </CardContent>
                </Card>
              </Link>

              <Link href="/progress">
                <Card className="cursor-pointer hover:shadow-lg transition-shadow">
                  <CardContent className="p-6 text-center">
                    <TrendingUp className="w-8 h-8 text-blue-500 mx-auto mb-2" />
                    <h3 className="font-semibold">Xem tiến độ</h3>
                    <p className="text-sm text-gray-600">Theo dõi sự tiến bộ</p>
                  </CardContent>
                </Card>
              </Link>

              <Link href="/achievements">
                <Card className="cursor-pointer hover:shadow-lg transition-shadow">
                  <CardContent className="p-6 text-center">
                    <Trophy className="w-8 h-8 text-yellow-500 mx-auto mb-2" />
                    <h3 className="font-semibold">Thành tích</h3>
                    <p className="text-sm text-gray-600">Xem huy hiệu đã đạt được</p>
                  </CardContent>
                </Card>
              </Link>
            </div>
          </>
        ) : (
          <>
            {/* Landing page content for guests */}
            <div className="grid grid-cols-1 lg:grid-cols-2 gap-8 mb-12">
              <div className="flex flex-col justify-center">
                <h2 className="text-2xl font-bold text-gray-800 mb-4">
                  Học từ vựng tiếng Anh hiệu quả
                </h2>
                <p className="text-gray-600 mb-6">
                  FlashLearn giúp bạn học từ vựng tiếng Anh nhanh chóng và hiệu quả thông qua:
                </p>
                <ul className="space-y-3">
                  <li className="flex items-start">
                    <div className="bg-purple-100 p-1 rounded-full mr-3 mt-0.5">
                      <BookOpen className="w-4 h-4 text-purple-600" />
                    </div>
                    <span>Thẻ ghi nhớ thông minh với hệ thống lặp lại theo khoảng thời gian</span>
                  </li>
                  <li className="flex items-start">
                    <div className="bg-green-100 p-1 rounded-full mr-3 mt-0.5">
                      <Target className="w-4 h-4 text-green-600" />
                    </div>
                    <span>Mục tiêu học tập cá nhân hóa và theo dõi tiến độ</span>
                  </li>
                  <li className="flex items-start">
                    <div className="bg-blue-100 p-1 rounded-full mr-3 mt-0.5">
                      <Trophy className="w-4 h-4 text-blue-600" />
                    </div>
                    <span>Hệ thống thành tích và phần thưởng để duy trì động lực</span>
                  </li>
                </ul>
                <div className="mt-8">
                  <Link href="/register">
                    <Button size="lg" className="bg-purple-600 hover:bg-purple-700">
                      Bắt đầu miễn phí ngay hôm nay
                    </Button>
                  </Link>
                </div>
              </div>
              
              <div className="bg-white p-6 rounded-xl shadow-lg">
                <div className="aspect-video bg-gradient-to-br from-purple-100 to-blue-100 rounded-lg flex items-center justify-center">
                  <div className="text-center">
                    <BookOpen className="w-16 h-16 text-purple-500 mx-auto mb-4" />
                    <p className="text-gray-600">Hình minh họa ứng dụng</p>
                  </div>
                </div>
              </div>
            </div>

            <div className="grid grid-cols-1 md:grid-cols-3 gap-6 mb-12">
              <Card>
                <CardContent className="p-6 text-center">
                  <div className="w-12 h-12 bg-purple-100 rounded-full flex items-center justify-center mx-auto mb-4">
                    <BookOpen className="w-6 h-6 text-purple-600" />
                  </div>
                  <h3 className="font-bold mb-2">Học hiệu quả</h3>
                  <p className="text-gray-600 text-sm">
                    Hệ thống lặp lại theo khoảng thời gian giúp bạn ghi nhớ từ vựng lâu dài
                  </p>
                </CardContent>
              </Card>

              <Card>
                <CardContent className="p-6 text-center">
                  <div className="w-12 h-12 bg-orange-100 rounded-full flex items-center justify-center mx-auto mb-4">
                    <Flame className="w-6 h-6 text-orange-600" />
                  </div>
                  <h3 className="font-bold mb-2">Duy trì động lực</h3>
                  <p className="text-gray-600 text-sm">
                    Chuỗi ngày học và hệ thống thành tích giúp bạn duy trì thói quen học tập
                  </p>
                </CardContent>
              </Card>

              <Card>
                <CardContent className="p-6 text-center">
                  <div className="w-12 h-12 bg-green-100 rounded-full flex items-center justify-center mx-auto mb-4">
                    <TrendingUp className="w-6 h-6 text-green-600" />
                  </div>
                  <h3 className="font-bold mb-2">Theo dõi tiến độ</h3>
                  <p className="text-gray-600 text-sm">
                    Biểu đồ và thống kê chi tiết giúp bạn theo dõi quá trình học tập
                  </p>
                </CardContent>
              </Card>
            </div>

            <div className="text-center mb-8">
              <h2 className="text-2xl font-bold text-gray-800 mb-4">
                Sẵn sàng bắt đầu hành trình học từ vựng?
              </h2>
              <Link href="/register">
                <Button size="lg" className="bg-purple-600 hover:bg-purple-700">
                  Đăng ký miễn phí
                </Button>
              </Link>
            </div>
          </>
        )}
      </main>
    </div>
  )
}
