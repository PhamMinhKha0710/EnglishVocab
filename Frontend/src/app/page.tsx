"use client"

import { useState, useEffect } from "react"
import { Link } from "react-router-dom"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Progress } from "@/components/ui/progress"
import { 
  BookOpen, Target, Flame, Trophy, TrendingUp, Play, 
  LogIn, UserPlus, CheckCircle, ArrowRight, Brain,
  Sparkles, BarChart, Clock, Award
} from "lucide-react"
import { useAuth } from "@/contexts/auth-context"
import { Badge } from "@/components/ui/badge"

export default function HomePage() {
  const [currentTime, setCurrentTime] = useState(new Date())
  const { isAuthenticated, user, isLoading } = useAuth()
  const [animatedFeature, setAnimatedFeature] = useState(0)

  useEffect(() => {
    const timer = setInterval(() => setCurrentTime(new Date()), 1000)
    
    // Animation timer for features
    const featureTimer = setInterval(() => {
      setAnimatedFeature(prev => (prev + 1) % 3)
    }, 3000)
    
    return () => {
      clearInterval(timer)
      clearInterval(featureTimer)
    }
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

  const features = [
    {
      title: "Học từ vựng thông minh",
      description: "Hệ thống lặp lại ngắt quãng (spaced repetition) giúp bạn ghi nhớ từ vựng lâu dài",
      icon: <Brain className="w-12 h-12 text-purple-600" />,
      color: "bg-purple-100"
    },
    {
      title: "Theo dõi tiến độ chi tiết",
      description: "Xem biểu đồ tiến độ và thống kê học tập để biết bạn đang học tốt đến đâu",
      icon: <BarChart className="w-12 h-12 text-blue-600" />,
      color: "bg-blue-100"
    },
    {
      title: "Học mọi lúc mọi nơi",
      description: "Ứng dụng hoạt động trên mọi thiết bị, ngay cả khi không có kết nối internet",
      icon: <Clock className="w-12 h-12 text-green-600" />,
      color: "bg-green-100"
    }
  ]

  const testimonials = [
    {
      name: "Nguyễn Văn A",
      role: "Sinh viên",
      content: "FlashLearn giúp tôi cải thiện vốn từ vựng tiếng Anh chỉ sau 2 tháng sử dụng. Giao diện dễ sử dụng và phương pháp học hiệu quả!",
      avatar: "/placeholder-user.jpg"
    },
    {
      name: "Trần Thị B",
      role: "Nhân viên văn phòng",
      content: "Tôi luôn muốn cải thiện tiếng Anh nhưng không có thời gian. FlashLearn giúp tôi học mọi lúc mọi nơi, chỉ với 10 phút mỗi ngày.",
      avatar: "/placeholder-user.jpg"
    },
    {
      name: "Lê Văn C",
      role: "Giáo viên",
      content: "Tôi thường giới thiệu FlashLearn cho học sinh của mình. Hệ thống lặp lại ngắt quãng giúp các em nhớ từ vựng lâu hơn.",
      avatar: "/placeholder-user.jpg"
    }
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
    <div className="min-h-screen bg-white">
      {isAuthenticated ? (
        // Giao diện cho người dùng đã đăng nhập
        <main className="p-6">
          {/* Hero Section - cải thiện thiết kế */}
          <div className="bg-white rounded-2xl p-0 mb-8 relative overflow-hidden">
            <div className="absolute inset-0 opacity-10 bg-pattern-dots"></div>
            <div className="relative rounded-2xl overflow-hidden">
              <div className="flex flex-col md:flex-row md:items-center border border-blue-100 rounded-2xl shadow-sm">
                <div className="bg-blue-50 p-8 md:w-2/3">
                  <h1 className="text-3xl font-bold text-gray-800">
                    Chào mừng trở lại, {user?.firstName || "Bạn"}! 👋
                  </h1>
                  <p className="mt-2 text-gray-600">
                    {currentTime.toLocaleDateString("vi-VN", {
                      weekday: "long",
                      year: "numeric",
                      month: "long",
                      day: "numeric",
                    })}
                  </p>
                  <div className="mt-5 bg-white rounded-lg p-3 border border-blue-100 shadow-sm max-w-xs">
                    <div className="flex items-center">
                      <div className="p-2 rounded-full bg-orange-100 mr-3">
                        <Flame className="w-4 h-4 text-orange-500" />
                      </div>
                      <div>
                        <span className="text-xs text-gray-500">Chuỗi ngày học</span>
                        <p className="font-bold text-orange-500">{userStats.currentStreak} ngày</p>
                      </div>
                    </div>
                  </div>
                </div>
                <div className="bg-white p-8 md:w-1/3 flex items-center justify-center">
                  <Link to="/study" className="w-full">
                    <Button size="lg" className="bg-blue-600 hover:bg-blue-700 shadow-md w-full gap-2 py-6">
                      <Play className="w-5 h-5" />
                      <div className="flex flex-col items-start">
                        <span className="text-sm font-normal">Bài học hôm nay</span>
                        <span className="font-bold">Bắt đầu học ngay</span>
                      </div>
                    </Button>
                  </Link>
                </div>
              </div>
            </div>
          </div>

          {/* Status Summary */}
          <div className="mb-8 p-4 bg-blue-50 rounded-xl border border-blue-100">
            <div className="flex items-center justify-between">
              <div className="flex items-center gap-2">
                <div className="p-2 bg-blue-100 rounded-full">
                  <TrendingUp className="w-5 h-5 text-blue-600" />
                </div>
                <div>
                  <p className="text-sm text-blue-700">Tổng quan học tập</p>
                  <p className="text-lg font-semibold">
                    Đã học <span className="text-blue-700">{userStats.wordsLearned}</span> từ, hoàn thành <span className="text-blue-700">{Math.round((userStats.wordsLearned / userStats.totalWords) * 100)}%</span>
                  </p>
                </div>
              </div>
              <Link to="/progress">
                <Button variant="outline" size="sm" className="border-blue-200 text-blue-700 hover:bg-blue-100">
                  Chi tiết
                  <ArrowRight className="w-3 h-3 ml-1" />
                </Button>
              </Link>
            </div>
          </div>

          {/* Stats Grid - cải thiện thiết kế */}
          <div className="grid grid-cols-2 md:grid-cols-4 gap-4 mb-8">
            <Card className="border shadow-sm hover:shadow-md transition-all">
              <CardContent className="p-0">
                <div className="p-5">
                  <div className="flex items-center justify-between">
                    <div className="p-2 bg-blue-100 rounded-full">
                      <BookOpen className="w-5 h-5 text-blue-600" />
                    </div>
                    <Badge variant="outline" className="font-medium">
                      {Math.round((userStats.wordsLearned / userStats.totalWords) * 100)}%
                    </Badge>
                  </div>
                  <p className="text-3xl font-bold text-gray-800 mt-3">{userStats.wordsLearned}</p>
                  <p className="text-sm text-gray-600">Từ đã học</p>
                </div>
                <div className="h-1 bg-blue-500 w-full"></div>
              </CardContent>
            </Card>

            <Card className="border shadow-sm hover:shadow-md transition-all">
              <CardContent className="p-0">
                <div className="p-5">
                  <div className="flex items-center justify-between">
                    <div className="p-2 bg-orange-100 rounded-full">
                      <Flame className="w-5 h-5 text-orange-500" />
                    </div>
                    <Badge variant="outline" className="font-medium">7 ngày đầu tiên</Badge>
                  </div>
                  <p className="text-3xl font-bold text-gray-800 mt-3">{userStats.currentStreak}</p>
                  <p className="text-sm text-gray-600">Chuỗi ngày học</p>
                </div>
                <div className="h-1 bg-orange-500 w-full"></div>
              </CardContent>
            </Card>

            <Card className="border shadow-sm hover:shadow-md transition-all">
              <CardContent className="p-0">
                <div className="p-5">
                  <div className="flex items-center justify-between">
                    <div className="p-2 bg-purple-100 rounded-full">
                      <Trophy className="w-5 h-5 text-purple-600" />
                    </div>
                    <Badge variant="outline" className="font-medium">
                      +{userStats.level*50} XP
                    </Badge>
                  </div>
                  <p className="text-3xl font-bold text-gray-800 mt-3">{userStats.level}</p>
                  <p className="text-sm text-gray-600">Cấp độ</p>
                </div>
                <div className="h-1 bg-purple-500 w-full"></div>
              </CardContent>
            </Card>

            <Card className="border shadow-sm hover:shadow-md transition-all">
              <CardContent className="p-0">
                <div className="p-5">
                  <div className="flex items-center justify-between">
                    <div className="p-2 bg-green-100 rounded-full">
                      <Target className="w-5 h-5 text-green-600" />
                    </div>
                    <Badge variant="outline" className="font-medium">
                      Hôm nay
                    </Badge>
                  </div>
                  <p className="text-3xl font-bold text-gray-800 mt-3">
                    {userStats.todayProgress}/{userStats.dailyGoal}
                  </p>
                  <p className="text-sm text-gray-600">Tiến độ hôm nay</p>
                </div>
                <div className="h-1 bg-green-500 w-full"></div>
              </CardContent>
            </Card>
          </div>

          {/* Progress Section - cải thiện thiết kế */}
          <div className="grid grid-cols-1 lg:grid-cols-2 gap-6 mb-8">
            <Card className="overflow-hidden border shadow-sm hover:shadow-md transition-all">
              <div className="bg-white border-b border-gray-100 py-4 px-6 flex justify-between items-center">
                <CardTitle className="flex items-center gap-2 text-gray-800">
                  <div className="p-1.5 bg-green-100 rounded-full">
                    <Target className="w-4 h-4 text-green-600" />
                  </div>
                  Mục tiêu hàng ngày
                </CardTitle>
                <Badge className="bg-green-100 text-green-700 hover:bg-green-200">{userStats.todayProgress}/{userStats.dailyGoal}</Badge>
              </div>
              
              <CardContent className="p-6">
                <div className="space-y-4">
                  <div className="w-full relative pt-2">
                    <Progress value={(userStats.todayProgress / userStats.dailyGoal) * 100} className="h-3 bg-gray-100">
                      <div className="h-full bg-green-500 rounded-full"></div>
                    </Progress>
                    <div className="absolute -top-1 transform -translate-x-1/2 flex flex-col items-center" style={{ left: `${(userStats.todayProgress / userStats.dailyGoal) * 100}%`, maxWidth: "95%" }}>
                      <div className="px-2 py-1 bg-green-500 text-white text-xs rounded-md whitespace-nowrap">
                        {Math.round((userStats.todayProgress / userStats.dailyGoal) * 100)}%
                      </div>
                      <div className="w-2 h-2 bg-green-500 rotate-45 -mt-0.5"></div>
                    </div>
                  </div>
                  
                  <div className="flex items-center gap-3 rounded-lg bg-green-50 p-4 border border-green-100">
                    <div className="p-2 bg-green-100 rounded-full">
                      <Sparkles className="w-4 h-4 text-green-600" />
                    </div>
                    <div>
                      <p className="font-medium text-gray-800">Mẹo:</p>
                      <p className="text-sm text-gray-600">Còn {userStats.dailyGoal - userStats.todayProgress} từ nữa để hoàn thành mục tiêu hôm nay!</p>
                    </div>
                  </div>
                </div>
              </CardContent>
            </Card>

            <Card className="overflow-hidden border shadow-sm hover:shadow-md transition-all">
              <div className="bg-white border-b border-gray-100 py-4 px-6 flex justify-between items-center">
                <CardTitle className="flex items-center gap-2 text-gray-800">
                  <div className="p-1.5 bg-blue-100 rounded-full">
                    <TrendingUp className="w-4 h-4 text-blue-600" />
                  </div>
                  Tiến độ tổng thể
                </CardTitle>
                <Badge className="bg-blue-100 text-blue-700 hover:bg-blue-200">Cấp {userStats.level}</Badge>
              </div>
              <CardContent className="p-6">
                <div className="space-y-4">
                  <div className="flex justify-between text-sm">
                    <span className="font-medium">Từ vựng đã học</span>
                    <span className="text-gray-500">
                      {userStats.wordsLearned}/{userStats.totalWords}
                    </span>
                  </div>
                  <div className="w-full relative pt-2">
                    <Progress value={(userStats.wordsLearned / userStats.totalWords) * 100} className="h-3 bg-gray-100">
                      <div className="h-full bg-blue-500 rounded-full"></div>
                    </Progress>
                    <div className="absolute -top-1 transform -translate-x-1/2 flex flex-col items-center" style={{ left: `${(userStats.wordsLearned / userStats.totalWords) * 100}%`, maxWidth: "95%" }}>
                      <div className="px-2 py-1 bg-blue-500 text-white text-xs rounded-md whitespace-nowrap">
                        {Math.round((userStats.wordsLearned / userStats.totalWords) * 100)}%
                      </div>
                      <div className="w-2 h-2 bg-blue-500 rotate-45 -mt-0.5"></div>
                    </div>
                  </div>

                  <div className="flex justify-between text-sm">
                    <span className="font-medium">Kinh nghiệm</span>
                    <span className="text-gray-500">
                      {userStats.experience}/{userStats.nextLevelExp} XP
                    </span>
                  </div>
                  <div className="w-full relative pt-2">
                    <Progress value={(userStats.experience / userStats.nextLevelExp) * 100} className="h-3 bg-gray-100">
                      <div className="h-full bg-purple-500 rounded-full"></div>
                    </Progress>
                    <div className="absolute -top-1 transform -translate-x-1/2 flex flex-col items-center" style={{ left: `${(userStats.experience / userStats.nextLevelExp) * 100}%`, maxWidth: "95%" }}>
                      <div className="px-2 py-1 bg-purple-500 text-white text-xs rounded-md whitespace-nowrap">
                        {Math.round((userStats.experience / userStats.nextLevelExp) * 100)}%
                      </div>
                      <div className="w-2 h-2 bg-purple-500 rotate-45 -mt-0.5"></div>
                    </div>
                  </div>
                </div>
              </CardContent>
            </Card>
          </div>

          {/* Quick Actions */}
          <div className="mb-8">
            <div className="flex justify-between items-center mb-4">
              <h2 className="text-2xl font-bold text-gray-800">Bắt đầu học</h2>
              <Link to="/study">
                <Button variant="ghost" size="sm" className="text-blue-600 hover:text-blue-700 hover:bg-blue-50">
                  Xem tất cả
                  <ArrowRight className="w-3 h-3 ml-1" />
                </Button>
              </Link>
            </div>
            <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
              <Link to="/study">
                <Card className="cursor-pointer hover:shadow-md transition-all hover:-translate-y-1 border shadow-sm h-full group">
                  <CardContent className="p-6 h-full flex flex-col">
                    <div className="bg-purple-50 p-3 rounded-full inline-flex items-center justify-center mb-4 group-hover:bg-purple-100 transition-colors">
                      <BookOpen className="w-8 h-8 text-purple-600" />
                    </div>
                    <h3 className="font-semibold text-lg mb-1 group-hover:text-purple-700 transition-colors">Học từ mới</h3>
                    <p className="text-sm text-gray-600 mb-4 flex-grow">Bắt đầu session học tập với thẻ ghi nhớ thông minh</p>
                    <div className="flex items-center text-purple-600 mt-auto text-sm font-medium">
                      <span className="mr-1">Bắt đầu</span>
                      <ArrowRight className="w-3 h-3 group-hover:translate-x-1 transition-transform" />
                    </div>
                  </CardContent>
                </Card>
              </Link>

              <Link to="/progress">
                <Card className="cursor-pointer hover:shadow-md transition-all hover:-translate-y-1 border shadow-sm h-full group">
                  <CardContent className="p-6 h-full flex flex-col">
                    <div className="bg-blue-50 p-3 rounded-full inline-flex items-center justify-center mb-4 group-hover:bg-blue-100 transition-colors">
                      <TrendingUp className="w-8 h-8 text-blue-600" />
                    </div>
                    <h3 className="font-semibold text-lg mb-1 group-hover:text-blue-700 transition-colors">Xem tiến độ</h3>
                    <p className="text-sm text-gray-600 mb-4 flex-grow">Theo dõi sự tiến bộ và phân tích kết quả học tập</p>
                    <div className="flex items-center text-blue-600 mt-auto text-sm font-medium">
                      <span className="mr-1">Xem</span>
                      <ArrowRight className="w-3 h-3 group-hover:translate-x-1 transition-transform" />
                    </div>
                  </CardContent>
                </Card>
              </Link>

              <Link to="/achievements">
                <Card className="cursor-pointer hover:shadow-md transition-all hover:-translate-y-1 border shadow-sm h-full group">
                  <CardContent className="p-6 h-full flex flex-col">
                    <div className="bg-yellow-50 p-3 rounded-full inline-flex items-center justify-center mb-4 group-hover:bg-yellow-100 transition-colors">
                      <Trophy className="w-8 h-8 text-yellow-600" />
                    </div>
                    <h3 className="font-semibold text-lg mb-1 group-hover:text-yellow-700 transition-colors">Thành tích</h3>
                    <p className="text-sm text-gray-600 mb-4 flex-grow">Khám phá các huy hiệu và thành tích bạn đã đạt được</p>
                    <div className="flex items-center text-yellow-600 mt-auto text-sm font-medium">
                      <span className="mr-1">Xem</span>
                      <ArrowRight className="w-3 h-3 group-hover:translate-x-1 transition-transform" />
                    </div>
                  </CardContent>
                </Card>
              </Link>
            </div>
          </div>

          {/* Recent Achievements */}
          <Card className="overflow-hidden border shadow-sm mb-8 hover:shadow-md transition-all">
            <div className="bg-white border-b border-gray-100 py-4 px-6 flex justify-between items-center">
              <CardTitle className="flex items-center gap-2 text-gray-800">
                <div className="p-1.5 bg-yellow-100 rounded-full">
                  <Award className="w-4 h-4 text-yellow-600" />
                </div>
                Thành tích gần đây
              </CardTitle>
              <Link to="/achievements">
                <Button variant="ghost" size="sm" className="text-yellow-600 hover:text-yellow-700 hover:bg-yellow-50">
                  Xem tất cả
                  <ArrowRight className="w-3 h-3 ml-1" />
                </Button>
              </Link>
            </div>
            <CardContent className="p-6">
              <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                {recentAchievements.map((achievement) => (
                  <div 
                    key={achievement.id} 
                    className="flex items-center gap-3 p-4 bg-white rounded-lg border border-gray-200 hover:border-yellow-200 hover:shadow-sm transition-all hover:-translate-y-0.5"
                  >
                    <div className="text-3xl bg-yellow-50 w-12 h-12 flex items-center justify-center rounded-full border border-yellow-100">{achievement.icon}</div>
                    <div>
                      <h4 className="font-semibold text-gray-800">{achievement.title}</h4>
                      <p className="text-xs text-gray-600">{achievement.description}</p>
                    </div>
                  </div>
                ))}
              </div>
            </CardContent>
          </Card>
        </main>
      ) : (
        // Giao diện cho người dùng chưa đăng nhập
        <main>
          {/* Hero Section - đơn giản hóa gradient */}
          <div className="relative bg-blue-50 text-gray-800 overflow-hidden border-b border-blue-100">
            <div className="container mx-auto px-6 py-16 md:py-24 relative z-10">
              <div className="grid grid-cols-1 md:grid-cols-2 gap-12 items-center">
                <div className="space-y-6">
                  <h1 className="text-4xl md:text-5xl font-bold leading-tight">
                    Học từ vựng tiếng Anh <span className="text-blue-600">hiệu quả</span> và <span className="text-blue-600">thú vị</span>
                  </h1>
                  <p className="text-lg text-gray-600 max-w-md">
                    FlashLearn giúp bạn ghi nhớ từ vựng lâu dài với hệ thống thẻ ghi nhớ thông minh và phương pháp lặp lại ngắt quãng.
                  </p>
                  <div className="flex flex-wrap gap-4">
                    <Link to="/auth/register">
                      <Button size="lg" className="bg-blue-600 hover:bg-blue-700 shadow-sm">
                        <UserPlus className="w-4 h-4 mr-2" />
                        Đăng ký miễn phí
                      </Button>
                    </Link>
                    <Link to="/auth/login">
                      <Button variant="outline" size="lg" className="border-blue-600 text-blue-600 hover:bg-blue-50">
                        <LogIn className="w-4 h-4 mr-2" />
                        Đăng nhập
                      </Button>
                    </Link>
                  </div>
                  <div className="flex items-center gap-2 text-sm">
                    <CheckCircle className="w-4 h-4 text-green-600" />
                    <span>Hơn 10,000+ người dùng đã tin tưởng</span>
                  </div>
                </div>
                <div className="hidden md:block">
                  <div className="relative">
                    <div className="bg-white p-6 rounded-2xl border border-blue-100 shadow-md transform rotate-3 hover:rotate-0 transition-transform duration-500">
                      <img 
                        src="/placeholder.jpg" 
                        alt="FlashLearn App" 
                        className="rounded-lg shadow-sm"
                      />
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>

          {/* Features Section - đơn giản hóa màu sắc */}
          <div className="py-16 bg-white">
            <div className="container mx-auto px-6">
              <div className="text-center mb-12">
                <h2 className="text-3xl font-bold text-gray-800">Tại sao chọn FlashLearn?</h2>
                <p className="text-gray-600 mt-2 max-w-2xl mx-auto">
                  Chúng tôi kết hợp công nghệ hiện đại với phương pháp học tập hiệu quả nhất
                </p>
              </div>
              
              <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
                {features.map((feature, index) => (
                  <div 
                    key={index} 
                    className={`p-6 rounded-xl border border-gray-100 shadow-sm transition-all duration-300 ${
                      animatedFeature === index ? 'transform -translate-y-2 shadow-md' : ''
                    }`}
                  >
                    <div className={`${feature.color} p-3 rounded-full inline-flex mb-4`}>
                      {feature.icon}
                    </div>
                    <h3 className="text-xl font-semibold mb-2">{feature.title}</h3>
                    <p className="text-gray-600">{feature.description}</p>
                  </div>
                ))}
              </div>
            </div>
          </div>

          {/* Statistics Section */}
          <div className="py-12 bg-gray-50">
            <div className="container mx-auto px-6">
              <div className="grid grid-cols-1 md:grid-cols-3 gap-6 text-center">
                <div className="p-6">
                  <div className="text-4xl font-bold text-blue-600 mb-2">10,000+</div>
                  <p className="text-gray-600">Người dùng</p>
                </div>
                <div className="p-6">
                  <div className="text-4xl font-bold text-blue-600 mb-2">5,000+</div>
                  <p className="text-gray-600">Từ vựng</p>
                </div>
                <div className="p-6">
                  <div className="text-4xl font-bold text-green-600 mb-2">98%</div>
                  <p className="text-gray-600">Đánh giá tích cực</p>
                </div>
              </div>
            </div>
          </div>

          {/* Testimonials Section */}
          <div className="py-16 bg-white">
            <div className="container mx-auto px-6">
              <div className="text-center mb-12">
                <h2 className="text-3xl font-bold text-gray-800">Người dùng nói gì về chúng tôi</h2>
                <p className="text-gray-600 mt-2">Trải nghiệm học tập thực tế từ cộng đồng FlashLearn</p>
              </div>
              
              <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
                {testimonials.map((testimonial, index) => (
                  <div key={index} className="bg-white p-6 rounded-xl shadow-sm border border-gray-100">
                    <div className="flex items-center mb-4">
                      <img 
                        src={testimonial.avatar} 
                        alt={testimonial.name} 
                        className="w-12 h-12 rounded-full mr-4 object-cover"
                      />
                      <div>
                        <h4 className="font-semibold">{testimonial.name}</h4>
                        <p className="text-gray-600 text-sm">{testimonial.role}</p>
                      </div>
                    </div>
                    <p className="text-gray-700 italic">"{testimonial.content}"</p>
                  </div>
                ))}
              </div>
            </div>
          </div>

          {/* CTA Section */}
          <div className="py-16 bg-blue-50 border-t border-blue-100">
            <div className="container mx-auto px-6 text-center">
              <h2 className="text-3xl font-bold mb-4 text-gray-800">Sẵn sàng bắt đầu hành trình học từ vựng?</h2>
              <p className="text-gray-600 max-w-2xl mx-auto mb-8">
                Đăng ký miễn phí ngay hôm nay và trải nghiệm cách học từ vựng hiệu quả nhất
              </p>
              <div className="flex flex-wrap justify-center gap-4">
                <Link to="/auth/register">
                  <Button size="lg" className="bg-blue-600 hover:bg-blue-700 shadow-sm">
                    <UserPlus className="w-4 h-4 mr-2" />
                    Đăng ký miễn phí
                  </Button>
                </Link>
                <Link to="/auth/login">
                  <Button variant="outline" size="lg" className="border-blue-600 text-blue-600 hover:bg-blue-50">
                    <LogIn className="w-4 h-4 mr-2" />
                    Đăng nhập
                  </Button>
                </Link>
              </div>
            </div>
          </div>
        </main>
      )}
    </div>
  )
}
