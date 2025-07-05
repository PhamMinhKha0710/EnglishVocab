"use client"

import { useState } from "react"
import { Link } from "react-router-dom"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardHeader, CardTitle, CardFooter } from "@/components/ui/card"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"
import { Badge } from "@/components/ui/badge"
import { Progress } from "@/components/ui/progress"
import { Trophy, Medal, Target, Lock, Star, Gift, Calendar, Flame, Award, Sparkles, Zap, Crown } from "lucide-react"

export default function AchievementsPage() {
  const [selectedCategory, setSelectedCategory] = useState<string>("all")

  // Mock data for achievements
  const earnedAchievements = [
    {
      id: 1,
      title: "Tuần đầu tiên",
      description: "Hoàn thành 7 ngày liên tiếp",
      icon: "🔥",
      date: "20/06/2025",
      color: "bg-orange-100 text-orange-700 border-orange-300",
      gradient: "from-orange-500 to-red-500",
      xp: 50
    },
    {
      id: 2,
      title: "Người mới bắt đầu",
      description: "Học 25 từ vựng đầu tiên",
      icon: "🌟",
      date: "18/06/2025",
      color: "bg-purple-100 text-purple-700 border-purple-300",
      gradient: "from-purple-500 to-indigo-500",
      xp: 25
    },
    {
      id: 3,
      title: "Chăm chỉ",
      description: "Học 50 từ trong tuần",
      icon: "💪",
      date: "23/06/2025",
      color: "bg-blue-100 text-blue-700 border-blue-300",
      gradient: "from-blue-500 to-cyan-500",
      xp: 75
    },
    {
      id: 4,
      title: "Nhà thông thái",
      description: "Trả lời đúng 10 câu hỏi liên tiếp",
      icon: "🧠",
      date: "19/06/2025",
      color: "bg-green-100 text-green-700 border-green-300",
      gradient: "from-green-500 to-emerald-500",
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
      icon: "☀️",
      color: "bg-yellow-100 border-yellow-300",
      gradient: "from-yellow-400 to-orange-500"
    },
    {
      id: 2,
      title: "Tuần học tập",
      description: "Học ít nhất 5 phút mỗi ngày trong 7 ngày",
      progress: 5,
      total: 7,
      deadline: "28/06/2025",
      reward: "50 XP + Streak Protection",
      icon: "📅",
      color: "bg-blue-100 border-blue-300",
      gradient: "from-blue-400 to-indigo-500"
    },
  ]

  const stats = {
    totalAchievements: earnedAchievements.length,
    totalAvailable: earnedAchievements.length + lockedAchievements.length,
    earnedXP: earnedAchievements.reduce((sum, item) => sum + item.xp, 0),
    remainingXP: lockedAchievements.reduce((sum, item) => sum + item.xp, 0),
  }

  const categories = [
    { id: "all", name: "Tất cả", icon: Trophy },
    { id: "streak", name: "Chuỗi ngày", icon: Flame },
    { id: "learning", name: "Học tập", icon: Award },
    { id: "special", name: "Đặc biệt", icon: Star },
  ]

  return (
    <div className="min-h-screen bg-white">
      <main className="container mx-auto px-4 py-8">
        {/* Header with simple underline */}
        <div className="mb-8">
          <div className="flex flex-col md:flex-row md:items-center md:justify-between gap-4">
            <div>
              <h1 className="text-4xl font-bold text-gray-800 mb-2">Thành tích</h1>
              <div className="h-1 w-32 bg-yellow-500 rounded"></div>
              <p className="text-gray-600 mt-3">Huy hiệu và kỷ niệm chương của bạn</p>
            </div>
            <div className="flex gap-2">
              <Button className="bg-yellow-500 hover:bg-yellow-600 shadow-sm">
                <Gift className="w-4 h-4 mr-2" />
                Đổi thưởng
              </Button>
            </div>
          </div>
        </div>

        {/* Achievement Progress Banner - đơn giản hóa màu sắc */}
        <Card className="mb-8 border border-yellow-200 bg-yellow-50 shadow-sm overflow-hidden">
          <CardContent className="p-6">
            <div className="flex flex-col md:flex-row items-center justify-between">
              <div className="mb-4 md:mb-0">
                <div className="flex items-center gap-3">
                  <div className="bg-yellow-100 p-3 rounded-full border border-yellow-200">
                    <Crown className="w-8 h-8 text-yellow-600" />
                  </div>
                  <div>
                    <h3 className="text-xl font-bold text-gray-800">Nhà vô địch từ vựng</h3>
                    <p className="text-gray-600">Bạn đã đạt được {stats.totalAchievements} thành tích!</p>
                  </div>
                </div>
              </div>
              <div className="w-full md:w-1/2">
                <div className="flex justify-between text-sm mb-1">
                  <span>Tiến độ thành tích</span>
                  <span>{stats.totalAchievements}/{stats.totalAvailable}</span>
                </div>
                <div className="h-3 bg-white rounded-full overflow-hidden border border-yellow-200">
                  <div 
                    className="h-full bg-yellow-500 rounded-full transition-all duration-1000 ease-in-out"
                    style={{ width: `${(stats.totalAchievements / stats.totalAvailable) * 100}%` }}
                  ></div>
                </div>
                <p className="text-xs mt-2 text-gray-600">
                  Còn {stats.totalAvailable - stats.totalAchievements} thành tích nữa để hoàn thành tất cả
                </p>
              </div>
            </div>
          </CardContent>
        </Card>

        {/* Stats Overview - đơn giản hóa màu sắc và hiệu ứng */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
          <Card className="border border-gray-200 shadow-sm hover:shadow-md transition-all duration-300 hover:-translate-y-1">
            <CardContent className="p-6">
              <div className="flex items-center justify-between">
                <div>
                  <p className="text-sm text-gray-600">Thành tích đạt được</p>
                  <div className="flex items-baseline">
                    <p className="text-3xl font-bold text-gray-800">{stats.totalAchievements}</p>
                    <p className="text-gray-500 ml-1">/{stats.totalAvailable}</p>
                  </div>
                </div>
                <div className="bg-yellow-50 p-3 rounded-full border border-yellow-100">
                  <Trophy className="w-6 h-6 text-yellow-600" />
                </div>
              </div>
              <Progress 
                value={(stats.totalAchievements / stats.totalAvailable) * 100} 
                className="h-1.5 mt-4 bg-gray-100" 
                indicatorClassName="bg-yellow-500"
              />
            </CardContent>
          </Card>

          <Card className="border border-gray-200 shadow-sm hover:shadow-md transition-all duration-300 hover:-translate-y-1">
            <CardContent className="p-6">
              <div className="flex items-center justify-between">
                <div>
                  <p className="text-sm text-gray-600">XP từ thành tích</p>
                  <p className="text-3xl font-bold text-gray-800">{stats.earnedXP}</p>
                </div>
                <div className="bg-purple-50 p-3 rounded-full border border-purple-100">
                  <Star className="w-6 h-6 text-purple-600" />
                </div>
              </div>
              <div className="flex items-center gap-1 mt-4">
                <Sparkles className="w-4 h-4 text-purple-600" />
                <p className="text-xs text-gray-600">Đóng góp 25% tổng XP của bạn</p>
              </div>
            </CardContent>
          </Card>

          <Card className="border border-gray-200 shadow-sm hover:shadow-md transition-all duration-300 hover:-translate-y-1">
            <CardContent className="p-6">
              <div className="flex items-center justify-between">
                <div>
                  <p className="text-sm text-gray-600">XP có thể nhận</p>
                  <p className="text-3xl font-bold text-gray-800">{stats.remainingXP}</p>
                </div>
                <div className="bg-blue-50 p-3 rounded-full border border-blue-100">
                  <Medal className="w-6 h-6 text-blue-600" />
                </div>
              </div>
              <div className="flex items-center gap-1 mt-4">
                <Zap className="w-4 h-4 text-blue-600" />
                <p className="text-xs text-gray-600">Từ {lockedAchievements.length} thành tích còn lại</p>
              </div>
            </CardContent>
          </Card>

          <Card className="border border-gray-200 shadow-sm hover:shadow-md transition-all duration-300 hover:-translate-y-1">
            <CardContent className="p-6">
              <div className="flex items-center justify-between">
                <div>
                  <p className="text-sm text-gray-600">Thử thách hiện tại</p>
                  <p className="text-3xl font-bold text-gray-800">{challengesData.length}</p>
                </div>
                <div className="bg-green-50 p-3 rounded-full border border-green-100">
                  <Target className="w-6 h-6 text-green-600" />
                </div>
              </div>
              <div className="flex items-center gap-1 mt-4">
                <Calendar className="w-4 h-4 text-green-600" />
                <p className="text-xs text-gray-600">Kết thúc trong 3 ngày</p>
              </div>
            </CardContent>
          </Card>
        </div>

        {/* Category Filter - đơn giản hóa màu sắc */}
        <div className="mb-6">
          <div className="flex flex-wrap gap-2">
            {categories.map((category) => {
              const Icon = category.icon;
              return (
                <Button
                  key={category.id}
                  variant={selectedCategory === category.id ? "default" : "outline"}
                  size="sm"
                  onClick={() => setSelectedCategory(category.id)}
                  className={`flex items-center gap-2 ${
                    selectedCategory === category.id 
                      ? "bg-yellow-500 hover:bg-yellow-600 text-white" 
                      : "hover:bg-yellow-50"
                  }`}
                >
                  <Icon className="w-4 h-4" />
                  {category.name}
                </Button>
              );
            })}
          </div>
        </div>

        <Tabs defaultValue="earned" className="space-y-6">
          <TabsList className="grid grid-cols-2 w-full max-w-md bg-white border border-gray-200">
            <TabsTrigger 
              value="earned" 
              className="flex items-center gap-2 data-[state=active]:bg-yellow-50 data-[state=active]:text-yellow-700"
            >
              <Trophy className="w-4 h-4" />
              <span>Đã đạt</span>
            </TabsTrigger>
            <TabsTrigger 
              value="challenges" 
              className="flex items-center gap-2 data-[state=active]:bg-green-50 data-[state=active]:text-green-700"
            >
              <Target className="w-4 h-4" />
              <span>Thử thách</span>
            </TabsTrigger>
          </TabsList>

          <TabsContent value="earned" className="space-y-6">
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
              {earnedAchievements.map((achievement) => (
                <Card 
                  key={achievement.id} 
                  className="overflow-hidden border-2 border-gray-100 hover:border-yellow-200 transition-all duration-300 hover:shadow-md hover:-translate-y-1 group"
                >
                  <div className={`h-2 bg-yellow-500 w-full`}></div>
                  <CardContent className="p-6">
                    <div className="flex items-center gap-4">
                      <div className="text-5xl relative">
                        {achievement.icon}
                        <div className="absolute -bottom-1 -right-1 bg-yellow-500 rounded-full w-5 h-5 flex items-center justify-center text-white text-xs border-2 border-white">
                          <Sparkles className="w-3 h-3" />
                        </div>
                      </div>
                      <div className="flex-1">
                        <div className="flex justify-between items-start">
                          <p className="font-semibold text-lg">{achievement.title}</p>
                          <Badge className="bg-yellow-500 text-white hover:bg-yellow-600 text-xs">
                            +{achievement.xp} XP
                          </Badge>
                        </div>
                        <p className="text-sm text-gray-600 mt-1">{achievement.description}</p>
                        <div className="flex items-center mt-3 gap-2">
                          <Badge variant="outline" className="text-xs flex items-center gap-1">
                            <Calendar className="w-3 h-3" />
                            {achievement.date}
                          </Badge>
                          <div className="h-1 w-1 rounded-full bg-gray-300"></div>
                          <span className="text-xs text-gray-500">Thành tích hiếm</span>
                        </div>
                      </div>
                    </div>
                  </CardContent>
                  <CardFooter className="bg-gray-50 py-2 px-6 hidden group-hover:flex justify-between items-center">
                    <span className="text-xs text-gray-500">Đạt được cách đây 3 ngày</span>
                    <Button variant="ghost" size="sm" className="text-xs h-7 px-2">
                      Chia sẻ
                    </Button>
                  </CardFooter>
                </Card>
              ))}

              {lockedAchievements.map((achievement) => (
                <Card 
                  key={achievement.id} 
                  className="overflow-hidden border-2 border-gray-100 opacity-80 hover:opacity-100 transition-all duration-300 group"
                >
                  <div className="h-2 bg-gray-200 w-full"></div>
                  <CardContent className="p-6">
                    <div className="flex items-center gap-4">
                      <div className="text-5xl flex items-center justify-center rounded-full w-14 h-14 bg-gray-100 text-gray-400 relative">
                        <Lock className="w-6 h-6" />
                        <div className="absolute -bottom-1 -right-1 bg-gray-400 rounded-full w-5 h-5 flex items-center justify-center text-white text-xs border-2 border-white">
                          <Star className="w-3 h-3" />
                        </div>
                      </div>
                      <div className="flex-1">
                        <div className="flex justify-between items-start">
                          <p className="font-semibold text-lg">{achievement.title}</p>
                          <Badge className="bg-gray-200 text-gray-600 hover:bg-gray-300 text-xs">
                            +{achievement.xp} XP
                          </Badge>
                        </div>
                        <p className="text-sm text-gray-600 mt-1">{achievement.description}</p>
                        <div className="mt-3">
                          <div className="flex justify-between text-xs text-gray-500 mb-1">
                            <span>Tiến độ: {achievement.progress}/{achievement.total}</span>
                            <span>{Math.round((achievement.progress / achievement.total) * 100)}%</span>
                          </div>
                          <div className="h-2 w-full bg-gray-200 rounded-full overflow-hidden">
                            <div 
                              className="h-2 bg-gray-400 rounded-full group-hover:bg-yellow-500 transition-all duration-500" 
                              style={{ width: `${Math.min(100, (achievement.progress / achievement.total) * 100)}%` }}
                            ></div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </CardContent>
                </Card>
              ))}
            </div>
          </TabsContent>

          <TabsContent value="challenges" className="space-y-6">
            <div className="space-y-6">
              {challengesData.map((challenge) => (
                <Card 
                  key={challenge.id} 
                  className="overflow-hidden border-2 border-gray-100 hover:border-green-200 transition-all duration-300 hover:shadow-md"
                >
                  <div className={`h-2 bg-green-500 w-full`}></div>
                  <CardContent className="p-6">
                    <div className="flex flex-col md:flex-row md:items-center gap-6">
                      <div className="text-5xl">{challenge.icon}</div>
                      <div className="flex-1">
                        <div className="flex flex-col md:flex-row md:items-center justify-between gap-2 mb-3">
                          <div>
                            <h3 className="text-xl font-bold">{challenge.title}</h3>
                            <p className="text-gray-600">{challenge.description}</p>
                          </div>
                          <Badge className="bg-green-50 text-green-700 border border-green-200 text-sm px-3 py-1 whitespace-nowrap">
                            <Calendar className="w-3 h-3 mr-1" />
                            Hạn: {challenge.deadline}
                          </Badge>
                        </div>
                        
                        <div className="mb-3">
                          <div className="flex justify-between text-sm mb-1">
                            <span className="font-medium">Tiến độ</span>
                            <span>{challenge.progress}/{challenge.total}</span>
                          </div>
                          <div className="h-3 w-full bg-gray-100 rounded-full overflow-hidden">
                            <div 
                              className="h-3 bg-green-500 rounded-full transition-all duration-500"
                              style={{ width: `${Math.min(100, (challenge.progress / challenge.total) * 100)}%` }}
                            ></div>
                          </div>
                        </div>
                        
                        <div className="flex flex-col md:flex-row md:items-center justify-between gap-2 mt-4">
                          <div className="flex items-center gap-2">
                            <Gift className="w-5 h-5 text-purple-600" />
                            <span className="font-medium text-gray-800">{challenge.reward}</span>
                          </div>
                          <Button size="sm" className="bg-green-600 hover:bg-green-700">
                            Tiếp tục thử thách
                          </Button>
                        </div>
                      </div>
                    </div>
                  </CardContent>
                </Card>
              ))}
              
              <Card className="border-dashed border-2 border-gray-200 bg-gray-50/50 hover:bg-white transition-all">
                <CardContent className="p-6 flex flex-col items-center justify-center text-center">
                  <div className="bg-gray-100 p-4 rounded-full mb-4">
                    <Target className="w-8 h-8 text-gray-400" />
                  </div>
                  <h3 className="text-lg font-medium text-gray-700 mb-2">Khám phá thêm thử thách</h3>
                  <p className="text-gray-500 mb-4 max-w-md">Tham gia các thử thách mới để nhận thêm phần thưởng và thành tích đặc biệt</p>
                  <Button variant="outline">
                    Xem tất cả thử thách
                  </Button>
                </CardContent>
              </Card>
            </div>
          </TabsContent>
        </Tabs>
      </main>
    </div>
  )
} 