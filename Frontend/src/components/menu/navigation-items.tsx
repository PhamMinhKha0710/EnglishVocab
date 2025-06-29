import { Home, BookOpen, BarChart3, Settings, Trophy, User, Edit } from "lucide-react"

export const navigationItems = [
  { href: "/", label: "Trang chủ", icon: Home },
  { href: "/study", label: "Học từ vựng", icon: BookOpen },
  { href: "/progress", label: "Tiến độ", icon: BarChart3 },
  { href: "/achievements", label: "Thành tích", icon: Trophy },
  { href: "/profile", label: "Hồ sơ", icon: User },
  { href: "/settings", label: "Cài đặt", icon: Settings },
  { href: "/edit-profile", label: "Chỉnh sửa hồ sơ", icon: Edit, hideInMenu: true },
] 