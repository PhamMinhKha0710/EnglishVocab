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
  Trophy
} from "lucide-react"

export default function ProgressPage() {
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
    { name: "Thông dụng", progress: 60 },
    { name: "Học thuật", progress: 40 },
    { name: "Kinh doanh", progress: 25 },
    { name: "Du lịch", progress: 75 },
  ]

  const recentActivity = [
    { date: "24/06/2025", action: "Hoàn thành 15 từ mới", icon: BookOpen },
    { date: "23/06/2025", action: "Đạt huy hiệu Người chăm chỉ", icon: Trophy },
    { date: "22/06/2025", action: "Kéo dài chuỗi ngày đến 7 ngày", icon: Flame },
    { date: "21/06/2025", action: "Hoàn thành 20 từ mới", icon: BookOpen },
    { date: "20/06/2025", action: "Đạt cấp độ 3", icon: Trophy },
  ]

  return (
    <div className="min-h-screen bg-gradient-to-br from-purple-50 to-blue-50">
      <main className="p-6">
        {/* Header */}
        <div className="mb-8">
          <div className="flex flex-col md:flex-row md:items-center md:justify-between gap-4">
            <div>
              <h1 className="text-3xl font-bold text-gray-800">Tiến độ học tập</h1>
              <p className="text-gray-600 mt-1">Theo dõi quá trình học và thành tích</p>
            </div>
            <Button className="bg-purple-600 hover:bg-purple-700">
              <Calendar className="w-4 h-4 mr-2" />
              Báo cáo chi tiết
            </Button>
          </div>
        </div>

        {/* Stats Overview */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
          <Card>
            <CardContent className="p-6">
              <div className="flex items-center justify-between">
                <div>
                  <p className="text-sm text-gray-600">Từ đã học</p>
                  <p className="text-2xl font-bold text-gray-800">{userStats.wordsLearned}/{userStats.totalWords}</p>
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
                  <p className="text-sm text-gray-600">Buổi học</p>
                  <p className="text-2xl font-bold text-blue-600">{userStats.totalSessions}</p>
                </div>
                <BarChart3 className="w-8 h-8 text-blue-500" />
              </div>
            </CardContent>
          </Card>

          <Card>
            <CardContent className="p-6">
              <div className="flex items-center justify-between">
                <div>
                  <p className="text-sm text-gray-600">Thời gian học</p>
                  <p className="text-2xl font-bold text-green-600">{userStats.timeSpent}</p>
                </div>
                <Clock className="w-8 h-8 text-green-500" />
              </div>
            </CardContent>
          </Card>
        </div>

        <Tabs defaultValue="overview" className="space-y-6">
          <TabsList className="grid grid-cols-3 w-full max-w-md">
            <TabsTrigger value="overview" className="flex items-center gap-2">
              <BarChart3 className="w-4 h-4" />
              <span>Tổng quan</span>
            </TabsTrigger>
            <TabsTrigger value="categories" className="flex items-center gap-2">
              <BarChart className="w-4 h-4" />
              <span>Danh mục</span>
            </TabsTrigger>
            <TabsTrigger value="history" className="flex items-center gap-2">
              <Calendar className="w-4 h-4" />
              <span>Lịch sử</span>
            </TabsTrigger>
          </TabsList>

          <TabsContent value="overview">
            <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
              <Card>
                <CardHeader>
                  <CardTitle className="flex items-center gap-2">
                    <TrendingUp className="w-5 h-5" />
                    Tiến độ tổng thể
                  </CardTitle>
                </CardHeader>
                <CardContent>
                  <div className="space-y-4">
                    <div className="space-y-2">
                      <div className="flex justify-between text-sm">
                        <span>Từ vựng đã học</span>
                        <span>{userStats.wordsLearned}/{userStats.totalWords}</span>
                      </div>
                      <Progress value={(userStats.wordsLearned / userStats.totalWords) * 100} className="h-3" />
                    </div>

                    <div className="space-y-2">
                      <div className="flex justify-between text-sm">
                        <span>Cấp độ {userStats.masteryLevel}</span>
                        <span>{userStats.experience}/{userStats.nextLevelExp} XP</span>
                      </div>
                      <Progress value={(userStats.experience / userStats.nextLevelExp) * 100} className="h-3" />
                      <p className="text-xs text-gray-500">
                        Cần thêm {userStats.nextLevelExp - userStats.experience} điểm kinh nghiệm để lên cấp
                      </p>
                    </div>
                  </div>
                </CardContent>
              </Card>

              <Card>
                <CardHeader>
                  <CardTitle className="flex items-center gap-2">
                    <BarChart className="w-5 h-5" />
                    Hoạt động theo ngày
                  </CardTitle>
                </CardHeader>
                <CardContent>
                  <div className="flex items-end justify-between h-40 pt-6 gap-2">
                    {dailyActivity.map((day, i) => (
                      <div key={i} className="flex flex-col items-center gap-1">
                        <div 
                          className={`w-10 rounded-t-md ${day.count > 0 ? 'bg-purple-500' : 'bg-gray-200'}`}
                          style={{ height: `${Math.max(15, (day.count / 20) * 100)}px` }}
                        ></div>
                        <span className="text-xs font-medium">{day.day}</span>
                        <span className="text-xs text-gray-500">{day.count}</span>
                      </div>
                    ))}
                  </div>
                </CardContent>
              </Card>
            </div>
          </TabsContent>

          <TabsContent value="categories">
            <Card>
              <CardHeader>
                <CardTitle>Tiến độ theo danh mục</CardTitle>
              </CardHeader>
              <CardContent>
                <div className="space-y-6">
                  {categoryProgress.map((category, i) => (
                    <div key={i} className="space-y-2">
                      <div className="flex justify-between text-sm">
                        <span>{category.name}</span>
                        <span>{category.progress}%</span>
                      </div>
                      <Progress value={category.progress} className="h-3" />
                    </div>
                  ))}
                </div>
              </CardContent>
            </Card>
          </TabsContent>

          <TabsContent value="history">
            <Card>
              <CardHeader>
                <CardTitle>Lịch sử hoạt động</CardTitle>
              </CardHeader>
              <CardContent>
                <div className="space-y-6">
                  {recentActivity.map((activity, i) => {
                    const Icon = activity.icon
                    return (
                      <div key={i} className="flex items-start gap-4">
                        <div className="bg-purple-100 p-2 rounded-full">
                          <Icon className="h-5 w-5 text-purple-600" />
                        </div>
                        <div>
                          <p className="font-medium">{activity.action}</p>
                          <p className="text-sm text-gray-500">{activity.date}</p>
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