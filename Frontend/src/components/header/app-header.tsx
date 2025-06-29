import { Link, useLocation } from "react-router-dom"
import { BookOpen, Bell, LogIn, UserPlus, Menu, X } from "lucide-react"
import { Button } from "@/components/ui/button"
import { useAuth } from "@/contexts/auth-context"
import { UserProfileButton } from "@/components/auth/UserProfileButton"
import { navigationItems } from "@/components/menu/navigation-items"
import { useState } from "react"
import { cn } from "@/lib/utils"

export function AppHeader() {
  const { isAuthenticated } = useAuth()
  const location = useLocation()
  const pathname = location.pathname
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false)

  // Lọc ra các mục không bị ẩn trong menu
  const visibleNavigationItems = navigationItems.filter(item => !item.hideInMenu)

  return (
    <header className="bg-white border-b border-gray-200 fixed top-0 right-0 left-0 z-30">
      <div className="px-4 h-16 flex items-center justify-between">
        {/* Logo */}
        <div className="flex items-center gap-2">
          {/* Mobile menu button */}
          <Button 
            variant="ghost" 
            size="icon" 
            className="md:hidden"
            onClick={() => setMobileMenuOpen(!mobileMenuOpen)}
          >
            {mobileMenuOpen ? <X className="h-5 w-5" /> : <Menu className="h-5 w-5" />}
          </Button>
          
          <Link to="/" className="flex items-center gap-2">
          <div className="w-8 h-8 bg-gradient-to-br from-purple-500 to-blue-500 rounded-lg flex items-center justify-center">
            <BookOpen className="w-4 h-4 text-white" />
          </div>
          <h1 className="text-xl font-bold text-gray-800">FlashLearn</h1>
          </Link>
        </div>

        {/* Desktop Navigation */}
        <nav className="hidden md:flex items-center space-x-6">
          {visibleNavigationItems.map((item, index) => {
            const Icon = item.icon
            const isActive = pathname === item.href

            return (
              <Link
                key={index}
                to={item.href}
                className={cn(
                  "flex items-center text-sm font-medium transition-colors hover:text-gray-900",
                  isActive ? "text-gray-900" : "text-gray-600"
                )}
              >
                <Icon className="mr-1 h-4 w-4" />
                {item.label}
              </Link>
            )
          })}
        </nav>

        {/* Right side - User menu */}
        <div className="flex items-center gap-4">
          {isAuthenticated ? (
            <>
              {/* Notifications */}
              <Button variant="ghost" size="icon" className="relative">
                <Bell className="h-5 w-5" />
                <span className="absolute top-0 right-0 h-2 w-2 bg-red-500 rounded-full"></span>
              </Button>
              
              {/* User menu */}
              <UserProfileButton />
            </>
          ) : (
            <div className="flex items-center gap-2">
              <Link to="/auth/login">
                <Button variant="ghost" size="sm" className="flex items-center gap-1">
                  <LogIn className="h-4 w-4" />
                  <span className="hidden sm:inline">Đăng nhập</span>
                </Button>
              </Link>
              <Link to="/auth/register">
                <Button size="sm" className="bg-purple-600 hover:bg-purple-700 flex items-center gap-1">
                  <UserPlus className="h-4 w-4" />
                  <span className="hidden sm:inline">Đăng ký</span>
                </Button>
              </Link>
            </div>
          )}
        </div>
      </div>

      {/* Mobile Navigation Menu */}
      {mobileMenuOpen && (
        <div className="md:hidden bg-white border-t border-gray-200">
          <div className="px-2 pt-2 pb-3 space-y-1">
            {visibleNavigationItems.map((item, index) => {
              const Icon = item.icon
              const isActive = pathname === item.href

              return (
                <Link
                  key={index}
                  to={item.href}
                  className={cn(
                    "flex items-center px-3 py-2 rounded-md text-base font-medium",
                    isActive 
                      ? "bg-purple-100 text-purple-700" 
                      : "text-gray-600 hover:bg-gray-50 hover:text-gray-900"
                  )}
                  onClick={() => setMobileMenuOpen(false)}
                >
                  <Icon className="mr-3 h-5 w-5" />
                  {item.label}
                </Link>
              )
            })}
          </div>
        </div>
      )}
    </header>
  )
} 