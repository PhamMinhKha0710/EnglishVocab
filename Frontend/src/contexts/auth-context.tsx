import React, { createContext, useContext, useState, useEffect } from "react"
import { useNavigate } from "react-router-dom"
import { apiService, AuthUser, UpdateProfileParams } from "@/services/api-service"

// Định nghĩa interface cho user
interface User {
  id: string
  username: string
  email: string
  firstName: string
  lastName: string
  roles: string[]
  accessToken: string
  refreshToken: string
}

// Interface cho tham số đăng ký
interface RegisterParams {
  username: string
  email: string
  password: string
  confirmPassword?: string
  firstName: string
  lastName: string
}

// Định nghĩa interface cho context
interface AuthContextType {
  user: User | null
  isAuthenticated: boolean
  isLoading: boolean
  error: string | null
  register: (params: RegisterParams) => Promise<boolean>
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
  const register = async (params: RegisterParams): Promise<boolean> => {
    setIsLoading(true)
    setError(null)

    try {
      console.log("Gửi yêu cầu đăng ký đến API:", maskSensitiveData(params))
      const result = await apiService.register({
        username: params.username,
        email: params.email,
        password: params.password,
        confirmPassword: params.confirmPassword || params.password, // Sử dụng password nếu confirmPassword không được cung cấp
        firstName: params.firstName,
        lastName: params.lastName
      })
      
      console.log("Kết quả từ API đăng ký:", result)

      setIsLoading(false)
      return result.succeeded
    } catch (err: any) {
      console.error("Lỗi trong hàm register của auth-context:", err)
      setError(err.message || "Đã xảy ra lỗi khi đăng ký")
      setIsLoading(false)
      return false
    }
  }

  // Hàm đăng nhập
  const login = async (email: string, password: string, rememberMe: boolean = false): Promise<boolean> => {
    setIsLoading(true)
    setError(null)
    console.log("Bắt đầu đăng nhập với email:", email)

    try {
      const result = await apiService.login({
        email,
        password,
      })

      console.log("Kết quả đăng nhập:", result.isAuthenticated)

      if (result && result.isAuthenticated) {
        // Chỉ log thông tin cơ bản, không bao gồm token
        console.log("Đăng nhập thành công cho:", result.username)
        
        const loggedInUser: User = {
          id: result.id?.toString() || "",
          username: result.username || "",
          email: result.email || "",
          firstName: result.firstName || "",
          lastName: result.lastName || "",
          roles: Array.isArray(result.roles) ? result.roles : 
                 result.roles ? [result.roles] : [],
          accessToken: result.accessToken || "",
          refreshToken: result.refreshToken || "",
        }

        console.log("Lưu thông tin người dùng vào storage")
        
        // Lưu trữ và cập nhật trạng thái
        setUser(loggedInUser)
        
        // Sử dụng storage phù hợp dựa trên rememberMe
        const storage = rememberMe ? localStorage : sessionStorage
        storage.setItem("user", JSON.stringify(loggedInUser))
        
        setIsLoading(false)
        return true
      } else {
        const errorMsg = result?.message || "Đăng nhập thất bại"
        console.error("Đăng nhập thất bại:", errorMsg)
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
      
      // Đảm bảo profileData có userId của người dùng hiện tại
      const updatedProfileData = {
        ...profileData,
        userId: user.id
      }
      
      console.log("Dữ liệu gửi đi:", updatedProfileData)
      
      const result = await apiService.updateProfile(updatedProfileData, user.accessToken)
      console.log("Kết quả nhận được:", result)
      
      if (result && result.succeeded) {
        console.log("Cập nhật hồ sơ thành công")
        
        // Cập nhật thông tin người dùng trong state và localStorage
        if (result.user) {
          const updatedUser: User = {
            ...user,
            id: result.user.id || user.id,
            username: result.user.username || user.username,
            email: result.user.email || user.email,
            firstName: result.user.firstName || user.firstName,
            lastName: result.user.lastName || user.lastName,
          }
          
          console.log("Cập nhật dữ liệu người dùng với:", updatedUser)
          
          // Cập nhật trong localStorage
          localStorage.removeItem("user")
          localStorage.setItem("user", JSON.stringify(updatedUser))
          
          // Cập nhật trong state
          setUser(updatedUser)
            
          setIsLoading(false)
          
          // Gọi callback nếu được cung cấp
          if (onSuccess && typeof onSuccess === 'function') {
            onSuccess()
          }
        } else {
          // Nếu không có dữ liệu user trả về, refresh lại từ localStorage
          refreshUserData()
          
          setIsLoading(false)
          
          // Gọi callback nếu được cung cấp
          if (onSuccess && typeof onSuccess === 'function') {
            onSuccess()
          }
        }
        
        return true
      } else {
        // Kiểm tra kỹ lưỡng hơn để đảm bảo chỉ hiển thị lỗi khi thực sự thất bại
        const errorMsg = result?.message || "Cập nhật hồ sơ thất bại"
        console.error("Cập nhật hồ sơ thất bại:", errorMsg)
        setError(errorMsg)
        setIsLoading(false)
        return false
      }
    } catch (err: any) {
      console.error("Lỗi khi cập nhật hồ sơ:", err.message)
      
      // Xử lý lỗi token hết hạn
      if (err.message && (
        err.message.includes("Unauthorized") || 
        err.message.includes("hết hạn") || 
        err.message.includes("401")
      )) {
        console.log("Phiên đăng nhập hết hạn, đang đăng xuất...")
        // Đăng xuất người dùng
        localStorage.removeItem("user")
        sessionStorage.removeItem("user")
        setUser(null)
        
        // Đặt thông báo lỗi và chuyển hướng
        setError("Phiên làm việc đã hết hạn. Vui lòng đăng nhập lại.")
        navigate("/auth/login")
      }
      
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