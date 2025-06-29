"use client";

import { useState } from "react"
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { Button } from "@/components/ui/button"
import { DropdownMenu, DropdownMenuContent, DropdownMenuGroup, DropdownMenuItem, DropdownMenuLabel, DropdownMenuSeparator, DropdownMenuTrigger } from "@/components/ui/dropdown-menu"
import { useAuth } from "@/contexts/auth-context"
import { Settings, LogOut, UserCircle, Edit } from "lucide-react"
import { Link, useNavigate } from "react-router-dom"

export function UserProfileButton() {
  const { user, logout, isAuthenticated } = useAuth()
  const navigate = useNavigate()
  const [open, setOpen] = useState(false)

  if (!isAuthenticated || !user) {
    return null; // Không hiển thị gì nếu chưa đăng nhập - các nút đăng nhập/đăng ký đã được xử lý ở AppHeader
  }

  const getInitials = () => {
    if (!user) return "U";
    const firstName = user.firstName || "";
    const lastName = user.lastName || "";
    return (firstName.charAt(0) + (lastName.charAt(0) || "")).toUpperCase();
  };

  const handleLogout = () => {
    logout();
    navigate("/auth/login");
  };

  return (
    <DropdownMenu open={open} onOpenChange={setOpen}>
      <DropdownMenuTrigger asChild>
        <Button variant="ghost" className="relative h-10 w-10 rounded-full">
          <Avatar className="h-10 w-10">
            <AvatarImage src="/placeholder-user.jpg" />
            <AvatarFallback className="bg-purple-100 text-purple-700">{getInitials()}</AvatarFallback>
          </Avatar>
        </Button>
      </DropdownMenuTrigger>
      <DropdownMenuContent className="w-56" align="end" forceMount>
        <DropdownMenuLabel className="font-normal">
          <div className="flex flex-col space-y-1">
            <p className="text-sm font-medium leading-none">
              {user.firstName} {user.lastName}
            </p>
            <p className="text-xs leading-none text-muted-foreground">
              {user.email}
            </p>
          </div>
        </DropdownMenuLabel>
        <DropdownMenuSeparator />
        <DropdownMenuGroup>
          <DropdownMenuItem onClick={() => { setOpen(false); navigate("/profile"); }}>
            <UserCircle className="mr-2 h-4 w-4" />
            <span>Hồ sơ</span>
          </DropdownMenuItem>
          <DropdownMenuItem onClick={() => { setOpen(false); navigate("/edit-profile"); }}>
            <Edit className="mr-2 h-4 w-4" />
            <span>Chỉnh sửa hồ sơ</span>
          </DropdownMenuItem>
          <DropdownMenuItem onClick={() => { setOpen(false); navigate("/settings"); }}>
            <Settings className="mr-2 h-4 w-4" />
            <span>Cài đặt ứng dụng</span>
          </DropdownMenuItem>
        </DropdownMenuGroup>
        <DropdownMenuSeparator />
        <DropdownMenuItem onClick={handleLogout}>
          <LogOut className="mr-2 h-4 w-4" />
          <span>Đăng xuất</span>
        </DropdownMenuItem>
      </DropdownMenuContent>
    </DropdownMenu>
  )
} 