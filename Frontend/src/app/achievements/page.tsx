"use client"

import { useState } from "react"
import { Link } from "react-router-dom"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"
import { Badge } from "@/components/ui/badge"
import { Trophy, Medal, Target, Lock, Star } from "lucide-react"

export default function AchievementsPage() {
  // Mock data for achievements
  const earnedAchievements = [
    {
      id: 1,
      title: "Tuần đầu tiên",
      description: "Hoàn thành 7 ngày liên tiếp",
      icon: "🔥",
      date: "20/06/2025",
      color: "bg-orange-100 text-orange-700",
      xp: 50
    },
    {
      id: 2,
      title: "Người mới bắt đầu",
      description: "Học 25 từ vựng đầu tiên",
      icon: "🌟",
      date: "18/06/2025",
      color: "bg-purple-100 text-purple-700",
      xp: 25
    },
    {
      id: 3,
      title: "Chăm chỉ",
      description: "Học 50 từ trong tuần",
      icon: "💪",
      date: "23/06/2025",
      color: "bg-blue-100 text-blue-700",
      xp: 75
    },
    {
      id: 4,
      title: "Nhà thông thái",
      description: "Trả lời đúng 10 câu hỏi liên tiếp",
      icon: "🧠",
      date: "19/06/2025",
      color: "bg-green-100 text-green-700",
      xp: 30
    },
  ]

  const lockedAchievements = [
    {
      id: 5,
      title: "Bậc thầy từ vựng",
      description: "Học 100 từ mới",
      icon: "📚",
      progress: 45,
      total: 100,
      color: "bg-gray-100 text-gray-500",
      xp: 100
    },
    {
      id: 6,
      title: "Không gì cản nổi",
      description: "Duy trì chuỗi ngày học 30 ngày",
      icon: "🏆",
      progress: 7,
      total: 30,
      color: "bg-gray-100 text-gray-500",
      xp: 200
    },
    {
      id: 7,
      title: "Nhà du lịch",
      description: "Học hết danh mục từ vựng du lịch",
      icon: "✈️",
      progress: 12,
      total: 20,
      color: "bg-gray-100 text-gray-500",
      xp: 50
    },
  ]

  const challengesData = [
    {
      id: 1,
      title: "Thử thách mùa hè",
      description: "Hoàn thành 20 buổi học trong tháng 7",
      progress: 8,
      total: 20,
      deadline: "31/07/2025",
      reward: "200 XP + Huy hiệu đặc biệt",
      icon: "☀️"
    },
    {
      id: 2,
      title: "Tuần học tập",
      description: "Học ít nhất 5 phút mỗi ngày trong 7 ngày",
      progress: 5,
      total: 7,
      deadline: "28/06/2025",
      reward: "50 XP + Streak Protection",
      icon: "📅"
    },
  ]

  const stats = {
    totalAchievements: earnedAchievements.length,
    totalAvailable: earnedAchievements.length + lockedAchievements.length,
    earnedXP: earnedAchievements.reduce((sum, item) => sum + item.xp, 0),
    remainingXP: lockedAchievements.reduce((sum, item) => sum + item.xp, 0),
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-purple-50 to-blue-50">
      <main className="p-6">
        {/* Header */}
        <div className="mb-8">
          <div className="flex flex-col md:flex-row md:items-center md:justify-between gap-4">
            <div>
              <h1 className="text-3xl font-bold text-gray-800">Thành tích</h1>
              <p className="text-gray-600 mt-1">Huy hiệu và kỷ niệm chương của bạn</p>
            </div>
          </div>
        </div>

        {/* Stats Overview */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
          <Card>
            <CardContent className="p-6">
              <div className="flex items-center justify-between">
                <div>
                  <p className="text-sm text-gray-600">Thành tích đạt được</p>
                  <p className="text-2xl font-bold text-gray-800">{stats.totalAchievements}/{stats.totalAvailable}</p>
                </div>
                <Trophy className="w-8 h-8 text-yellow-500" />
              </div>
            </CardContent>
          </Card>

          <Card>
            <CardContent className="p-6">
              <div className="flex items-center justify-between">
                <div>
                  <p className="text-sm text-gray-600">XP từ thành tích</p>
                  <p className="text-2xl font-bold text-purple-600">{stats.earnedXP}</p>
                </div>
                <Star className="w-8 h-8 text-purple-500" />
              </div>
            </CardContent>
          </Card>

          <Card>
            <CardContent className="p-6">
              <div className="flex items-center justify-between">
                <div>
                  <p className="text-sm text-gray-600">XP có thể nhận</p>
                  <p className="text-2xl font-bold text-blue-600">{stats.remainingXP}</p>
                </div>
                <Medal className="w-8 h-8 text-blue-500" />
              </div>
            </CardContent>
          </Card>

          <Card>
            <CardContent className="p-6">
              <div className="flex items-center justify-between">
                <div>
                  <p className="text-sm text-gray-600">Thử thách hiện tại</p>
                  <p className="text-2xl font-bold text-green-600">{challengesData.length}</p>
                </div>
                <Target className="w-8 h-8 text-green-500" />
              </div>
            </CardContent>
          </Card>
        </div>

        <Tabs defaultValue="earned" className="space-y-6">
          <TabsList className="grid grid-cols-2 w-full max-w-md">
            <TabsTrigger value="earned" className="flex items-center gap-2">
              <Trophy className="w-4 h-4" />
              <span>Đã đạt</span>
            </TabsTrigger>
            <TabsTrigger value="challenges" className="flex items-center gap-2">
              <Target className="w-4 h-4" />
              <span>Thử thách</span>
            </TabsTrigger>
          </TabsList>

          <TabsContent value="earned">
            <div className="space-y-6">
              <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                {earnedAchievements.map((achievement) => (
                  <Card key={achievement.id} className="overflow-hidden">
                    <div className={`h-2 ${achievement.color.split(" ")[0]} w-full`}></div>
                    <CardContent className="p-6">
                      <div className="flex items-center gap-4">
                        <div className="text-4xl">{achievement.icon}</div>
                        <div>
                          <p className="font-semibold">{achievement.title}</p>
                          <p className="text-sm text-gray-600">{achievement.description}</p>
                          <div className="flex items-center mt-2 gap-2">
                            <Badge className="text-xs">{achievement.date}</Badge>
                            <Badge className="bg-purple-100 text-purple-700 hover:bg-purple-200 text-xs">+{achievement.xp} XP</Badge>
                          </div>
                        </div>
                      </div>
                    </CardContent>
                  </Card>
                ))}

                {lockedAchievements.map((achievement) => (
                  <Card key={achievement.id} className="overflow-hidden opacity-70">
                    <div className="h-2 bg-gray-200 w-full"></div>
                    <CardContent className="p-6">
                      <div className="flex items-center gap-4">
                        <div className="text-4xl flex items-center justify-center rounded-full w-12 h-12 bg-gray-200 text-gray-400">
                          <Lock className="w-5 h-5" />
                        </div>
                        <div className="flex-1">
                          <div className="flex items-center justify-between">
                            <p className="font-semibold">{achievement.title}</p>
                            <Badge className="bg-gray-100 text-gray-500 hover:bg-gray-200 text-xs">+{achievement.xp} XP</Badge>
                          </div>
                          <p className="text-sm text-gray-600">{achievement.description}</p>
                          <div className="mt-2 h-2 w-full bg-gray-200 rounded-full">
                            <div 
                              className="h-2 bg-gray-400 rounded-full" 
                              style={{ width: `${Math.min(100, (achievement.progress / achievement.total) * 100)}%` }}
                            ></div>
                          </div>
                          <p className="text-xs text-gray-500 mt-1">{achievement.progress}/{achievement.total}</p>
                        </div>
                      </div>
                    </CardContent>
                  </Card>
                ))}
              </div>
            </div>
          </TabsContent>

          <TabsContent value="challenges">
            <div className="space-y-6">
              {challengesData.map((challenge) => (
                <Card key={challenge.id} className="overflow-hidden">
                  <CardContent className="p-6">
                    <div className="flex items-center gap-4">
                      <div className="text-4xl">{challenge.icon}</div>
                      <div className="flex-1">
                        <div className="flex flex-col md:flex-row md:items-center md:justify-between gap-2">
                          <div>
                            <p className="font-semibold">{challenge.title}</p>
                            <p className="text-sm text-gray-600">{challenge.description}</p>
                          </div>
                          <Badge className="self-start md:self-auto bg-green-100 text-green-700 hover:bg-green-200">
                            Hạn: {challenge.deadline}
                          </Badge>
                        </div>
                        <div className="mt-4">
                          <div className="flex justify-between text-sm mb-1">
                            <span>Tiến độ</span>
                            <span>{challenge.progress}/{challenge.total}</span>
                          </div>
                          <div className="h-2 w-full bg-gray-200 rounded-full">
                            <div 
                              className="h-2 bg-blue-500 rounded-full" 
                              style={{ width: `${Math.min(100, (challenge.progress / challenge.total) * 100)}%` }}
                            ></div>
                          </div>
                          <div className="flex justify-between mt-4">
                            <p className="text-sm text-gray-600">
                              <span className="font-medium">Phần thưởng:</span> {challenge.reward}
                            </p>
                            <Button variant="outline" size="sm">
                              Chi tiết
                            </Button>
                          </div>
                        </div>
                      </div>
                    </div>
                  </CardContent>
                </Card>
              ))}
              
              <div className="text-center mt-8">
                <p className="text-gray-600 mb-4">Hoàn thành thử thách để nhận thêm phần thưởng và huy hiệu đặc biệt</p>
                <Button className="bg-purple-600 hover:bg-purple-700">
                  Xem tất cả thử thách
                </Button>
              </div>
            </div>
          </TabsContent>
        </Tabs>
      </main>
    </div>
  )
} 