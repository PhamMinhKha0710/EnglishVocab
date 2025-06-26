"use client"

import { Sidebar } from "@/components/menu/sidebar"
import { AppHeader } from "@/components/header/app-header"
import { AppFooter } from "@/components/footer/app-footer"
import { useAuth } from "@/contexts/auth-context"

interface AppLayoutProps {
  children: React.ReactNode
}

export function AppLayout({ children }: AppLayoutProps) {
  const { isAuthenticated } = useAuth()

  return (
    <div className="min-h-screen bg-gray-50">
      <Sidebar isAuthenticated={isAuthenticated} />
      <AppHeader />
      
      {/* Main content */}
      <main className="pt-16 md:ml-64 min-h-screen">
        <div className="container mx-auto px-4 py-6">
          {children}
        </div>
      </main>
      
      <AppFooter />
    </div>
  )
} 