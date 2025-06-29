"use client"

import { Outlet } from "react-router-dom"
import { AppHeader } from "@/components/header/app-header"
import { AppFooter } from "@/components/footer/app-footer"
import { useAuth } from "@/contexts/auth-context"

export function AppLayout() {
  const { isAuthenticated } = useAuth()

  return (
    <div className="min-h-screen bg-gray-50">
      <AppHeader />
      
      {/* Main content */}
      <main className="pt-16 min-h-screen">
        <div className="container mx-auto px-4 py-6">
          <Outlet />
        </div>
      </main>
      
      <AppFooter />
    </div>
  )
} 