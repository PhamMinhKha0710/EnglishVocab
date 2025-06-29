"use client"

import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { useAuth } from "@/contexts/auth-context"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { Separator } from "@/components/ui/separator"
import { toast } from "@/components/ui/use-toast"
import { Toaster } from "@/components/ui/toaster"
import { UpdateProfileParams } from "@/services/api-service"
import { Loader2, ArrowLeft, User, Mail, AtSign } from "lucide-react"
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"

export default function EditProfilePage() {
  const { user, updateProfile, isLoading, error, isAuthenticated } = useAuth()
  const navigate = useNavigate()
  const [isClient, setIsClient] = useState(false)

  // Form state
  const [formData, setFormData] = useState<UpdateProfileParams>({
    firstName: "",
    lastName: "",
    email: "",
    username: "",
  })

  // Form validation state
  const [formErrors, setFormErrors] = useState({
    firstName: "",
    lastName: "",
    email: "",
    username: "",
  })

  // Tạo chữ cái đầu từ tên người dùng cho avatar fallback
  const getInitials = () => {
    if (!user) return "U"
    const firstName = user.firstName || ""
    const lastName = user.lastName || ""
    return (firstName.charAt(0) + (lastName.charAt(0) || "")).toUpperCase()
  }

  // Kiểm tra xác thực và điền thông tin người dùng vào form
  useEffect(() => {
    setIsClient(true)
    
    // Nếu không đăng nhập, chuyển hướng đến trang đăng nhập
    if (!isAuthenticated && isClient) {
      navigate("/auth/login")
      return
    }

    // Điền thông tin người dùng vào form
    if (user) {
      setFormData({
        firstName: user.firstName || "",
        lastName: user.lastName || "",
        email: user.email || "",
        username: user.username || "",
      })
    }
  }, [isAuthenticated, navigate, user, isClient])

  // Xử lý thay đổi input
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target
    setFormData(prev => ({
      ...prev,
      [name]: value
    }))
    
    // Xóa lỗi khi người dùng bắt đầu nhập
    if (formErrors[name as keyof typeof formErrors]) {
      setFormErrors(prev => ({
        ...prev,
        [name]: ""
      }))
    }
  }

  // Xác thực form
  const validateForm = () => {
    let valid = true
    const errors = {
      firstName: "",
      lastName: "",
      email: "",
      username: "",
    }

    // Kiểm tra firstName
    if (!formData.firstName.trim()) {
      errors.firstName = "Tên là bắt buộc"
      valid = false
    }

    // Kiểm tra lastName
    if (!formData.lastName.trim()) {
      errors.lastName = "Họ là bắt buộc"
      valid = false
    }

    // Kiểm tra email
    if (!formData.email.trim()) {
      errors.email = "Email là bắt buộc"
      valid = false
    } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email)) {
      errors.email = "Email không hợp lệ"
      valid = false
    }

    // Kiểm tra username
    if (!formData.username.trim()) {
      errors.username = "Tên đăng nhập là bắt buộc"
      valid = false
    } else if (formData.username.length < 3) {
      errors.username = "Tên đăng nhập phải có ít nhất 3 ký tự"
      valid = false
    }

    setFormErrors(errors)
    return valid
  }

  // Xử lý submit form
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    
    if (!validateForm()) {
      return
    }
    
    try {
      const success = await updateProfile(formData, () => {
        // Hiển thị thông báo thành công
        toast({
          title: "Cập nhật thành công",
          description: "Thông tin hồ sơ của bạn đã được cập nhật",
          variant: "default",
        })
        
        // Chuyển hướng ngay lập tức đến trang hồ sơ
        navigate("/profile")
      })
      
      if (!success) {
        toast({
          title: "Cập nhật thất bại",
          description: error || "Đã xảy ra lỗi khi cập nhật hồ sơ",
          variant: "destructive",
        })
      }
    } catch (err: any) {
      toast({
        title: "Lỗi",
        description: err.message || "Đã xảy ra lỗi khi cập nhật hồ sơ",
        variant: "destructive",
      })
    }
  }

  // Hủy và quay lại trang hồ sơ
  const handleCancel = () => {
    navigate("/profile")
  }

  if (!isClient || !isAuthenticated) {
    return null // Không hiển thị gì nếu đang kiểm tra xác thực hoặc chưa đăng nhập
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-purple-50 to-blue-50">
      <Toaster />

      <main className="p-6">
        {/* Header */}
        <div className="mb-8">
          <div className="flex flex-col md:flex-row md:items-center md:justify-between gap-4">
            <div className="flex items-center gap-3">
              <Button 
                variant="ghost" 
                size="icon" 
                onClick={() => navigate("/profile")}
                className="rounded-full"
              >
                <ArrowLeft className="h-5 w-5" />
              </Button>
              <div>
                <h1 className="text-3xl font-bold text-gray-800">Chỉnh sửa hồ sơ</h1>
                <p className="text-gray-600 mt-1">Cập nhật thông tin cá nhân của bạn</p>
              </div>
            </div>
          </div>
        </div>

        <div className="max-w-3xl mx-auto">
          <Card className="border-0 shadow-md">
            <CardHeader className="pb-0 flex flex-col items-center">
              <Avatar className="h-24 w-24 border-4 border-white shadow-lg mb-4">
                <AvatarImage src="/placeholder-user.jpg" alt={user?.username || "User"} />
                <AvatarFallback className="text-2xl bg-purple-100 text-purple-700">
                  {getInitials()}
                </AvatarFallback>
              </Avatar>
              <CardTitle className="text-2xl text-center">{user?.firstName} {user?.lastName}</CardTitle>
              <CardDescription className="text-center">
                Cập nhật thông tin hồ sơ của bạn
              </CardDescription>
            </CardHeader>
            
            <form onSubmit={handleSubmit}>
              <CardContent className="space-y-6 pt-6">
                <div className="grid grid-cols-1 gap-6 sm:grid-cols-2">
                  {/* First Name */}
                  <div className="space-y-2">
                    <Label htmlFor="firstName" className="flex items-center gap-2">
                      <User className="h-4 w-4 text-gray-500" />
                      Tên
                    </Label>
                    <Input
                      id="firstName"
                      name="firstName"
                      value={formData.firstName}
                      onChange={handleChange}
                      placeholder="Nhập tên của bạn"
                      className={formErrors.firstName ? "border-red-500" : ""}
                    />
                    {formErrors.firstName && (
                      <p className="text-sm text-red-500">{formErrors.firstName}</p>
                    )}
                  </div>
                  
                  {/* Last Name */}
                  <div className="space-y-2">
                    <Label htmlFor="lastName" className="flex items-center gap-2">
                      <User className="h-4 w-4 text-gray-500" />
                      Họ
                    </Label>
                    <Input
                      id="lastName"
                      name="lastName"
                      value={formData.lastName}
                      onChange={handleChange}
                      placeholder="Nhập họ của bạn"
                      className={formErrors.lastName ? "border-red-500" : ""}
                    />
                    {formErrors.lastName && (
                      <p className="text-sm text-red-500">{formErrors.lastName}</p>
                    )}
                  </div>
                </div>

                <Separator />

                {/* Email */}
                <div className="space-y-2">
                  <Label htmlFor="email" className="flex items-center gap-2">
                    <Mail className="h-4 w-4 text-gray-500" />
                    Email
                  </Label>
                  <Input
                    id="email"
                    name="email"
                    type="email"
                    value={formData.email}
                    onChange={handleChange}
                    placeholder="your.email@example.com"
                    className={formErrors.email ? "border-red-500" : ""}
                  />
                  {formErrors.email && (
                    <p className="text-sm text-red-500">{formErrors.email}</p>
                  )}
                </div>

                {/* Username */}
                <div className="space-y-2">
                  <Label htmlFor="username" className="flex items-center gap-2">
                    <AtSign className="h-4 w-4 text-gray-500" />
                    Tên đăng nhập
                  </Label>
                  <Input
                    id="username"
                    name="username"
                    value={formData.username}
                    onChange={handleChange}
                    placeholder="your_username"
                    className={formErrors.username ? "border-red-500" : ""}
                  />
                  {formErrors.username && (
                    <p className="text-sm text-red-500">{formErrors.username}</p>
                  )}
                  <p className="text-xs text-gray-500">
                    Tên đăng nhập của bạn sẽ hiển thị công khai và dùng để đăng nhập.
                  </p>
                </div>
              </CardContent>
              
              <CardFooter className="flex justify-between pt-4">
                <Button 
                  type="button" 
                  variant="outline" 
                  onClick={handleCancel}
                >
                  Hủy
                </Button>
                <Button 
                  type="submit" 
                  disabled={isLoading}
                  className="bg-purple-600 hover:bg-purple-700"
                >
                  {isLoading ? (
                    <>
                      <Loader2 className="mr-2 h-4 w-4 animate-spin" />
                      Đang cập nhật...
                    </>
                  ) : (
                    "Lưu thay đổi"
                  )}
                </Button>
              </CardFooter>
            </form>
          </Card>
        </div>
      </main>
    </div>
  )
} 