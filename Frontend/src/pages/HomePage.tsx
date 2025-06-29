"use client"

import { useEffect, useState } from "react"
import { Link } from "react-router-dom"
import { Layout } from "@/components/Layout/Layout"
import { SearchBar } from "@/components/Search/SearchBar"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Progress } from "@/components/ui/progress"
import { useAppStore } from "@/store/useAppStore"
import { useOfflineSync } from "@/hooks/useOfflineSync"
import { formatDate, getGreeting, calculateLevel } from "@/lib/utils"
import { BookOpen, Target, Flame, Trophy, TrendingUp, Play, Calendar, Award, Wifi, WifiOff, LogIn, UserPlus } from "lucide-react"
import { useAuth } from "@/contexts/auth-context"

export function HomePage() {
  const [currentTime, setCurrentTime] = useState(new Date())
  const [showSearch, setShowSearch] = useState(false)
  const { userProgress, studySessions, vocabulary, getWordsForReview } = useAppStore()
  const { syncStatus } = useOfflineSync()
  const { isAuthenticated } = useAuth()

  useEffect(() => {
    const timer = setInterval(() => setCurrentTime(new Date()), 1000)
    return () => clearInterval(timer)
  }, [])

  const currentLevel = calculateLevel(userProgress.experience)
  const nextLevelExp = currentLevel * 100
  const currentLevelProgress = ((userProgress.experience % 100) / 100) * 100

  const recentAchievements = [
    { id: 1, title: "Tuần đầu tiên", description: "Hoàn thành 7 ngày liên tiếp", icon: "🔥" },
    { id: 2, title: "Người mới bắt đầu", description: "Học 25 từ vựng đầu tiên", icon: "🌟" },
    { id: 3, title: "Chăm chỉ", description: "Học 50 từ trong tuần", icon: "💪" },
  ]

  const todayProgress = (userProgress.wordsToday / userProgress.dailyGoal) * 100
  const wordsForReview = getWordsForReview()

  const handleWordSelect = (word: any) => {
    // Navigate to study page with selected word
    // This could be implemented with URL params
    console.log("Selected word:", word)
  }

  // Hero banner cho khách (không đăng nhập)
  const GuestHero = () => (
    <div className="bg-gradient-to-r from-purple-600 to-blue-500 rounded-xl p-8 mb-8 text-white">
      <div className="max-w-3xl mx-auto">
        <h1 className="text-3xl md:text-4xl font-bold mb-4">Học từ vựng tiếng Anh hiệu quả</h1>
        <p className="text-lg mb-6 opacity-90">
          Ứng dụng học từ vựng thông minh với phương pháp Spaced Repetition giúp bạn ghi nhớ từ vựng lâu hơn
          và hiệu quả hơn.
        </p>
        <div className="flex flex-col sm:flex-row gap-4">
          <Link to="/auth/register">
            <Button size="lg" className="bg-white text-purple-700 hover:bg-gray-100 w-full sm:w-auto">
              <UserPlus className="w-4 h-4 mr-2" />
              Đăng ký miễn phí
            </Button>
          </Link>
          <Link to="/auth/login">
            <Button size="lg" variant="outline" className="border-white text-white hover:bg-white/10 w-full sm:w-auto">
              <LogIn className="w-4 h-4 mr-2" />
              Đăng nhập
            </Button>
          </Link>
        </div>
      </div>
    </div>
  )

  return (
    <Layout>
      {/* Offline Status */}
      {!syncStatus.isOnline && (
        <div className="mb-4 p-3 bg-orange-50 border border-orange-200 rounded-lg flex items-center gap-2">
          <WifiOff className="w-4 h-4 text-orange-600" />
          <span className="text-sm text-orange-800">Đang offline - Dữ liệu sẽ được đồng bộ khi có kết nối</span>
        </div>
      )}

      {/* Hero banner cho khách */}
      {!isAuthenticated && <GuestHero />}

      {/* Header */}
      <div className="mb-8">
        <div className="flex flex-col md:flex-row md:items-center md:justify-between gap-4">
          <div className="animate-fade-in">
            <h1 className="text-3xl font-bold text-gray-800 flex items-center gap-2">
              {getGreeting()}! 👋
              {syncStatus.isOnline ? (
                <Wifi className="w-5 h-5 text-green-500" />
              ) : (
                <WifiOff className="w-5 h-5 text-orange-500" />
              )}
            </h1>
            <p className="text-gray-600 mt-1 flex items-center gap-2">
              <Calendar className="w-4 h-4" />
              {formatDate(currentTime)}
            </p>
          </div>
          <div className="flex gap-2">
            {isAuthenticated ? (
              <>
                <Button variant="outline" onClick={() => setShowSearch(!showSearch)} className="flex items-center gap-2">
                  <BookOpen className="w-4 h-4" />
                  Tìm từ
                </Button>
                <Link to="/study">
                  <Button size="lg" className="bg-purple-600 hover:bg-purple-700 animate-fade-in">
                    <Play className="w-4 h-4 mr-2" />
                    Bắt đầu học
                  </Button>
                </Link>
              </>
            ) : (
              <Link to="/auth/login">
                <Button size="lg" className="bg-purple-600 hover:bg-purple-700 animate-fade-in">
                  <LogIn className="w-4 h-4 mr-2" />
                  Đăng nhập để học
                </Button>
              </Link>
            )}
          </div>
        </div>

        {/* Search Bar */}
        {showSearch && isAuthenticated && (
          <div className="mt-4 animate-fade-in">
            <SearchBar vocabulary={vocabulary} onWordSelect={handleWordSelect} placeholder="Tìm kiếm từ vựng..." />
          </div>
        )}
      </div>

      {/* Chỉ hiển thị khi đã đăng nhập */}
      {isAuthenticated && (
        <>
          {/* Spaced Repetition Alert */}
          {wordsForReview.length > 0 && (
            <Card className="mb-6 border-blue-200 bg-blue-50 animate-fade-in">
              <CardContent className="p-4">
                <div className="flex items-center justify-between">
                  <div>
                    <h3 className="font-semibold text-blue-800">Có {wordsForReview.length} từ cần ôn tập!</h3>
                    <p className="text-sm text-blue-600">Spaced Repetition đề xuất ôn tập ngay bây giờ</p>
                  </div>
                  <Link to="/study">
                    <Button size="sm" className="bg-blue-600 hover:bg-blue-700">
                      Ôn tập ngay
                    </Button>
                  </Link>
                </div>
              </CardContent>
            </Card>
          )}

          {/* Stats Grid */}
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
            <Card className="animate-fade-in">
              <CardContent className="p-6">
                <div className="flex items-center justify-between">
                  <div>
                    <p className="text-sm text-gray-600">Từ đã học</p>
                    <p className="text-2xl font-bold text-gray-800">{userProgress.learnedWords}</p>
                  </div>
                  <BookOpen className="w-8 h-8 text-purple-500" />
                </div>
              </CardContent>
            </Card>

            <Card className="animate-fade-in" style={{ animationDelay: "0.1s" }}>
              <CardContent className="p-6">
                <div className="flex items-center justify-between">
                  <div>
                    <p className="text-sm text-gray-600">Chuỗi ngày</p>
                    <p className="text-2xl font-bold text-orange-600">{userProgress.currentStreak}</p>
                  </div>
                  <Flame className="w-8 h-8 text-orange-500" />
                </div>
              </CardContent>
            </Card>

            <Card className="animate-fade-in" style={{ animationDelay: "0.2s" }}>
              <CardContent className="p-6">
                <div className="flex items-center justify-between">
                  <div>
                    <p className="text-sm text-gray-600">Cấp độ</p>
                    <p className="text-2xl font-bold text-blue-600">{currentLevel}</p>
                  </div>
                  <Trophy className="w-8 h-8 text-yellow-500" />
                </div>
              </CardContent>
            </Card>

            <Card className="animate-fade-in" style={{ animationDelay: "0.3s" }}>
              <CardContent className="p-6">
                <div className="flex items-center justify-between">
                  <div>
                    <p className="text-sm text-gray-600">Điểm số</p>
                    <p className="text-2xl font-bold text-green-600">{userProgress.totalPoints}</p>
                  </div>
                  <Award className="w-8 h-8 text-green-500" />
                </div>
              </CardContent>
            </Card>
          </div>

          {/* Progress Section */}
          <div className="grid grid-cols-1 lg:grid-cols-2 gap-6 mb-8">
            <Card className="animate-fade-in" style={{ animationDelay: "0.4s" }}>
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
                      {userProgress.wordsToday}/{userProgress.dailyGoal} từ
                    </span>
                  </div>
                  <Progress value={todayProgress} className="h-3" />
                  <p className="text-sm text-gray-600">
                    {userProgress.wordsToday >= userProgress.dailyGoal
                      ? "🎉 Bạn đã hoàn thành mục tiêu hôm nay!"
                      : `Còn ${userProgress.dailyGoal - userProgress.wordsToday} từ nữa để hoàn thành mục tiêu!`}
                  </p>
                </div>
              </CardContent>
            </Card>

            <Card className="animate-fade-in" style={{ animationDelay: "0.5s" }}>
              <CardHeader>
                <CardTitle className="flex items-center gap-2">
                  <TrendingUp className="w-5 h-5" />
                  Tiến độ cấp độ
                </CardTitle>
              </CardHeader>
              <CardContent>
                <div className="space-y-4">
                  <div className="flex justify-between text-sm">
                    <span>Cấp độ {currentLevel}</span>
                    <span>
                      {userProgress.experience % 100}/{100} XP
                    </span>
                  </div>
                  <Progress value={currentLevelProgress} className="h-3" />
                  <p className="text-sm text-gray-600">
                    Còn {100 - (userProgress.experience % 100)} XP để lên cấp {currentLevel + 1}
                  </p>
                </div>
              </CardContent>
            </Card>
          </div>

          {/* Recent Achievements */}
          <Card className="mb-8 animate-fade-in" style={{ animationDelay: "0.6s" }}>
            <CardHeader>
              <CardTitle className="flex items-center gap-2">
                <Trophy className="w-5 h-5" />
                Thành tích gần đây
              </CardTitle>
            </CardHeader>
            <CardContent>
              <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                {recentAchievements.map((achievement, index) => (
                  <div
                    key={achievement.id}
                    className="flex items-center gap-3 p-3 bg-gray-50 rounded-lg hover:bg-gray-100 transition-colors animate-fade-in"
                    style={{ animationDelay: `${0.7 + index * 0.1}s` }}
                  >
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
        </>
      )}

      {/* Hiển thị cho tất cả người dùng - giới thiệu tính năng */}
      {!isAuthenticated && (
        <div className="grid grid-cols-1 md:grid-cols-3 gap-6 mb-8">
          <Card className="animate-fade-in">
            <CardContent className="p-6 text-center">
              <div className="w-16 h-16 bg-purple-100 rounded-full flex items-center justify-center mx-auto mb-4">
                <BookOpen className="w-8 h-8 text-purple-600" />
              </div>
              <h3 className="text-lg font-semibold mb-2">Học từ vựng thông minh</h3>
              <p className="text-gray-600">Phương pháp học dựa trên Spaced Repetition giúp bạn ghi nhớ từ vựng lâu hơn</p>
            </CardContent>
          </Card>

          <Card className="animate-fade-in" style={{ animationDelay: "0.1s" }}>
            <CardContent className="p-6 text-center">
              <div className="w-16 h-16 bg-blue-100 rounded-full flex items-center justify-center mx-auto mb-4">
                <Target className="w-8 h-8 text-blue-600" />
              </div>
              <h3 className="text-lg font-semibold mb-2">Theo dõi tiến độ</h3>
              <p className="text-gray-600">Đặt mục tiêu, theo dõi tiến độ và nhận thành tích khi đạt được mục tiêu</p>
            </CardContent>
          </Card>

          <Card className="animate-fade-in" style={{ animationDelay: "0.2s" }}>
            <CardContent className="p-6 text-center">
              <div className="w-16 h-16 bg-green-100 rounded-full flex items-center justify-center mx-auto mb-4">
                <Flame className="w-8 h-8 text-green-600" />
              </div>
              <h3 className="text-lg font-semibold mb-2">Duy trì động lực</h3>
              <p className="text-gray-600">Hệ thống chuỗi ngày và thành tích giúp bạn duy trì thói quen học tập</p>
            </CardContent>
          </Card>
        </div>
      )}

      {/* Quick Actions */}
      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        {isAuthenticated ? (
          <>
            <Link to="/study">
              <Card
                className="cursor-pointer hover:shadow-lg transition-all duration-300 animate-fade-in"
                style={{ animationDelay: "0.8s" }}
              >
                <CardContent className="p-6 text-center">
                  <BookOpen className="w-8 h-8 text-purple-500 mx-auto mb-2" />
                  <h3 className="font-semibold mb-1">Học từ vựng</h3>
                  <p className="text-sm text-gray-600">Học từ mới và ôn tập từ cũ</p>
                </CardContent>
              </Card>
            </Link>
            <Link to="/progress">
              <Card
                className="cursor-pointer hover:shadow-lg transition-all duration-300 animate-fade-in"
                style={{ animationDelay: "0.9s" }}
              >
                <CardContent className="p-6 text-center">
                  <TrendingUp className="w-8 h-8 text-blue-500 mx-auto mb-2" />
                  <h3 className="font-semibold mb-1">Xem tiến độ</h3>
                  <p className="text-sm text-gray-600">Theo dõi tiến độ học tập của bạn</p>
                </CardContent>
              </Card>
            </Link>
            <Link to="/achievements">
              <Card
                className="cursor-pointer hover:shadow-lg transition-all duration-300 animate-fade-in"
                style={{ animationDelay: "1s" }}
              >
                <CardContent className="p-6 text-center">
                  <Trophy className="w-8 h-8 text-yellow-500 mx-auto mb-2" />
                  <h3 className="font-semibold mb-1">Thành tích</h3>
                  <p className="text-sm text-gray-600">Xem tất cả thành tích đã đạt được</p>
                </CardContent>
              </Card>
            </Link>
          </>
        ) : (
          <>
            <Card className="animate-fade-in" style={{ animationDelay: "0.8s" }}>
              <CardContent className="p-6 text-center">
                <BookOpen className="w-8 h-8 text-purple-500 mx-auto mb-2" />
                <h3 className="font-semibold mb-1">5000+ từ vựng</h3>
                <p className="text-sm text-gray-600">Kho từ vựng phong phú từ cơ bản đến nâng cao</p>
              </CardContent>
            </Card>
            <Card className="animate-fade-in" style={{ animationDelay: "0.9s" }}>
              <CardContent className="p-6 text-center">
                <Play className="w-8 h-8 text-blue-500 mx-auto mb-2" />
                <h3 className="font-semibold mb-1">Đa dạng hình thức</h3>
                <p className="text-sm text-gray-600">Flashcard, trắc nghiệm, nhập từ và nhiều hơn nữa</p>
              </CardContent>
            </Card>
            <Card className="animate-fade-in" style={{ animationDelay: "1s" }}>
              <CardContent className="p-6 text-center">
                <Wifi className="w-8 h-8 text-green-500 mx-auto mb-2" />
                <h3 className="font-semibold mb-1">Hỗ trợ offline</h3>
                <p className="text-sm text-gray-600">Học mọi lúc mọi nơi, ngay cả khi không có mạng</p>
              </CardContent>
            </Card>
          </>
        )}
      </div>
    </Layout>
  )
}
