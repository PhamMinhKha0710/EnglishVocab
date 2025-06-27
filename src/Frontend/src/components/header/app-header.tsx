import { Link } from "react-router-dom"
import { BookOpen, Bell } from "lucide-react"
import { Button } from "@/components/ui/button"
import { useAuth } from "@/contexts/auth-context"
import { UserProfileButton } from "@/components/auth/UserProfileButton"

export function AppHeader() {
  const { isAuthenticated } = useAuth()

  return (
    <header className="bg-white border-b border-gray-200 fixed top-0 right-0 left-0 md:left-64 z-30">
      <div className="px-4 h-16 flex items-center justify-between">
        {/* Logo - Only visible on mobile */}
        <div className="flex md:hidden items-center gap-2">
          <div className="w-8 h-8 bg-gradient-to-br from-purple-500 to-blue-500 rounded-lg flex items-center justify-center">
            <BookOpen className="w-4 h-4 text-white" />
          </div>
          <h1 className="text-xl font-bold text-gray-800">FlashLearn</h1>
        </div>

        {/* Right side - User menu */}
        <div className="flex items-center gap-4 ml-auto">
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
                <Button variant="ghost" size="sm">Đăng nhập</Button>
              </Link>
              <Link to="/auth/register">
                <Button size="sm" className="bg-purple-600 hover:bg-purple-700">Đăng ký</Button>
              </Link>
            </div>
          )}
        </div>
      </div>
    </header>
  )
} 