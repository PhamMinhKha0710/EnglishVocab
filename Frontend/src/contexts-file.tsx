"use client"

import React, { createContext, useContext, useState, useEffect } from "react"
import { useRouter } from "next/navigation"

interface User {
  id: string
  email: string
  firstName: string
  lastName: string
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

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [user, setUser] = useState<User | null>(null)
  const [isLoading, setIsLoading] = useState(true)
  const router = useRouter()

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

  // Login function
  const login = async (email: string, password: string) => {
    setIsLoading(true)
    try {
      // In a real app, you would make an API request to your backend
      // For now, we'll simulate a successful login
      await new Promise(resolve => setTimeout(resolve, 1000))
      
      // Mock user data
      const userData: User = {
        id: "user-123",
        email,
        firstName: "Người",
        lastName: "Dùng"
      }
      
      // Save to localStorage
      localStorage.setItem("user", JSON.stringify(userData))
      setUser(userData)
      
      // Redirect to home page
      router.push("/")
    } catch (error) {
      console.error("Login error:", error)
      throw new Error("Đăng nhập không thành công. Vui lòng kiểm tra email và mật khẩu.")
    } finally {
      setIsLoading(false)
    }
  }

  // Register function
  const register = async (firstName: string, lastName: string, email: string, password: string) => {
    setIsLoading(true)
    try {
      // In a real app, you would make an API request to your backend
      // For now, we'll simulate a successful registration
      await new Promise(resolve => setTimeout(resolve, 1000))
      
      // After successful registration, redirect to login
      router.push("/auth/login")
    } catch (error) {
      console.error("Registration error:", error)
      throw new Error("Đăng ký không thành công. Vui lòng thử lại sau.")
    } finally {
      setIsLoading(false)
    }
  }

  // Logout function
  const logout = () => {
    localStorage.removeItem("user")
    setUser(null)
    router.push("/auth/login")
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