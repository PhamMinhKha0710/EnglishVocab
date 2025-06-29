"use client"

import { Navigate } from "react-router-dom"
import { useAuth } from "@/contexts/auth-context"
import { ReactNode } from "react"

interface GuestRouteProps {
  children: ReactNode
}

export function GuestRoute({ children }: GuestRouteProps) {
  const { isAuthenticated, isLoading } = useAuth()

  // Đang kiểm tra trạng thái đăng nhập
  if (isLoading) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gradient-to-br from-purple-50 to-blue-50">
        <div className="flex flex-col items-center gap-4">
          <div className="w-10 h-10 border-4 border-purple-500 border-t-transparent rounded-full animate-spin"></div>
          <p className="text-gray-600">Đang tải...</p>
        </div>
      </div>
    )
  }

  // Nếu đã đăng nhập, chuyển hướng đến trang chủ
  if (isAuthenticated) {
    return <Navigate to="/" replace />
  }

  // Nếu chưa đăng nhập, hiển thị nội dung của route
  return <>{children}</>
} 