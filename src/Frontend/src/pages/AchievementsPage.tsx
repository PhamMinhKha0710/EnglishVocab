"use client"

import { useState } from "react"
import { Layout } from "@/components/Layout/Layout"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Progress } from "@/components/ui/progress"
import { Badge } from "@/components/ui/badge"
import { Button } from "@/components/ui/button"
import { useAppStore } from "@/store/useAppStore"
import { Trophy, Star, Flame, Target, Award, CheckCircle, Calendar } from "lucide-react"

interface Achievement {
  id: string
  title: string
  description: string
  icon: string
  category: "streak" | "learning" | "achievement" | "special"
  requirement: number
  currentProgress: number
  unlocked: boolean
  unlockedAt?: Date
  reward: number
}

export function AchievementsPage() {
  const { userProgress, studySessions, vocabulary } = useAppStore()
  const [selectedCategory, setSelectedCategory] = useState<string>("all")

  // Calculate achievements
  const achievements: Achievement[] = [
    // Streak achievements
    {
      id: "first-day",
      title: "Ngày đầu tiên",
      description: "Hoàn thành ngày học đầu tiên",
      icon: "🌟",
      category: "streak",
      requirement: 1,
      currentProgress: userProgress.currentStreak,
      unlocked: userProgress.currentStreak >= 1,
      reward: 50,
    },
    {
      id: "week-warrior",
      title: "Chiến binh tuần",
      description: "Học liên tiếp 7 ngày",
      icon: "🔥",
      category: "streak",
      requirement: 7,
      currentProgress: userProgress.currentStreak,
      unlocked: userProgress.currentStreak >= 7,
      reward: 100,
    },
    {
      id: "month-master",
      title: "Bậc thầy tháng",
      description: "Học liên tiếp 30 ngày",
      icon: "👑",
      category: "streak",
      requirement: 30,
      currentProgress: userProgress.currentStreak,
      unlocked: userProgress.currentStreak >= 30,
      reward: 500,
    },
    {
      id: "year-legend",
      title: "Huyền thoại năm",
      description: "Học liên tiếp 365 ngày",
      icon: "🏆",
      category: "streak",
      requirement: 365,
      currentProgress: userProgress.currentStreak,
      unlocked: userProgress.currentStreak >= 365,
      reward: 2000,
    },

    // Learning achievements
    {
      id: "first-words",
      title: "Những từ đầu tiên",
      description: "Học 10 từ vựng đầu tiên",
      icon: "📚",
      category: "learning",
      requirement: 10,
      currentProgress: userProgress.learnedWords,
      unlocked: userProgress.learnedWords >= 10,
      reward: 50,
    },
    {
      id: "vocabulary-builder",
      title: "Người xây dựng từ vựng",
      description: "Học 50 từ vựng",
      icon: "🏗️",
      category: "learning",
      requirement: 50,
      currentProgress: userProgress.learnedWords,
      unlocked: userProgress.learnedWords >= 50,
      reward: 200,
    },
    {
      id: "word-collector",
      title: "Nhà sưu tập từ",
      description: "Học 100 từ vựng",
      icon: "💎",
      category: "learning",
      requirement: 100,
      currentProgress: userProgress.learnedWords,
      unlocked: userProgress.learnedWords >= 100,
      reward: 500,
    },
    {
      id: "vocabulary-master",
      title: "Bậc thầy từ vựng",
      description: "Học 500 từ vựng",
      icon: "🎓",
      category: "learning",
      requirement: 500,
      currentProgress: userProgress.learnedWords,
      unlocked: userProgress.learnedWords >= 500,
      reward: 1000,
    },

    // Achievement based achievements
    {
      id: "perfectionist",
      title: "Người hoàn hảo",
      description: "Đạt 100% trong một session",
      icon: "🎯",
      category: "achievement",
      requirement: 1,
      currentProgress: studySessions.filter((s) => s.correctAnswers === s.wordsStudied && s.wordsStudied > 0).length,
      unlocked: studySessions.some((s) => s.correctAnswers === s.wordsStudied && s.wordsStudied > 0),
      reward: 100,
    },
    {
      id: "speed-learner",
      title: "Học nhanh",
      description: "Hoàn thành session dưới 3 phút",
      icon: "⚡",
      category: "achievement",
      requirement: 1,
      currentProgress: studySessions.filter((s) => s.duration < 180).length,
      unlocked: studySessions.some((s) => s.duration < 180),
      reward: 150,
    },
    {
      id: "marathon-learner",
      title: "Người học marathon",
      description: "Học liên tục 30 phút",
      icon: "🏃",
      category: "achievement",
      requirement: 1,
      currentProgress: studySessions.filter((s) => s.duration >= 1800).length,
      unlocked: studySessions.some((s) => s.duration >= 1800),
      reward: 300,
    },

    // Special achievements
    {
      id: "early-bird",
      title: "Chim sớm",
      description: "Học trước 7h sáng",
      icon: "🌅",
      category: "special",
      requirement: 1,
      currentProgress: studySessions.filter((s) => new Date(s.date).getHours() < 7).length,
      unlocked: studySessions.some((s) => new Date(s.date).getHours() < 7),
      reward: 100,
    },
    {
      id: "night-owl",
      title: "Cú đêm",
      description: "Học sau 11h đêm",
      icon: "🦉",
      category: "special",
      requirement: 1,
      currentProgress: studySessions.filter((s) => new Date(s.date).getHours() >= 23).length,
      unlocked: studySessions.some((s) => new Date(s.date).getHours() >= 23),
      reward: 100,
    },
    {
      id: "weekend-warrior",
      title: "Chiến binh cuối tuần",
      description: "Học vào cuối tuần",
      icon: "🎮",
      category: "special",
      requirement: 1,
      currentProgress: studySessions.filter((s) => {
        const day = new Date(s.date).getDay()
        return day === 0 || day === 6
      }).length,
      unlocked: studySessions.some((s) => {
        const day = new Date(s.date).getDay()
        return day === 0 || day === 6
      }),
      reward: 75,
    },
  ]

  const categories = [
    { id: "all", name: "Tất cả", icon: Trophy },
    { id: "streak", name: "Chuỗi ngày", icon: Flame },
    { id: "learning", name: "Học tập", icon: Target },
    { id: "achievement", name: "Thành tích", icon: Award },
    { id: "special", name: "Đặc biệt", icon: Star },
  ]

  const filteredAchievements =
    selectedCategory === "all" ? achievements : achievements.filter((a) => a.category === selectedCategory)

  const unlockedCount = achievements.filter((a) => a.unlocked).length
  const totalRewards = achievements.filter((a) => a.unlocked).reduce((sum, a) => sum + a.reward, 0)

  const getCategoryIcon = (category: string) => {
    switch (category) {
      case "streak":
        return "🔥"
      case "learning":
        return "📚"
      case "achievement":
        return "🏆"
      case "special":
        return "⭐"
      default:
        return "🎯"
    }
  }

  const getCategoryColor = (category: string) => {
    switch (category) {
      case "streak":
        return "bg-orange-100 text-orange-800"
      case "learning":
        return "bg-blue-100 text-blue-800"
      case "achievement":
        return "bg-purple-100 text-purple-800"
      case "special":
        return "bg-yellow-100 text-yellow-800"
      default:
        return "bg-gray-100 text-gray-800"
    }
  }

  return (
    <Layout>
      <div className="space-y-8">
        {/* Header */}
        <div className="text-center space-y-4">
          <h1 className="text-3xl font-bold text-gray-800 flex items-center justify-center gap-2">
            <Trophy className="w-8 h-8 text-yellow-500" />
            Thành tích
          </h1>
          <p className="text-gray-600">Khám phá và mở khóa các thành tích trong hành trình học tập</p>

          {/* Stats */}
          <div className="flex justify-center gap-8 mt-6">
            <div className="text-center">
              <p className="text-2xl font-bold text-purple-600">{unlockedCount}</p>
              <p className="text-sm text-gray-600">Đã mở khóa</p>
            </div>
            <div className="text-center">
              <p className="text-2xl font-bold text-green-600">{totalRewards}</p>
              <p className="text-sm text-gray-600">Điểm thưởng</p>
            </div>
            <div className="text-center">
              <p className="text-2xl font-bold text-blue-600">{achievements.length}</p>
              <p className="text-sm text-gray-600">Tổng cộng</p>
            </div>
          </div>
        </div>

        {/* Category Filter */}
        <div className="flex flex-wrap justify-center gap-2">
          {categories.map((category) => {
            const Icon = category.icon
            return (
              <Button
                key={category.id}
                variant={selectedCategory === category.id ? "default" : "outline"}
                size="sm"
                onClick={() => setSelectedCategory(category.id)}
                className="flex items-center gap-2"
              >
                <Icon className="w-4 h-4" />
                {category.name}
              </Button>
            )
          })}
        </div>

        {/* Achievements Grid */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {filteredAchievements.map((achievement, index) => (
            <Card
              key={achievement.id}
              className={`relative transition-all duration-300 hover:shadow-lg animate-fade-in ${
                achievement.unlocked
                  ? "border-green-200 bg-gradient-to-br from-green-50 to-emerald-50"
                  : "border-gray-200 bg-gray-50"
              }`}
              style={{ animationDelay: `${index * 0.1}s` }}
            >
              {achievement.unlocked && (
                <div className="absolute -top-2 -right-2 w-6 h-6 bg-green-500 rounded-full flex items-center justify-center">
                  <CheckCircle className="w-4 h-4 text-white" />
                </div>
              )}

              <CardContent className="p-6">
                <div className="text-center space-y-4">
                  {/* Icon */}
                  <div className={`text-4xl ${!achievement.unlocked && "grayscale opacity-50"}`}>
                    {achievement.unlocked ? achievement.icon : "🔒"}
                  </div>

                  {/* Title and Category */}
                  <div>
                    <h3 className={`font-bold text-lg ${achievement.unlocked ? "text-gray-800" : "text-gray-500"}`}>
                      {achievement.title}
                    </h3>
                    <Badge variant="outline" className={`mt-2 ${getCategoryColor(achievement.category)}`}>
                      {getCategoryIcon(achievement.category)}{" "}
                      {achievement.category === "streak"
                        ? "Chuỗi ngày"
                        : achievement.category === "learning"
                          ? "Học tập"
                          : achievement.category === "achievement"
                            ? "Thành tích"
                            : "Đặc biệt"}
                    </Badge>
                  </div>

                  {/* Description */}
                  <p className={`text-sm ${achievement.unlocked ? "text-gray-600" : "text-gray-400"}`}>
                    {achievement.description}
                  </p>

                  {/* Progress */}
                  <div className="space-y-2">
                    <div className="flex justify-between text-sm">
                      <span className={achievement.unlocked ? "text-gray-600" : "text-gray-400"}>Tiến độ</span>
                      <span className={achievement.unlocked ? "text-gray-800" : "text-gray-500"}>
                        {Math.min(achievement.currentProgress, achievement.requirement)}/{achievement.requirement}
                      </span>
                    </div>
                    <Progress
                      value={
                        (Math.min(achievement.currentProgress, achievement.requirement) / achievement.requirement) * 100
                      }
                      className="h-2"
                    />
                  </div>

                  {/* Reward */}
                  <div
                    className={`flex items-center justify-center gap-2 ${
                      achievement.unlocked ? "text-green-600" : "text-gray-400"
                    }`}
                  >
                    <Award className="w-4 h-4" />
                    <span className="font-semibold">+{achievement.reward} điểm</span>
                  </div>

                  {/* Unlock date */}
                  {achievement.unlocked && achievement.unlockedAt && (
                    <div className="flex items-center justify-center gap-2 text-xs text-gray-500">
                      <Calendar className="w-3 h-3" />
                      <span>Mở khóa: {new Date(achievement.unlockedAt).toLocaleDateString("vi-VN")}</span>
                    </div>
                  )}
                </div>
              </CardContent>
            </Card>
          ))}
        </div>

        {/* Progress Summary */}
        <Card className="animate-fade-in">
          <CardHeader>
            <CardTitle className="flex items-center gap-2">
              <Target className="w-5 h-5" />
              Tổng quan tiến độ
            </CardTitle>
          </CardHeader>
          <CardContent>
            <div className="grid grid-cols-1 md:grid-cols-4 gap-6">
              {categories.slice(1).map((category) => {
                const categoryAchievements = achievements.filter((a) => a.category === category.id)
                const unlockedInCategory = categoryAchievements.filter((a) => a.unlocked).length
                const progressPercentage = (unlockedInCategory / categoryAchievements.length) * 100

                return (
                  <div key={category.id} className="text-center space-y-3">
                    <div className="text-2xl">{getCategoryIcon(category.id)}</div>
                    <div>
                      <h4 className="font-semibold">{category.name}</h4>
                      <p className="text-sm text-gray-600">
                        {unlockedInCategory}/{categoryAchievements.length}
                      </p>
                    </div>
                    <Progress value={progressPercentage} className="h-2" />
                    <p className="text-xs text-gray-500">{Math.round(progressPercentage)}% hoàn thành</p>
                  </div>
                )
              })}
            </div>
          </CardContent>
        </Card>

        {/* Motivational Message */}
        <Card className="bg-gradient-to-r from-purple-500 to-blue-500 text-white animate-fade-in">
          <CardContent className="p-6 text-center">
            <Trophy className="w-12 h-12 mx-auto mb-4 opacity-80" />
            <h3 className="text-xl font-bold mb-2">Tiếp tục phấn đấu!</h3>
            <p className="opacity-90">
              Mỗi từ vựng bạn học là một bước tiến gần hơn đến mục tiêu. Hãy duy trì động lực và mở khóa thêm nhiều
              thành tích!
            </p>
          </CardContent>
        </Card>
      </div>
    </Layout>
  )
}
