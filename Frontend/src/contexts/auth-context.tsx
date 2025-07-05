import React, { createContext, useContext, useState, useEffect } from "react"
import { useNavigate } from "react-router-dom"
import { apiService, AuthUser, UpdateProfileParams } from "@/services/api-service"

// Định nghĩa interface cho user
interface User {
  id: number
  username: string
  email: string
  firstName: string
  lastName: string
  roles: string[]
  accessToken: string
  refreshToken: string
}

// Định nghĩa interface cho context
interface AuthContextType {
  user: User | null
  isAuthenticated: boolean
  isLoading: boolean
  error: string | null
  register: (username: string, email: string, password: string, firstName: string, lastName: string) => Promise<boolean>
  login: (email: string, password: string, rememberMe?: boolean) => Promise<boolean>
  logout: () => void
  updateProfile: (profileData: UpdateProfileParams, onSuccess?: () => void) => Promise<boolean>
  refreshUserData: () => void
}

// Hàm che giấu thông tin nhạy cảm trong logs
const maskSensitiveData = (data: any) => {
  if (!data) return data;
  
  const maskedData = { ...data };
  
  // Che giấu tokens
  if (maskedData.accessToken) maskedData.accessToken = "********";
  if (maskedData.refreshToken) maskedData.refreshToken = "********";
  
  return maskedData;
};

// Tạo context
const AuthContext = createContext<AuthContextType | undefined>(undefined)

// Hook để sử dụng context
export const useAuth = () => {
  const context = useContext(AuthContext)
  if (context === undefined) {
    throw new Error("useAuth must be used within an AuthProvider")
  }
  return context
}

// Tạo AuthProvider component
export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [user, setUser] = useState<User | null>(null)
  const [isLoading, setIsLoading] = useState<boolean>(true)
  const [error, setError] = useState<string | null>(null)
  const navigate = useNavigate()

  // Kiểm tra xem người dùng đã đăng nhập chưa khi component mount
  useEffect(() => {
    const checkLoggedIn = () => {
      // Kiểm tra cả localStorage và sessionStorage
      const storedUser = localStorage.getItem("user") || sessionStorage.getItem("user")
      if (storedUser) {
        try {
          const parsedUser = JSON.parse(storedUser)
          setUser(parsedUser)
        } catch (err) {
          console.error("Error parsing user from storage")
          localStorage.removeItem("user")
          sessionStorage.removeItem("user")
        }
      }
      setIsLoading(false)
    }

    checkLoggedIn()
  }, [])

  // Hàm làm mới dữ liệu người dùng từ localStorage
  const refreshUserData = () => {
    const storedUser = localStorage.getItem("user")
    if (storedUser) {
      try {
        const parsedUser = JSON.parse(storedUser)
        setUser(parsedUser)
      } catch (err) {
        console.error("Error parsing user from localStorage")
      }
    }
  }

  // Hàm đăng ký
  const register = async (
    username: string,
    email: string,
    password: string,
    firstName: string,
    lastName: string
  ): Promise<boolean> => {
    setIsLoading(true)
    setError(null)

    try {
      const result = await apiService.register({
        username,
        email,
        password,
        confirmPassword: password,
        firstName,
        lastName
      })

      setIsLoading(false)
      return result.succeeded
    } catch (err: any) {
      setError(err.message || "Đã xảy ra lỗi khi đăng ký")
      setIsLoading(false)
      return false
    }
  }

  // Hàm đăng nhập
  const login = async (email: string, password: string, rememberMe: boolean = false): Promise<boolean> => {
    setIsLoading(true)
    setError(null)
    console.log("Bắt đầu đăng nhập")

    try {
      const result = await apiService.login({
        email,
        password,
      })

      if (result && result.isAuthenticated) {
        // Chỉ log thông tin cơ bản, không bao gồm token
        console.log("Xác thực thành công")
        
        const loggedInUser: User = {
          id: typeof result.id === 'string' ? parseInt(result.id) : result.id,
          username: result.username || "",
          email: result.email || "",
          firstName: result.firstName || "",
          lastName: result.lastName || "",
          roles: Array.isArray(result.roles) ? result.roles : 
                 result.roles ? [result.roles] : [],
          accessToken: result.accessToken || "",
          refreshToken: result.refreshToken || "",
        }

        // Lưu trữ và cập nhật trạng thái
        setUser(loggedInUser)
        
        // Sử dụng storage phù hợp dựa trên rememberMe
        const storage = rememberMe ? localStorage : sessionStorage
        storage.setItem("user", JSON.stringify(loggedInUser))
        
        setIsLoading(false)
        return true
      } else {
        const errorMsg = result?.message || "Đăng nhập thất bại"
        console.log("Đăng nhập thất bại")
        setError(errorMsg)
        setIsLoading(false)
        return false
      }
    } catch (err: any) {
      console.error("Lỗi khi đăng nhập:", err.message)
      setError(err.message || "Đã xảy ra lỗi khi đăng nhập")
      setIsLoading(false)
      return false
    }
  }

  // Hàm cập nhật hồ sơ người dùng
  const updateProfile = async (profileData: UpdateProfileParams, onSuccess?: () => void): Promise<boolean> => {
    setIsLoading(true)
    setError(null)
    
    if (!user) {
      setError("Bạn cần đăng nhập để cập nhật hồ sơ")
      setIsLoading(false)
      return false
    }
    
    try {
      console.log("Đang cập nhật hồ sơ người dùng:", profileData.username)
      const result = await apiService.updateProfile(profileData, user.accessToken)
      
      if (result && result.succeeded) {
        console.log("Cập nhật hồ sơ thành công")
        
        // Cập nhật thông tin người dùng trong state và localStorage
        if (result.user) {
          const updatedUser: User = {
            ...user,
            username: result.user.username || user.username,
            email: result.user.email || user.email,
            firstName: result.user.firstName || user.firstName,
            lastName: result.user.lastName || user.lastName,
          }
          
          // Cập nhật state và localStorage ngay lập tức
          setUser(updatedUser)
          localStorage.setItem("user", JSON.stringify(updatedUser))
          
          // Gọi callback nếu được cung cấp
          if (onSuccess && typeof onSuccess === 'function') {
            onSuccess()
          }
        }
        
        setIsLoading(false)
        return true
      } else {
        const errorMsg = result?.message || "Cập nhật hồ sơ thất bại"
        console.log("Cập nhật hồ sơ thất bại:", errorMsg)
        setError(errorMsg)
        setIsLoading(false)
        return false
      }
    } catch (err: any) {
      console.error("Lỗi khi cập nhật hồ sơ:", err.message)
      setError(err.message || "Đã xảy ra lỗi khi cập nhật hồ sơ")
      setIsLoading(false)
      return false
    }
  }

  // Hàm đăng xuất
  const logout = async () => {
    console.log("Đang đăng xuất...")
    setIsLoading(true)
    
    try {
      // Gọi API đăng xuất nếu có token
      if (user?.accessToken) {
        await apiService.logout(user.accessToken)
      }
      
      // Xóa thông tin người dùng khỏi localStorage và sessionStorage
      localStorage.removeItem("user")
      sessionStorage.removeItem("user")
      
      // Cập nhật state
      setUser(null)
      
      // Kết thúc loading và điều hướng
      setIsLoading(false)
      navigate("/auth/login")
      console.log("Đăng xuất thành công")
    } catch (err) {
      console.error("Lỗi khi đăng xuất:", err)
      // Xóa thông tin người dùng khỏi localStorage và sessionStorage ngay cả khi có lỗi
      localStorage.removeItem("user")
      sessionStorage.removeItem("user")
      setUser(null)
      setIsLoading(false)
      navigate("/auth/login")
    }
  }

  // Giá trị trả về cho context
  const value = {
    user,
    isAuthenticated: !!user,
    isLoading,
    error,
    register,
    login,
    logout,
    updateProfile,
    refreshUserData
  }

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>
} 