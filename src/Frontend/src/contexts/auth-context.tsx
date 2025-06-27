import React, { createContext, useContext, useState, useEffect } from "react"
import { useNavigate } from "react-router-dom"

interface User {
  id: string
  email: string
  firstName: string
  lastName: string
  avatar?: string
  createdAt: string
}

interface AuthContextType {
  user: User | null
  isLoading: boolean
  isAuthenticated: boolean
  login: (email: string, password: string) => Promise<void>
  register: (firstName: string, lastName: string, email: string, password: string) => Promise<void>
  logout: () => void
}

const AuthContext = createContext<AuthContextType | undefined>(undefined)

// Khóa cho danh sách người dùng trong localStorage
const USERS_STORAGE_KEY = "flashlearn_users"

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [user, setUser] = useState<User | null>(null)
  const [isLoading, setIsLoading] = useState(true)
  const navigate = useNavigate()

  // Check if user is logged in on initial load
  useEffect(() => {
    const checkAuth = async () => {
      try {
        // In a real app, you would check with your backend API
        // For now, we'll check localStorage
        const userData = localStorage.getItem("user")
        if (userData) {
          setUser(JSON.parse(userData))
        }
      } catch (error) {
        console.error("Authentication error:", error)
      } finally {
        setIsLoading(false)
      }
    }

    checkAuth()
  }, [])

  // Hàm lấy danh sách người dùng từ localStorage
  const getUsers = (): { [email: string]: User & { password: string } } => {
    const usersJson = localStorage.getItem(USERS_STORAGE_KEY)
    return usersJson ? JSON.parse(usersJson) : {}
  }

  // Login function
  const login = async (email: string, password: string) => {
    setIsLoading(true)
    try {
      // Thay vì mock dữ liệu, kiểm tra trong localStorage
      const users = getUsers()
      const user = users[email]

      if (!user || user.password !== password) {
        throw new Error("Email hoặc mật khẩu không chính xác")
      }
      
      // Lấy thông tin người dùng (trừ mật khẩu)
      const { password: _, ...userData } = user
      
      // Save to localStorage
      localStorage.setItem("user", JSON.stringify(userData))
      setUser(userData)
      
      // Redirect to home page
      navigate("/")
    } catch (error) {
      console.error("Login error:", error)
      throw new Error(error instanceof Error ? error.message : "Đăng nhập không thành công")
    } finally {
      setIsLoading(false)
    }
  }

  // Register function
  const register = async (firstName: string, lastName: string, email: string, password: string) => {
    setIsLoading(true)
    try {
      // Kiểm tra xem email đã tồn tại chưa
      const users = getUsers()
      
      if (users[email]) {
        throw new Error("Email này đã được đăng ký")
      }
      
      // Tạo người dùng mới
      const newUser = {
        id: `user-${Date.now()}`,
        email,
        firstName,
        lastName,
        password,
        createdAt: new Date().toISOString(),
        avatar: `https://ui-avatars.com/api/?name=${firstName}+${lastName}&background=random`
      }
      
      // Lưu vào danh sách người dùng
      users[email] = newUser
      localStorage.setItem(USERS_STORAGE_KEY, JSON.stringify(users))
      
      // After successful registration, redirect to login
      navigate("/auth/login")
    } catch (error) {
      console.error("Registration error:", error)
      throw new Error(error instanceof Error ? error.message : "Đăng ký không thành công")
    } finally {
      setIsLoading(false)
    }
  }

  // Logout function
  const logout = () => {
    localStorage.removeItem("user")
    setUser(null)
    navigate("/auth/login")
  }

  const value = {
    user,
    isLoading,
    isAuthenticated: !!user,
    login,
    register,
    logout
  }

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>
}

export function useAuth() {
  const context = useContext(AuthContext)
  if (context === undefined) {
    throw new Error("useAuth must be used within an AuthProvider")
  }
  return context
} 