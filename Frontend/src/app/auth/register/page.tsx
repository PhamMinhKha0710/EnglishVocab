"use client"

import { useState } from "react"
import { Link, useNavigate } from "react-router-dom"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { Checkbox } from "@/components/ui/checkbox"
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from "@/components/ui/card"
import { BookOpen, UserPlus } from "lucide-react"
import { useAuth } from "@/contexts/auth-context"

export default function RegisterPage() {
  const [firstName, setFirstName] = useState("")
  const [lastName, setLastName] = useState("")
  const [email, setEmail] = useState("")
  const [username, setUsername] = useState("")
  const [password, setPassword] = useState("")
  const [confirmPassword, setConfirmPassword] = useState("")
  const [agreeTerms, setAgreeTerms] = useState(false)
  const [isLoading, setIsLoading] = useState(false)
  const [error, setError] = useState("")
  const navigate = useNavigate()
  const { register } = useAuth()

  const handleRegister = async (e: React.FormEvent) => {
    e.preventDefault()
    setIsLoading(true)
    setError("")

    // Basic validation
    if (password !== confirmPassword) {
      setError("Mật khẩu xác nhận không khớp")
      setIsLoading(false)
      return
    }

    if (!agreeTerms) {
      setError("Bạn cần đồng ý với điều khoản dịch vụ")
      setIsLoading(false)
      return
    }

    try {
      // Gọi hàm register từ AuthContext
      console.log("Đang gọi API đăng ký...")
      const success = await register({
        firstName,
        lastName,
        email,
        username: username || email.split('@')[0], // Nếu không có username, lấy phần trước @ của email
        password,
        confirmPassword
      })
      
      console.log("Kết quả đăng ký:", success)
      
      if (success) {
        console.log("Đăng ký thành công, đang chuyển hướng đến /auth/login")
        // Chuyển hướng đến trang đăng nhập sau khi đăng ký thành công
        navigate("/auth/login")
        console.log("Đã gọi navigate")
        
        // Thêm phương pháp chuyển hướng thay thế sau 500ms nếu navigate không hoạt động
        setTimeout(() => {
          console.log("Sử dụng phương pháp chuyển hướng thay thế")
          window.location.href = "/auth/login"
        }, 500)
      } else {
        setError("Đăng ký không thành công. Vui lòng thử lại sau.")
      }
    } catch (err: any) {
      console.error("Lỗi khi đăng ký:", err)
      setError(err.message || "Đăng ký không thành công. Vui lòng thử lại sau.")
    } finally {
      setIsLoading(false)
    }
  }

  return (
    <div className="min-h-screen flex items-center justify-center bg-gradient-to-br from-purple-50 to-blue-50 p-4">
      <div className="w-full max-w-md">
        <div className="flex justify-center mb-8">
          <div className="flex items-center gap-2">
            <div className="w-10 h-10 bg-gradient-to-br from-purple-500 to-blue-500 rounded-lg flex items-center justify-center">
              <BookOpen className="w-5 h-5 text-white" />
            </div>
            <h1 className="text-2xl font-bold text-gray-800">FlashLearn</h1>
          </div>
        </div>

        <Card className="border-none shadow-lg">
          <CardHeader className="space-y-1">
            <CardTitle className="text-2xl font-bold text-center">Đăng ký tài khoản</CardTitle>
            <CardDescription className="text-center">
              Tạo tài khoản để bắt đầu học từ vựng ngay hôm nay
            </CardDescription>
          </CardHeader>
          <CardContent>
            <form onSubmit={handleRegister} className="space-y-4">
              {error && (
                <div className="p-3 rounded-md bg-red-50 text-red-600 text-sm">
                  {error}
                </div>
              )}
              
              <div className="grid grid-cols-2 gap-4">
                <div className="space-y-2">
                  <Label htmlFor="firstName">Tên</Label>
                  <Input 
                    id="firstName" 
                    placeholder="Nguyễn" 
                    value={firstName}
                    onChange={(e) => setFirstName(e.target.value)}
                    required
                  />
                </div>
                <div className="space-y-2">
                  <Label htmlFor="lastName">Họ</Label>
                  <Input 
                    id="lastName" 
                    placeholder="Văn A" 
                    value={lastName}
                    onChange={(e) => setLastName(e.target.value)}
                    required
                  />
                </div>
              </div>
              
              <div className="space-y-2">
                <Label htmlFor="email">Email</Label>
                <Input 
                  id="email" 
                  type="email" 
                  placeholder="name@example.com" 
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  required
                />
              </div>
              
              <div className="space-y-2">
                <Label htmlFor="username">Tên đăng nhập</Label>
                <Input 
                  id="username" 
                  placeholder="username" 
                  value={username}
                  onChange={(e) => setUsername(e.target.value)}
                />
                <p className="text-xs text-gray-500">
                  Không bắt buộc. Nếu bỏ trống, hệ thống sẽ sử dụng phần trước @ của email
                </p>
              </div>
              
              <div className="space-y-2">
                <Label htmlFor="password">Mật khẩu</Label>
                <Input 
                  id="password" 
                  type="password" 
                  placeholder="••••••••" 
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  required
                />
                <p className="text-xs text-gray-500">
                  Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường và số
                </p>
              </div>
              
              <div className="space-y-2">
                <Label htmlFor="confirmPassword">Xác nhận mật khẩu</Label>
                <Input 
                  id="confirmPassword" 
                  type="password" 
                  placeholder="••••••••" 
                  value={confirmPassword}
                  onChange={(e) => setConfirmPassword(e.target.value)}
                  required
                />
              </div>
              
              <div className="flex items-start space-x-2">
                <Checkbox 
                  id="terms" 
                  checked={agreeTerms}
                  onCheckedChange={(checked) => setAgreeTerms(checked as boolean)}
                />
                <Label htmlFor="terms" className="text-sm font-normal leading-tight">
                  Tôi đồng ý với{" "}
                  <Link to="/terms" className="text-purple-600 hover:text-purple-800 font-medium">
                    Điều khoản dịch vụ
                  </Link>
                  {" "}và{" "}
                  <Link to="/privacy" className="text-purple-600 hover:text-purple-800 font-medium">
                    Chính sách bảo mật
                  </Link>
                </Label>
              </div>
              
              <Button type="submit" className="w-full bg-purple-600 hover:bg-purple-700" disabled={isLoading}>
                {isLoading ? (
                  <div className="flex items-center justify-center">
                    <div className="w-4 h-4 border-2 border-white border-t-transparent rounded-full animate-spin mr-2"></div>
                    Đang đăng ký...
                  </div>
                ) : (
                  <div className="flex items-center justify-center">
                    <UserPlus className="w-4 h-4 mr-2" />
                    Đăng ký
                  </div>
                )}
              </Button>
            </form>
          </CardContent>
          <CardFooter className="flex flex-col space-y-4">
            <div className="text-center text-sm text-gray-600">
              Đã có tài khoản?{" "}
              <Link to="/auth/login" className="text-purple-600 hover:text-purple-800 font-medium">
                Đăng nhập
              </Link>
            </div>
          </CardFooter>
        </Card>
      </div>
    </div>
  )
} 