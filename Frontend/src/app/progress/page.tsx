"use client"

import { useState } from "react"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Progress } from "@/components/ui/progress"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"
import {
  BarChart3,
  Calendar,
  Clock,
  BarChart,
  TrendingUp,
  BookOpen,
  Flame,
  Trophy,
  Award,
  Star,
  Zap,
  ArrowRight
} from "lucide-react"

export default function ProgressPage() {
  const [selectedPeriod, setSelectedPeriod] = useState<"week" | "month" | "year">("week")
  
  // Mock data for charts and progress statistics
  const userStats = {
    wordsLearned: 45,
    totalWords: 200,
    currentStreak: 7,
    totalSessions: 12,
    timeSpent: "8h 24m",
    masteryLevel: 3,
    experience: 480,
    nextLevelExp: 500,
    lastActive: "Hôm nay",
  }

  const dailyActivity = [
    { day: "T2", count: 15 },
    { day: "T3", count: 8 },
    { day: "T4", count: 12 },
    { day: "T5", count: 0 },
    { day: "T6", count: 20 },
    { day: "T7", count: 15 },
    { day: "CN", count: 5 },
  ]

  const categoryProgress = [
    { name: "Thông dụng", progress: 60, color: "bg-purple-500" },
    { name: "Học thuật", progress: 40, color: "bg-blue-500" },
    { name: "Kinh doanh", progress: 25, color: "bg-green-500" },
    { name: "Du lịch", progress: 75, color: "bg-orange-500" },
  ]

  const recentActivity = [
    { date: "24/06/2025", action: "Hoàn thành 15 từ mới", icon: BookOpen },
    { date: "23/06/2025", action: "Đạt huy hiệu Người chăm chỉ", icon: Trophy },
    { date: "22/06/2025", action: "Kéo dài chuỗi ngày đến 7 ngày", icon: Flame },
    { date: "21/06/2025", action: "Hoàn thành 20 từ mới", icon: BookOpen },
    { date: "20/06/2025", action: "Đạt cấp độ 3", icon: Trophy },
  ]

  return (
    <div className="min-h-screen bg-gradient-to-br from-purple-50 via-indigo-50 to-blue-50">
      <main className="container mx-auto px-4 py-8">
        {/* Header with animated gradient underline */}
        <div className="mb-8 relative">
          <div className="flex flex-col md:flex-row md:items-center md:justify-between gap-4">
            <div className="relative">
              <h1 className="text-4xl font-bold text-gray-800 mb-2">Tiến độ học tập</h1>
              <div className="h-1 w-32 bg-gradient-to-r from-purple-500 via-blue-500 to-teal-500 rounded animate-pulse"></div>
              <p className="text-gray-600 mt-3">Theo dõi quá trình học và thành tích của bạn</p>
            </div>
            <Button className="bg-gradient-to-r from-purple-600 to-blue-600 hover:from-purple-700 hover:to-blue-700 transition-all shadow-md">
              <Calendar className="w-4 h-4 mr-2" />
              Báo cáo chi tiết
            </Button>
          </div>
        </div>

        {/* Level Progress Banner */}
        <Card className="mb-8 border-0 bg-gradient-to-r from-purple-600 to-blue-600 text-white shadow-lg overflow-hidden">
          <CardContent className="p-6">
            <div className="flex flex-col md:flex-row items-center justify-between">
              <div className="mb-4 md:mb-0">
                <div className="flex items-center gap-3">
                  <div className="bg-white/20 p-3 rounded-full">
                    <Star className="w-8 h-8" />
                  </div>
                  <div>
                    <h3 className="text-xl font-bold">Cấp độ {userStats.masteryLevel}</h3>
                    <p className="text-white/80">Bạn đang tiến bộ rất tốt!</p>
                  </div>
                </div>
              </div>
              <div className="w-full md:w-1/2">
                <div className="flex justify-between text-sm mb-1">
                  <span>Kinh nghiệm</span>
                  <span>{userStats.experience}/{userStats.nextLevelExp} XP</span>
                </div>
                <div className="h-3 bg-white/20 rounded-full overflow-hidden">
                  <div 
                    className="h-full bg-gradient-to-r from-yellow-300 to-yellow-500 rounded-full transition-all duration-1000 ease-in-out"
                    style={{ width: `${(userStats.experience / userStats.nextLevelExp) * 100}%` }}
                  ></div>
                </div>
                <p className="text-xs mt-2 text-white/70">
                  Còn {userStats.nextLevelExp - userStats.experience} XP nữa để lên cấp {userStats.masteryLevel + 1}
                </p>
              </div>
            </div>
          </CardContent>
        </Card>

        {/* Stats Overview with hover effects */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
          <Card className="border border-purple-100 shadow-sm hover:shadow-md transition-all duration-300 hover:-translate-y-1">
            <CardContent className="p-6">
              <div className="flex items-center justify-between">
                <div>
                  <p className="text-sm text-gray-600">Từ đã học</p>
                  <div className="flex items-baseline">
                    <p className="text-3xl font-bold text-gray-800">{userStats.wordsLearned}</p>
                    <p className="text-gray-500 ml-1">/{userStats.totalWords}</p>
                  </div>
                </div>
                <div className="bg-purple-100 p-3 rounded-full">
                  <BookOpen className="w-6 h-6 text-purple-600" />
                </div>
              </div>
              <Progress 
                value={(userStats.wordsLearned / userStats.totalWords) * 100} 
                className="h-1.5 mt-4 bg-purple-100" 
                indicatorClassName="bg-purple-500"
              />
            </CardContent>
          </Card>

          <Card className="border border-orange-100 shadow-sm hover:shadow-md transition-all duration-300 hover:-translate-y-1">
            <CardContent className="p-6">
              <div className="flex items-center justify-between">
                <div>
                  <p className="text-sm text-gray-600">Chuỗi ngày</p>
                  <p className="text-3xl font-bold text-orange-600">{userStats.currentStreak}</p>
                </div>
                <div className="bg-orange-100 p-3 rounded-full">
                  <Flame className="w-6 h-6 text-orange-500" />
                </div>
              </div>
              <div className="flex items-center mt-4">
                <div className="flex space-x-1">
                  {[...Array(7)].map((_, i) => (
                    <div 
                      key={i} 
                      className={`h-1.5 w-5 rounded-full ${i < userStats.currentStreak % 7 ? 'bg-orange-500' : 'bg-orange-100'}`}
                    ></div>
                  ))}
                </div>
                <p className="text-xs text-gray-500 ml-2">Tuần này</p>
              </div>
            </CardContent>
          </Card>

          <Card className="border border-blue-100 shadow-sm hover:shadow-md transition-all duration-300 hover:-translate-y-1">
            <CardContent className="p-6">
              <div className="flex items-center justify-between">
                <div>
                  <p className="text-sm text-gray-600">Buổi học</p>
                  <p className="text-3xl font-bold text-blue-600">{userStats.totalSessions}</p>
                </div>
                <div className="bg-blue-100 p-3 rounded-full">
                  <BarChart3 className="w-6 h-6 text-blue-500" />
                </div>
              </div>
              <div className="flex items-center justify-between mt-4">
                <div className="flex space-x-1">
                  {dailyActivity.slice(0, 5).map((day, i) => (
                    <div 
                      key={i} 
                      className="h-6 w-2 bg-blue-100 rounded-full relative"
                    >
                      <div 
                        className="absolute bottom-0 w-full bg-blue-500 rounded-full transition-all duration-500"
                        style={{ height: `${Math.max(15, (day.count / 20) * 100)}%` }}
                      ></div>
                    </div>
                  ))}
                </div>
                <ArrowRight className="w-4 h-4 text-blue-500" />
              </div>
            </CardContent>
          </Card>

          <Card className="border border-green-100 shadow-sm hover:shadow-md transition-all duration-300 hover:-translate-y-1">
            <CardContent className="p-6">
              <div className="flex items-center justify-between">
                <div>
                  <p className="text-sm text-gray-600">Thời gian học</p>
                  <p className="text-3xl font-bold text-green-600">{userStats.timeSpent}</p>
                </div>
                <div className="bg-green-100 p-3 rounded-full">
                  <Clock className="w-6 h-6 text-green-500" />
                </div>
              </div>
              <div className="flex items-center gap-1 mt-4">
                <Zap className="w-4 h-4 text-yellow-500" />
                <p className="text-xs text-gray-600">Hiệu quả hơn 65% người dùng</p>
              </div>
            </CardContent>
          </Card>
        </div>

        <Tabs defaultValue="overview" className="space-y-6">
          <TabsList className="grid grid-cols-3 w-full max-w-md bg-white/80 backdrop-blur-sm">
            <TabsTrigger value="overview" className="flex items-center gap-2 data-[state=active]:bg-purple-100 data-[state=active]:text-purple-700">
              <BarChart3 className="w-4 h-4" />
              <span>Tổng quan</span>
            </TabsTrigger>
            <TabsTrigger value="categories" className="flex items-center gap-2 data-[state=active]:bg-blue-100 data-[state=active]:text-blue-700">
              <BarChart className="w-4 h-4" />
              <span>Danh mục</span>
            </TabsTrigger>
            <TabsTrigger value="history" className="flex items-center gap-2 data-[state=active]:bg-green-100 data-[state=active]:text-green-700">
              <Calendar className="w-4 h-4" />
              <span>Lịch sử</span>
            </TabsTrigger>
          </TabsList>

          <TabsContent value="overview" className="animate-fadeIn">
            <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
              <Card className="border-2 border-purple-100 shadow-md">
                <CardHeader className="bg-purple-50/50">
                  <CardTitle className="flex items-center gap-2 text-purple-900">
                    <TrendingUp className="w-5 h-5 text-purple-600" />
                    Tiến độ tổng thể
                  </CardTitle>
                </CardHeader>
                <CardContent className="p-6">
                  <div className="space-y-6">
                    <div className="space-y-2">
                      <div className="flex justify-between text-sm">
                        <span className="font-medium">Từ vựng đã học</span>
                        <span className="font-semibold text-purple-700">{userStats.wordsLearned}/{userStats.totalWords}</span>
                      </div>
                      <div className="relative">
                        <Progress 
                          value={(userStats.wordsLearned / userStats.totalWords) * 100} 
                          className="h-3 bg-purple-100" 
                          indicatorClassName="bg-gradient-to-r from-purple-500 to-blue-500"
                        />
                        <div className="absolute -bottom-5 left-0 text-xs text-gray-500">
                          0%
                        </div>
                        <div className="absolute -bottom-5 right-0 text-xs text-gray-500">
                          100%
                        </div>
                      </div>
                    </div>

                    <div className="mt-8 pt-6 border-t border-gray-100">
                      <div className="grid grid-cols-3 gap-4">
                        <div className="bg-purple-50 p-4 rounded-lg text-center">
                          <p className="text-xs text-gray-600">Tỷ lệ chính xác</p>
                          <p className="text-xl font-bold text-purple-700">85%</p>
                        </div>
                        <div className="bg-blue-50 p-4 rounded-lg text-center">
                          <p className="text-xs text-gray-600">Từ mới/ngày</p>
                          <p className="text-xl font-bold text-blue-700">6.4</p>
                        </div>
                        <div className="bg-green-50 p-4 rounded-lg text-center">
                          <p className="text-xs text-gray-600">Thời gian/từ</p>
                          <p className="text-xl font-bold text-green-700">1.2<span className="text-sm">p</span></p>
                        </div>
                      </div>
                    </div>
                  </div>
                </CardContent>
              </Card>

              <Card className="border-2 border-blue-100 shadow-md">
                <CardHeader className="bg-blue-50/50">
                  <CardTitle className="flex items-center gap-2 text-blue-900">
                    <BarChart className="w-5 h-5 text-blue-600" />
                    Hoạt động theo ngày
                  </CardTitle>
                </CardHeader>
                <CardContent className="p-6">
                  <div className="flex items-end justify-between h-48 pt-6 gap-2">
                    {dailyActivity.map((day, i) => (
                      <div key={i} className="flex flex-col items-center gap-1 group">
                        <div className="relative">
                          <div 
                            className={`w-12 rounded-md ${day.count > 0 ? 'bg-gradient-to-t from-blue-600 to-purple-500' : 'bg-gray-200'} transition-all duration-300 group-hover:opacity-80`}
                            style={{ height: `${Math.max(20, (day.count / 20) * 100)}px` }}
                          ></div>
                          <div className={`absolute -top-8 left-1/2 transform -translate-x-1/2 bg-gray-800 text-white px-2 py-1 rounded text-xs opacity-0 group-hover:opacity-100 transition-opacity duration-300 pointer-events-none`}>
                            {day.count} từ
                          </div>
                        </div>
                        <span className="text-sm font-medium">{day.day}</span>
                        <span className="text-xs text-gray-500">{day.count}</span>
                      </div>
                    ))}
                  </div>
                  
                  <div className="mt-6 pt-6 border-t border-gray-100">
                    <div className="flex items-center justify-between">
                      <div className="flex items-center gap-2">
                        <Award className="w-5 h-5 text-yellow-500" />
                        <span className="text-sm font-medium">Ngày tốt nhất: Thứ 6 (20 từ)</span>
                      </div>
                      <Button variant="outline" size="sm" className="text-xs">
                        Xem chi tiết
                      </Button>
                    </div>
                  </div>
                </CardContent>
              </Card>
            </div>
          </TabsContent>

          <TabsContent value="categories" className="animate-fadeIn">
            <Card className="border-2 border-blue-100 shadow-md">
              <CardHeader className="bg-blue-50/50">
                <CardTitle className="text-blue-900">Tiến độ theo danh mục</CardTitle>
              </CardHeader>
              <CardContent className="p-6">
                <div className="space-y-8">
                  {categoryProgress.map((category, i) => (
                    <div key={i} className="space-y-3">
                      <div className="flex justify-between text-sm">
                        <span className="font-medium flex items-center gap-2">
                          <div className={`w-3 h-3 rounded-full ${category.color}`}></div>
                          {category.name}
                        </span>
                        <span className="font-semibold">{category.progress}%</span>
                      </div>
                      <div className="relative">
                        <Progress 
                          value={category.progress} 
                          className="h-3 bg-gray-100" 
                          indicatorClassName={category.color}
                        />
                        <div className="absolute right-0 top-0 transform translate-x-1/2 -translate-y-1/2">
                          <div className={`w-6 h-6 rounded-full flex items-center justify-center text-xs text-white ${category.color}`}>
                            {category.progress}
                          </div>
                        </div>
                      </div>
                      <div className="flex justify-between text-xs text-gray-500">
                        <span>{Math.round(category.progress * 0.2)} từ</span>
                        <span>{Math.round(category.progress * 0.2)}/{Math.round(20)} từ</span>
                      </div>
                    </div>
                  ))}
                </div>
              </CardContent>
            </Card>
          </TabsContent>

          <TabsContent value="history" className="animate-fadeIn">
            <Card className="border-2 border-green-100 shadow-md">
              <CardHeader className="bg-green-50/50">
                <CardTitle className="text-green-900">Lịch sử hoạt động</CardTitle>
              </CardHeader>
              <CardContent className="p-6">
                <div className="space-y-6">
                  {recentActivity.map((activity, i) => {
                    const Icon = activity.icon
                    return (
                      <div key={i} className="flex items-start gap-4 p-3 rounded-lg hover:bg-gray-50 transition-colors">
                        <div className="bg-gradient-to-br from-purple-100 to-blue-100 p-3 rounded-full">
                          <Icon className="h-5 w-5 text-purple-600" />
                        </div>
                        <div className="flex-1">
                          <div className="flex justify-between items-center">
                            <p className="font-medium">{activity.action}</p>
                            <p className="text-sm text-gray-500">{activity.date}</p>
                          </div>
                          <div className="h-1 w-16 bg-gradient-to-r from-purple-500 to-blue-500 rounded mt-2"></div>
                        </div>
                      </div>
                    )
                  })}
                </div>
              </CardContent>
            </Card>
          </TabsContent>
        </Tabs>
      </main>
    </div>
  )
} 