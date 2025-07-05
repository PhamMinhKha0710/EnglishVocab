"use client"

import { Link, useLocation } from "react-router-dom"
import { AuthButtons } from "@/components/menu/auth-buttons"
import { UserProfileButton } from "@/components/auth/UserProfileButton"
import { useAuth } from "@/contexts/auth-context"
import { navigationItems } from "@/components/menu/navigation-items"
import { BookOpen } from "lucide-react"
import { cn } from "@/lib/utils"

export function Navigation() {
  const { isAuthenticated } = useAuth()
  const location = useLocation()
  const pathname = location.pathname

  // Lọc ra các mục không bị ẩn trong menu
  const visibleNavigationItems = navigationItems.filter(item => !item.hideInMenu)

  return (
    <header className="sticky top-0 z-50 w-full border-b border-border/40 bg-background/95 backdrop-blur supports-[backdrop-filter]:bg-background/60">
      <div className="container flex h-14 max-w-screen-2xl items-center">
        <div className="mr-4 hidden md:flex">
          <Link to="/" className="mr-6 flex items-center space-x-2">
            <div className="w-6 h-6 bg-gradient-to-br from-purple-500 to-blue-500 rounded-md flex items-center justify-center">
              <BookOpen className="h-3 w-3 text-white" />
            </div>
            <span className="hidden font-bold sm:inline-block">FlashLearn</span>
          </Link>
          <nav className="flex items-center gap-6 text-sm">
            {visibleNavigationItems.map((item, index) => {
              const Icon = item.icon
              const isActive = pathname === item.href

              return (
                <Link
                  key={index}
                  to={item.href}
                  className={cn(
                    "flex items-center text-lg font-medium transition-colors hover:text-foreground/80 sm:text-sm",
                    isActive ? "text-foreground" : "text-foreground/60"
                  )}
                  >
                  <Icon className="mr-1 h-4 w-4" />
                    {item.label}
                </Link>
              )
            })}
          </nav>
        </div>

        <div className="flex flex-1 items-center justify-between space-x-2 md:justify-end">
          <div className="w-full flex-1 md:w-auto md:flex-none">
            {/* Placeholder for search */}
          </div>
          <div className="flex items-center">
            {isAuthenticated ? <UserProfileButton /> : <AuthButtons />}
          </div>
        </div>
      </div>
    </header>
  )
}
