"use client"

import { useState } from "react"
import { Navigation } from "@/components/navigation"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { Switch } from "@/components/ui/switch"
import { Slider } from "@/components/ui/slider"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"
import { Settings, User, Moon, Bell, Volume2 } from "lucide-react"
import Link from "next/link"

export default function SettingsPage() {
  const [notifications, setNotifications] = useState(true)
  const [sound, setSound] = useState(true)
  const [dailyGoal, setDailyGoal] = useState(10)
  
  return (
    <div className="min-h-screen bg-gradient-to-br from-purple-50 to-blue-50">
      <Navigation />

      <main className="md:ml-64 p-6">
        {/* Header */}
        <div className="mb-8">
          <div className="flex flex-col md:flex-row md:items-center md:justify-between gap-4">
            <div>
              <h1 className="text-3xl font-bold text-gray-800">Cài đặt</h1>
              <p className="text-gray-600 mt-1">Quản lý thông tin cá nhân và tùy chỉnh ứng dụng</p>
            </div>
          </div>
        </div>

        <Tabs defaultValue="general" className="space-y-6">
          <TabsList className="grid grid-cols-3 w-full max-w-md">
            <TabsTrigger value="general" className="flex items-center gap-2">
              <Settings className="w-4 h-4" />
              <span>Chung</span>
            </TabsTrigger>
            <TabsTrigger value="profile" className="flex items-center gap-2">
              <User className="w-4 h-4" />
              <span>Hồ sơ</span>
            </TabsTrigger>
            <TabsTrigger value="appearance" className="flex items-center gap-2">
              <Moon className="w-4 h-4" />
              <span>Giao diện</span>
            </TabsTrigger>
          </TabsList>

          <TabsContent value="general">
            <div className="space-y-6 max-w-2xl">
              <Card>
                <CardHeader>
                  <CardTitle>Thông báo</CardTitle>
                </CardHeader>
                <CardContent className="space-y-4">
                  <div className="flex items-center justify-between">
                    <div className="space-y-1">
                      <Label htmlFor="notifications">Nhận thông báo</Label>
                      <div className="text-sm text-gray-500">
                        Nhận thông báo về giờ học và thành tích
                      </div>
                    </div>
                    <Switch
                      id="notifications"
                      checked={notifications}
                      onCheckedChange={setNotifications}
                    />
                  </div>
                  <div className="flex items-center justify-between">
                    <div className="space-y-1">
                      <Label htmlFor="sound">Âm thanh</Label>
                      <div className="text-sm text-gray-500">
                        Bật âm thanh khi sử dụng ứng dụng
                      </div>
                    </div>
                    <Switch
                      id="sound"
                      checked={sound}
                      onCheckedChange={setSound}
                    />
                  </div>
                </CardContent>
              </Card>

              <Card>
                <CardHeader>
                  <CardTitle>Mục tiêu học tập</CardTitle>
                </CardHeader>
                <CardContent className="space-y-4">
                  <div className="space-y-2">
                    <div className="flex justify-between">
                      <Label htmlFor="daily-goal">Mục tiêu hàng ngày</Label>
                      <span>{dailyGoal} từ</span>
                    </div>
                    <Slider
                      id="daily-goal"
                      value={[dailyGoal]}
                      min={5}
                      max={50}
                      step={5}
                      onValueChange={(value) => setDailyGoal(value[0])}
                      className="py-4"
                    />
                    <div className="flex justify-between text-xs text-gray-500">
                      <span>5</span>
                      <span>25</span>
                      <span>50</span>
                    </div>
                  </div>
                </CardContent>
              </Card>

              <Card>
                <CardHeader>
                  <CardTitle>Ngôn ngữ</CardTitle>
                </CardHeader>
                <CardContent className="space-y-4">
                  <div className="space-y-2">
                    <Label htmlFor="language">Ngôn ngữ hiển thị</Label>
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
            </div>
          </TabsContent>

          <TabsContent value="profile">
            <div className="space-y-6 max-w-2xl">
              <Card>
                <CardHeader>
                  <CardTitle>Thông tin cá nhân</CardTitle>
                </CardHeader>
                <CardContent className="space-y-4">
                  <div className="space-y-2">
                    <Label htmlFor="name">Họ tên</Label>
                    <Input id="name" placeholder="Nhập họ tên" defaultValue="Nguyễn Văn A" />
                  </div>
                  <div className="space-y-2">
                    <Label htmlFor="email">Email</Label>
                    <Input id="email" type="email" placeholder="Nhập email" defaultValue="example@gmail.com" />
                  </div>
                  <div className="space-y-2">
                    <Label htmlFor="level">Trình độ</Label>
                    <Select defaultValue="intermediate">
                      <SelectTrigger id="level">
                        <SelectValue placeholder="Chọn trình độ" />
                      </SelectTrigger>
                      <SelectContent>
                        <SelectItem value="beginner">Cơ bản</SelectItem>
                        <SelectItem value="intermediate">Trung cấp</SelectItem>
                        <SelectItem value="advanced">Nâng cao</SelectItem>
                      </SelectContent>
                    </Select>
                  </div>
                </CardContent>
              </Card>

              <Card>
                <CardHeader>
                  <CardTitle>Bảo mật</CardTitle>
                </CardHeader>
                <CardContent className="space-y-4">
                  <div className="space-y-2">
                    <Label htmlFor="current-password">Mật khẩu hiện tại</Label>
                    <Input id="current-password" type="password" placeholder="••••••••" />
                  </div>
                  <div className="space-y-2">
                    <Label htmlFor="new-password">Mật khẩu mới</Label>
                    <Input id="new-password" type="password" placeholder="••••••••" />
                  </div>
                  <div className="space-y-2">
                    <Label htmlFor="confirm-password">Xác nhận mật khẩu</Label>
                    <Input id="confirm-password" type="password" placeholder="••••••••" />
                  </div>
                  <Button className="bg-purple-600 hover:bg-purple-700 mt-2">
                    Cập nhật mật khẩu
                  </Button>
                </CardContent>
              </Card>
            </div>
          </TabsContent>

          <TabsContent value="appearance">
            <div className="space-y-6 max-w-2xl">
              <Card>
                <CardHeader>
                  <CardTitle>Giao diện</CardTitle>
                </CardHeader>
                <CardContent className="space-y-4">
                  <div className="space-y-2">
                    <Label htmlFor="theme">Chủ đề</Label>
                    <Select defaultValue="light">
                      <SelectTrigger id="theme">
                        <SelectValue placeholder="Chọn chủ đề" />
                      </SelectTrigger>
                      <SelectContent>
                        <SelectItem value="light">Sáng</SelectItem>
                        <SelectItem value="dark">Tối</SelectItem>
                        <SelectItem value="system">Theo hệ thống</SelectItem>
                      </SelectContent>
                    </Select>
                  </div>
                </CardContent>
              </Card>

              <Card>
                <CardHeader>
                  <CardTitle>Hiệu ứng</CardTitle>
                </CardHeader>
                <CardContent className="space-y-4">
                  <div className="flex items-center justify-between">
                    <div className="space-y-1">
                      <Label htmlFor="animations">Hiệu ứng chuyển động</Label>
                      <div className="text-sm text-gray-500">
                        Bật hiệu ứng chuyển động trong ứng dụng
                      </div>
                    </div>
                    <Switch id="animations" defaultChecked />
                  </div>
                </CardContent>
              </Card>
            </div>
          </TabsContent>
        </Tabs>
      </main>
    </div>
  )
} 