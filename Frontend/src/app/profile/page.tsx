"use client"

import { useEffect, useState } from "react"
import { Card, CardContent, CardHeader, CardTitle, CardDescription } from "@/components/ui/card"
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { useAuth } from "@/contexts/auth-context"
import { 
  LogOut, 
  Edit2, 
  Shield, 
  Mail, 
  Settings, 
  BookOpen, 
  Calendar, 
  Award,
  Bookmark,
  Clock,
  TrendingUp
} from "lucide-react"
import { useNavigate } from "react-router-dom"
import { Separator } from "@/components/ui/separator"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"
import { Progress } from "@/components/ui/progress"
import { Button } from "@/components/ui/button"

export default function ProfilePage() {
  const { user, logout, isAuthenticated, refreshUserData } = useAuth()
  const navigate = useNavigate()
  const [isClient, setIsClient] = useState(false)

  useEffect(() => {
    setIsClient(true)
    // Nếu không đăng nhập, chuyển hướng đến trang đăng nhập
    if (!isAuthenticated && isClient) {
      navigate("/auth/login")
      return
    }
    
    // Làm mới dữ liệu người dùng khi trang được tải
    if (isAuthenticated) {
      refreshUserData()
    }
  }, [isAuthenticated, navigate, isClient, refreshUserData])

  // Tạo chữ cái đầu từ tên người dùng cho avatar fallback
  const getInitials = () => {
    if (!user) return "U"
    const firstName = user.firstName || ""
    const lastName = user.lastName || ""
    return (firstName.charAt(0) + (lastName.charAt(0) || "")).toUpperCase()
  }

  const handleLogout = () => {
    logout()
    navigate("/auth/login")
  }

  const handleEditProfile = () => {
    navigate("/edit-profile")
  }

  const handleSettings = () => {
    navigate("/settings")
  }

  if (!isClient || !isAuthenticated) {
    return null // Không hiển thị gì nếu đang kiểm tra xác thực hoặc chưa đăng nhập
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-purple-50 to-blue-50 pb-8">
      <main className="p-6">
        {/* Header */}
        <div className="mb-8">
          <div className="flex flex-col md:flex-row md:items-center md:justify-between gap-4">
            <div>
              <h1 className="text-3xl font-bold text-gray-800">Hồ sơ cá nhân</h1>
              <p className="text-gray-600 mt-1">Quản lý thông tin và theo dõi tiến trình học tập</p>
            </div>
            <div className="flex gap-3">
              <Button 
                variant="outline" 
                className="flex gap-2 items-center"
                onClick={handleSettings}
              >
                <Settings className="h-4 w-4" />
                <span>Cài đặt ứng dụng</span>
              </Button>
              <Button 
                variant="destructive" 
                className="flex gap-2 items-center"
                onClick={handleLogout}
              >
                <LogOut className="h-4 w-4" />
                <span>Đăng xuất</span>
              </Button>
            </div>
          </div>
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
          {/* Profile Card - Left Column */}
          <Card className="lg:col-span-1">
            <CardHeader className="text-center pb-0">
              <div className="flex flex-col items-center">
                <Avatar className="h-24 w-24 border-4 border-white shadow-lg mb-4">
                  <AvatarImage src="/placeholder-user.jpg" alt={user?.username || "User"} />
                  <AvatarFallback className="text-2xl bg-purple-100 text-purple-700">
                    {getInitials()}
                  </AvatarFallback>
                </Avatar>
                  <CardTitle className="text-2xl">{user?.firstName} {user?.lastName}</CardTitle>
                <p className="text-gray-500 mt-1">@{user?.username}</p>
                
                {user?.roles && user.roles.length > 0 && (
                  <div className="flex flex-wrap justify-center gap-2 mt-2">
                    {user.roles.map((role, index) => (
                      <span key={index} className="bg-purple-100 text-purple-700 text-xs py-1 px-3 rounded-full flex items-center gap-1">
                        <Shield className="h-3 w-3" />
                        {role}
                      </span>
                    ))}
                </div>
                )}
              </div>
            </CardHeader>
            
            <CardContent className="pt-6">
              <div className="space-y-6">
                <div className="flex items-center gap-3 bg-blue-50 p-3 rounded-lg">
                  <Mail className="h-5 w-5 text-blue-500" />
                  <span className="text-gray-800 font-medium">{user?.email}</span>
                </div>

                <Separator />
                
                <div className="space-y-3">
                  <h3 className="font-medium text-gray-700 flex items-center gap-2">
                    <Award className="h-4 w-4 text-yellow-500" />
                    Thành tựu
                  </h3>
                  <div className="grid grid-cols-2 gap-2">
                    <div className="bg-green-50 p-3 rounded-lg text-center">
                      <p className="text-xl font-bold text-green-600">0</p>
                      <p className="text-xs text-gray-600">Từ đã học</p>
                    </div>
                    <div className="bg-orange-50 p-3 rounded-lg text-center">
                      <p className="text-xl font-bold text-orange-600">0</p>
                      <p className="text-xs text-gray-600">Ngày liên tiếp</p>
                    </div>
                  </div>
                </div>
                
                  <Button 
                    variant="outline" 
                  className="w-full flex gap-2 items-center justify-center"
                  onClick={handleEditProfile}
                  >
                    <Edit2 className="h-4 w-4" />
                    <span>Chỉnh sửa hồ sơ</span>
                  </Button>
              </div>
            </CardContent>
          </Card>

          {/* Main Content - Right Column */}
          <div className="lg:col-span-2 space-y-6">
            {/* Learning Progress */}
            <Card>
              <CardHeader>
                <CardTitle className="flex items-center gap-2">
                  <TrendingUp className="h-5 w-5 text-purple-500" />
                  Tiến trình học tập
                </CardTitle>
                <CardDescription>
                  Theo dõi quá trình học từ vựng của bạn
                </CardDescription>
              </CardHeader>
              <CardContent>
                <Tabs defaultValue="overview">
                  <TabsList className="mb-4">
                    <TabsTrigger value="overview">Tổng quan</TabsTrigger>
                    <TabsTrigger value="weekly">Tuần này</TabsTrigger>
                    <TabsTrigger value="monthly">Tháng này</TabsTrigger>
                  </TabsList>
                  
                  <TabsContent value="overview" className="space-y-4">
                    <div className="space-y-2">
                      <div className="flex justify-between text-sm">
                        <span className="text-gray-600">Tiến độ mục tiêu hàng ngày</span>
                        <span className="font-medium">0/10 từ</span>
                      </div>
                      <Progress value={0} className="h-2" />
                    </div>
                    
                    <div className="grid grid-cols-1 md:grid-cols-3 gap-4 mt-4">
                      <div className="flex items-center gap-3 bg-blue-50 p-4 rounded-lg">
                        <BookOpen className="h-8 w-8 text-blue-500" />
                        <div>
                          <p className="text-2xl font-bold text-blue-600">0</p>
                          <p className="text-xs text-gray-600">Từ vựng đã học</p>
                        </div>
                      </div>
                      
                      <div className="flex items-center gap-3 bg-green-50 p-4 rounded-lg">
                        <Calendar className="h-8 w-8 text-green-500" />
                        <div>
                          <p className="text-2xl font-bold text-green-600">0</p>
                          <p className="text-xs text-gray-600">Ngày học liên tiếp</p>
                        </div>
                      </div>
                      
                      <div className="flex items-center gap-3 bg-purple-50 p-4 rounded-lg">
                        <Clock className="h-8 w-8 text-purple-500" />
                        <div>
                          <p className="text-2xl font-bold text-purple-600">0</p>
                          <p className="text-xs text-gray-600">Phút học tập</p>
                        </div>
                      </div>
                    </div>
                  </TabsContent>
                  
                  <TabsContent value="weekly">
                    <div className="h-40 flex items-center justify-center text-gray-500">
                      Chưa có dữ liệu học tập trong tuần này
                    </div>
                  </TabsContent>
                  
                  <TabsContent value="monthly">
                    <div className="h-40 flex items-center justify-center text-gray-500">
                      Chưa có dữ liệu học tập trong tháng này
                    </div>
                  </TabsContent>
                </Tabs>
              </CardContent>
            </Card>
            
            {/* Recent Activity & Word Sets */}
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              {/* Recent Activity */}
          <Card>
            <CardHeader>
                  <CardTitle className="flex items-center gap-2">
                    <Clock className="h-5 w-5 text-blue-500" />
                    Hoạt động gần đây
                  </CardTitle>
            </CardHeader>
            <CardContent>
                  <div className="space-y-4">
                    <div className="flex items-center justify-center h-32 text-gray-500 text-sm">
                      Chưa có hoạt động nào được ghi nhận
                </div>
                </div>
                </CardContent>
              </Card>
              
              {/* Word Sets */}
              <Card>
                <CardHeader>
                  <CardTitle className="flex items-center gap-2">
                    <Bookmark className="h-5 w-5 text-green-500" />
                    Bộ từ vựng
                  </CardTitle>
                </CardHeader>
                <CardContent>
                  <div className="space-y-4">
                    <div className="flex items-center justify-center h-32 text-gray-500 text-sm">
                      Chưa có bộ từ vựng nào được tạo
                </div>
              </div>
            </CardContent>
          </Card>
            </div>
          </div>
        </div>
      </main>
    </div>
  )
} 