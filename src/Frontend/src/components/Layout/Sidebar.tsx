"use client"

import { useState } from "react"
import { Link, useLocation } from "react-router-dom"
import { Button } from "@/components/ui/button"
import { Badge } from "@/components/ui/badge"
import { useAppStore } from "@/store/useAppStore"
import { Home, BookOpen, BarChart3, Settings, Trophy, Menu, X, Flame, Target, LogIn, UserPlus } from "lucide-react"
import { useAuth } from "@/contexts/auth-context"

// Các liên kết sẽ luôn hiển thị (cả khi đăng nhập và không đăng nhập)
const publicNavigationItems = [
  { href: "/", label: "Trang chủ", icon: Home },
]

// Các liên kết chỉ hiển thị khi đã đăng nhập
const privateNavigationItems = [
  { href: "/study", label: "Học từ vựng", icon: BookOpen },
  { href: "/progress", label: "Tiến độ", icon: BarChart3 },
  { href: "/achievements", label: "Thành tích", icon: Trophy },
  { href: "/settings", label: "Cài đặt", icon: Settings },
]

export function Sidebar() {
  const [isOpen, setIsOpen] = useState(false)
  const location = useLocation()
  const { userProgress } = useAppStore()
  const { isAuthenticated } = useAuth()

  return (
    <>
      {/* Mobile Menu Button */}
      <div className="md:hidden fixed top-4 left-4 z-50">
        <Button variant="outline" size="sm" onClick={() => setIsOpen(!isOpen)} className="bg-white/90 backdrop-blur-sm">
          {isOpen ? <X className="w-4 h-4" /> : <Menu className="w-4 h-4" />}
        </Button>
      </div>

      {/* Navigation Sidebar */}
      <nav
        className={`
          fixed top-0 left-0 h-full w-64 bg-white shadow-lg transform transition-transform duration-300 z-40
          ${isOpen ? "translate-x-0" : "-translate-x-full"}
          md:translate-x-0
        `}
      >
        <div className="p-6">
          <div className="flex items-center gap-2 mb-8">
            <div className="w-8 h-8 bg-gradient-to-br from-purple-500 to-blue-500 rounded-lg flex items-center justify-center">
              <BookOpen className="w-4 h-4 text-white" />
            </div>
            <h1 className="text-xl font-bold text-gray-800">FlashLearn</h1>
          </div>

          <div className="space-y-2">
            {/* Các liên kết công khai */}
            {publicNavigationItems.map((item) => {
              const Icon = item.icon
              const isActive = location.pathname === item.href

              return (
                <Link key={item.href} to={item.href}>
                  <Button
                    variant={isActive ? "default" : "ghost"}
                    className={`w-full justify-start ${
                      isActive ? "bg-purple-600 hover:bg-purple-700 text-white" : "text-gray-600 hover:text-gray-800"
                    }`}
                    onClick={() => setIsOpen(false)}
                  >
                    <Icon className="w-4 h-4 mr-3" />
                    {item.label}
                  </Button>
                </Link>
              )
            })}

            {/* Các liên kết riêng tư (chỉ hiển thị khi đã đăng nhập) */}
            {isAuthenticated && privateNavigationItems.map((item) => {
              const Icon = item.icon
              const isActive = location.pathname === item.href

              return (
                <Link key={item.href} to={item.href}>
                  <Button
                    variant={isActive ? "default" : "ghost"}
                    className={`w-full justify-start ${
                      isActive ? "bg-purple-600 hover:bg-purple-700 text-white" : "text-gray-600 hover:text-gray-800"
                    }`}
                    onClick={() => setIsOpen(false)}
                  >
                    <Icon className="w-4 h-4 mr-3" />
                    {item.label}
                  </Button>
                </Link>
              )
            })}

            {/* Hiển thị nút đăng nhập/đăng ký nếu chưa đăng nhập */}
            {!isAuthenticated && (
              <div className="mt-4 space-y-2">
                <Link to="/auth/login">
                  <Button variant="outline" className="w-full justify-start">
                    <LogIn className="w-4 h-4 mr-3" />
                    Đăng nhập
                  </Button>
                </Link>
                <Link to="/auth/register">
                  <Button className="w-full justify-start bg-purple-600 hover:bg-purple-700">
                    <UserPlus className="w-4 h-4 mr-3" />
                    Đăng ký
                  </Button>
                </Link>
              </div>
            )}
          </div>

          {/* Quick Stats - chỉ hiển thị khi đã đăng nhập */}
          {isAuthenticated && (
            <div className="mt-8 p-4 bg-gray-50 rounded-lg">
              <h3 className="text-sm font-semibold text-gray-700 mb-2">Hôm nay</h3>
              <div className="space-y-2">
                <div className="flex justify-between items-center">
                  <span className="text-xs text-gray-600 flex items-center gap-1">
                    <Target className="w-3 h-3" />
                    Mục tiêu
                  </span>
                  <Badge variant="secondary" className="text-xs">
                    {userProgress.wordsToday}/{userProgress.dailyGoal}
                  </Badge>
                </div>
                <div className="flex justify-between items-center">
                  <span className="text-xs text-gray-600 flex items-center gap-1">
                    <Flame className="w-3 h-3" />
                    Chuỗi ngày
                  </span>
                  <Badge variant="secondary" className="text-xs">
                    {userProgress.currentStreak} ngày
                  </Badge>
                </div>
              </div>
            </div>
          )}
        </div>
      </nav>

      {/* Overlay for mobile */}
      {isOpen && <div className="fixed inset-0 bg-black/20 z-30 md:hidden" onClick={() => setIsOpen(false)} />}
    </>
  )
}
