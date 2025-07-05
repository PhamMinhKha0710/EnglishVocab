"use client"

import { useState } from "react"
import { useNavigate } from "react-router-dom"
import { useAuth } from "@/contexts/auth-context"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from "@/components/ui/card"
import { Switch } from "@/components/ui/switch"
import { Label } from "@/components/ui/label"
import { Separator } from "@/components/ui/separator"
import { toast } from "@/components/ui/use-toast"
import { Toaster } from "@/components/ui/toaster"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"
import {
  Volume2,
  Bell,
  Moon,
  Sun,
  Laptop,
  Globe,
  Database,
  Download,
  Trash2,
  ArrowLeft,
  Palette,
  Languages,
  BookOpen
} from "lucide-react"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import { Slider } from "@/components/ui/slider"

export default function SettingsPage() {
  const { isAuthenticated } = useAuth()
  const navigate = useNavigate()
  const [isClient, setIsClient] = useState(true)

  // Cài đặt giao diện
  const [darkMode, setDarkMode] = useState<'light' | 'dark' | 'system'>('system')
  const [colorTheme, setColorTheme] = useState('purple')

  // Cài đặt âm thanh
  const [soundEnabled, setSoundEnabled] = useState(true)
  const [volume, setVolume] = useState(80)

  // Cài đặt thông báo
  const [notificationsEnabled, setNotificationsEnabled] = useState(true)
  const [reminderNotifications, setReminderNotifications] = useState(true)
  const [achievementNotifications, setAchievementNotifications] = useState(true)

  // Cài đặt học tập
  const [dailyGoal, setDailyGoal] = useState(10)
  const [autoPlayAudio, setAutoPlayAudio] = useState(true)
  const [showTranslation, setShowTranslation] = useState(true)

  // Cài đặt dữ liệu
  const [offlineMode, setOfflineMode] = useState(false)
  const [autoSync, setAutoSync] = useState(true)

  // Xử lý lưu cài đặt
  const handleSaveSettings = () => {
    // Lưu cài đặt vào localStorage hoặc gửi lên server
    localStorage.setItem('settings', JSON.stringify({
      darkMode,
      colorTheme,
      soundEnabled,
      volume,
      notificationsEnabled,
      reminderNotifications,
      achievementNotifications,
      dailyGoal,
      autoPlayAudio,
      showTranslation,
      offlineMode,
      autoSync
    }))

    toast({
      title: "Cài đặt đã được lưu",
      description: "Các tùy chọn của bạn đã được cập nhật",
      variant: "default",
    })
  }

  // Khôi phục cài đặt mặc định
  const handleResetSettings = () => {
    setDarkMode('system')
    setColorTheme('purple')
    setSoundEnabled(true)
    setVolume(80)
    setNotificationsEnabled(true)
    setReminderNotifications(true)
    setAchievementNotifications(true)
    setDailyGoal(10)
    setAutoPlayAudio(true)
    setShowTranslation(true)
    setOfflineMode(false)
    setAutoSync(true)

    toast({
      title: "Đã khôi phục cài đặt mặc định",
      description: "Tất cả tùy chọn đã được đặt về giá trị mặc định",
      variant: "default",
    })
  }

  // Quay lại trang trước
  const handleBack = () => {
    navigate(-1)
  }

  if (!isClient || !isAuthenticated) {
    return null
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
                onClick={handleBack}
                className="rounded-full"
              >
                <ArrowLeft className="h-5 w-5" />
              </Button>
            <div>
                <h1 className="text-3xl font-bold text-gray-800">Cài đặt ứng dụng</h1>
                <p className="text-gray-600 mt-1">Tùy chỉnh trải nghiệm học tập của bạn</p>
              </div>
            </div>
          </div>
        </div>

        <div className="max-w-4xl mx-auto">
          <Tabs defaultValue="appearance">
            <div className="flex justify-center mb-6">
              <TabsList className="grid grid-cols-4 w-full max-w-2xl">
                <TabsTrigger value="appearance" className="flex gap-2 items-center">
                  <Palette className="h-4 w-4" />
                  <span className="hidden sm:inline">Giao diện</span>
                </TabsTrigger>
                <TabsTrigger value="sound" className="flex gap-2 items-center">
                  <Volume2 className="h-4 w-4" />
                  <span className="hidden sm:inline">Âm thanh</span>
            </TabsTrigger>
                <TabsTrigger value="learning" className="flex gap-2 items-center">
                  <BookOpen className="h-4 w-4" />
                  <span className="hidden sm:inline">Học tập</span>
            </TabsTrigger>
                <TabsTrigger value="data" className="flex gap-2 items-center">
                  <Database className="h-4 w-4" />
                  <span className="hidden sm:inline">Dữ liệu</span>
            </TabsTrigger>
          </TabsList>
            </div>

            {/* Cài đặt giao diện */}
            <TabsContent value="appearance">
              <Card>
                <CardHeader>
                  <CardTitle>Giao diện</CardTitle>
                  <CardDescription>
                    Tùy chỉnh giao diện và chủ đề ứng dụng
                  </CardDescription>
                </CardHeader>
                <CardContent className="space-y-6">
                  {/* Chế độ sáng/tối */}
                  <div className="space-y-4">
                    <h3 className="text-lg font-medium">Chế độ hiển thị</h3>
                    <div className="grid grid-cols-3 gap-4">
                      <Button 
                        variant={darkMode === 'light' ? "default" : "outline"}
                        className="flex flex-col items-center justify-center h-24 gap-2"
                        onClick={() => setDarkMode('light')}
                      >
                        <Sun className="h-8 w-8" />
                        <span>Sáng</span>
                      </Button>
                      <Button 
                        variant={darkMode === 'dark' ? "default" : "outline"}
                        className="flex flex-col items-center justify-center h-24 gap-2"
                        onClick={() => setDarkMode('dark')}
                      >
                        <Moon className="h-8 w-8" />
                        <span>Tối</span>
                      </Button>
                      <Button 
                        variant={darkMode === 'system' ? "default" : "outline"}
                        className="flex flex-col items-center justify-center h-24 gap-2"
                        onClick={() => setDarkMode('system')}
                      >
                        <Laptop className="h-8 w-8" />
                        <span>Hệ thống</span>
                      </Button>
                    </div>
                  </div>
                  
                  <Separator />
                  
                  {/* Màu chủ đề */}
                  <div className="space-y-4">
                    <h3 className="text-lg font-medium">Màu chủ đề</h3>
                    <div className="grid grid-cols-4 gap-4">
                      <Button 
                        variant={colorTheme === 'purple' ? "default" : "outline"}
                        className="bg-purple-600 hover:bg-purple-700 text-white"
                        onClick={() => setColorTheme('purple')}
                      >
                        Tím
                      </Button>
                      <Button 
                        variant={colorTheme === 'blue' ? "default" : "outline"}
                        className="bg-blue-600 hover:bg-blue-700 text-white"
                        onClick={() => setColorTheme('blue')}
                      >
                        Xanh dương
                      </Button>
                      <Button 
                        variant={colorTheme === 'green' ? "default" : "outline"}
                        className="bg-green-600 hover:bg-green-700 text-white"
                        onClick={() => setColorTheme('green')}
                      >
                        Xanh lá
                      </Button>
                      <Button 
                        variant={colorTheme === 'orange' ? "default" : "outline"}
                        className="bg-orange-600 hover:bg-orange-700 text-white"
                        onClick={() => setColorTheme('orange')}
                      >
                        Cam
                      </Button>
                    </div>
                  </div>
                  
                  <Separator />
                  
                  {/* Ngôn ngữ */}
                  <div className="space-y-2">
                    <Label htmlFor="language" className="flex items-center gap-2">
                      <Globe className="h-4 w-4 text-gray-500" />
                      Ngôn ngữ
                    </Label>
                    <Select defaultValue="vi">
                      <SelectTrigger id="language">
                        <SelectValue placeholder="Chọn ngôn ngữ" />
                      </SelectTrigger>
                      <SelectContent>
                        <SelectItem value="vi">Tiếng Việt</SelectItem>
                        <SelectItem value="en">English</SelectItem>
                      </SelectContent>
                    </Select>
                  </div>
                </CardContent>
              </Card>
          </TabsContent>

            {/* Cài đặt âm thanh */}
            <TabsContent value="sound">
              <Card>
                <CardHeader>
                  <CardTitle>Âm thanh & Thông báo</CardTitle>
                  <CardDescription>
                    Quản lý âm thanh và thông báo trong ứng dụng
                  </CardDescription>
                </CardHeader>
                <CardContent className="space-y-6">
                  {/* Âm thanh */}
                  <div className="space-y-4">
                    <h3 className="text-lg font-medium">Âm thanh</h3>
                    
                    <div className="flex items-center justify-between">
                      <Label htmlFor="sound-enabled" className="flex items-center gap-2 cursor-pointer">
                        <Volume2 className="h-4 w-4 text-gray-500" />
                        Bật âm thanh
                      </Label>
                      <Switch 
                        id="sound-enabled" 
                        checked={soundEnabled}
                        onCheckedChange={setSoundEnabled}
                      />
                    </div>
                    
                  <div className="space-y-2">
                      <div className="flex justify-between">
                        <Label htmlFor="volume">Âm lượng</Label>
                        <span>{volume}%</span>
                      </div>
                      <Slider
                        id="volume"
                        disabled={!soundEnabled}
                        value={[volume]}
                        min={0}
                        max={100}
                        step={1}
                        onValueChange={(value) => setVolume(value[0])}
                        className={!soundEnabled ? "opacity-50" : ""}
                      />
                    </div>
                  </div>
                  
                  <Separator />
                  
                  {/* Thông báo */}
                  <div className="space-y-4">
                    <h3 className="text-lg font-medium">Thông báo</h3>
                    
                    <div className="flex items-center justify-between">
                      <Label htmlFor="notifications-enabled" className="flex items-center gap-2 cursor-pointer">
                        <Bell className="h-4 w-4 text-gray-500" />
                        Bật thông báo
                      </Label>
                      <Switch 
                        id="notifications-enabled" 
                        checked={notificationsEnabled}
                        onCheckedChange={setNotificationsEnabled}
                      />
                    </div>
                    
                    <div className="flex items-center justify-between pl-6">
                      <Label htmlFor="reminder-notifications" className="cursor-pointer">
                        Nhắc nhở học tập hàng ngày
                      </Label>
                      <Switch 
                        id="reminder-notifications" 
                        checked={reminderNotifications && notificationsEnabled}
                        onCheckedChange={setReminderNotifications}
                        disabled={!notificationsEnabled}
                      />
                    </div>
                    
                    <div className="flex items-center justify-between pl-6">
                      <Label htmlFor="achievement-notifications" className="cursor-pointer">
                        Thông báo thành tựu
                      </Label>
                      <Switch 
                        id="achievement-notifications" 
                        checked={achievementNotifications && notificationsEnabled}
                        onCheckedChange={setAchievementNotifications}
                        disabled={!notificationsEnabled}
                      />
                  </div>
                  </div>
                </CardContent>
              </Card>
            </TabsContent>

            {/* Cài đặt học tập */}
            <TabsContent value="learning">
              <Card>
                <CardHeader>
                  <CardTitle>Học tập</CardTitle>
                  <CardDescription>
                    Tùy chỉnh trải nghiệm học tập của bạn
                  </CardDescription>
                </CardHeader>
                <CardContent className="space-y-6">
                  {/* Mục tiêu hàng ngày */}
                  <div className="space-y-2">
                    <div className="flex justify-between">
                      <Label htmlFor="daily-goal">Mục tiêu từ vựng hàng ngày</Label>
                      <span>{dailyGoal} từ</span>
                    </div>
                    <Slider
                      id="daily-goal"
                      value={[dailyGoal]}
                      min={5}
                      max={50}
                      step={5}
                      onValueChange={(value) => setDailyGoal(value[0])}
                    />
                  </div>
                  
                  <Separator />
                  
                  {/* Tùy chọn học tập */}
                  <div className="space-y-4">
                    <h3 className="text-lg font-medium">Tùy chọn học tập</h3>
                    
                    <div className="flex items-center justify-between">
                      <Label htmlFor="auto-play-audio" className="flex items-center gap-2 cursor-pointer">
                        <Volume2 className="h-4 w-4 text-gray-500" />
                        Tự động phát âm thanh
                      </Label>
                      <Switch 
                        id="auto-play-audio" 
                        checked={autoPlayAudio}
                        onCheckedChange={setAutoPlayAudio}
                      />
                    </div>
                    
                    <div className="flex items-center justify-between">
                      <Label htmlFor="show-translation" className="flex items-center gap-2 cursor-pointer">
                        <Languages className="h-4 w-4 text-gray-500" />
                        Hiển thị bản dịch
                      </Label>
                      <Switch 
                        id="show-translation" 
                        checked={showTranslation}
                        onCheckedChange={setShowTranslation}
                      />
                  </div>
                  </div>
                </CardContent>
              </Card>
          </TabsContent>

            {/* Cài đặt dữ liệu */}
            <TabsContent value="data">
              <Card>
                <CardHeader>
                  <CardTitle>Dữ liệu & Đồng bộ hóa</CardTitle>
                  <CardDescription>
                    Quản lý dữ liệu và tùy chọn đồng bộ hóa
                  </CardDescription>
                </CardHeader>
                <CardContent className="space-y-6">
                  {/* Chế độ offline */}
                  <div className="flex items-center justify-between">
                    <div>
                      <Label htmlFor="offline-mode" className="flex items-center gap-2 cursor-pointer">
                        <Globe className="h-4 w-4 text-gray-500" />
                        Chế độ ngoại tuyến
                      </Label>
                      <p className="text-sm text-gray-500 mt-1">Sử dụng ứng dụng khi không có kết nối internet</p>
                    </div>
                    <Switch 
                      id="offline-mode" 
                      checked={offlineMode}
                      onCheckedChange={setOfflineMode}
                    />
                  </div>

                  <div className="flex items-center justify-between">
                    <div>
                      <Label htmlFor="auto-sync" className="flex items-center gap-2 cursor-pointer">
                        <Database className="h-4 w-4 text-gray-500" />
                        Tự động đồng bộ
                      </Label>
                      <p className="text-sm text-gray-500 mt-1">Đồng bộ hóa dữ liệu khi có kết nối internet</p>
                    </div>
                    <Switch 
                      id="auto-sync" 
                      checked={autoSync}
                      onCheckedChange={setAutoSync}
                      disabled={!offlineMode}
                    />
                  </div>
                  
                  <Separator />
                  
                  {/* Quản lý dữ liệu */}
                  <div className="space-y-4">
                    <h3 className="text-lg font-medium">Quản lý dữ liệu</h3>
                    
                    <div className="flex justify-between items-center">
                      <div>
                        <p className="font-medium">Tải xuống dữ liệu</p>
                        <p className="text-sm text-gray-500">Tải xuống toàn bộ dữ liệu học tập của bạn</p>
                      </div>
                      <Button variant="outline" className="flex gap-2 items-center">
                        <Download className="h-4 w-4" />
                        <span>Tải xuống</span>
                      </Button>
                    </div>
                    
                    <div className="flex justify-between items-center">
                      <div>
                        <p className="font-medium text-red-600">Xóa dữ liệu</p>
                        <p className="text-sm text-gray-500">Xóa toàn bộ dữ liệu học tập của bạn</p>
                      </div>
                      <Button variant="destructive" className="flex gap-2 items-center">
                        <Trash2 className="h-4 w-4" />
                        <span>Xóa dữ liệu</span>
                      </Button>
                    </div>
                  </div>
                </CardContent>
              </Card>
          </TabsContent>
        </Tabs>
          
          <div className="flex justify-between mt-6">
            <Button 
              variant="outline" 
              onClick={handleResetSettings}
            >
              Khôi phục mặc định
            </Button>
            <Button 
              onClick={handleSaveSettings}
              className="bg-purple-600 hover:bg-purple-700"
            >
              Lưu cài đặt
            </Button>
          </div>
        </div>
      </main>
    </div>
  )
} 