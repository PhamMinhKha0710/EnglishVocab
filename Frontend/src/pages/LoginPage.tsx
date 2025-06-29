import { useState, useEffect } from "react"
import { Link, useLocation, useNavigate } from "react-router-dom"
import { useAuth } from "@/contexts/auth-context"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from "@/components/ui/card"
import { Label } from "@/components/ui/label"
import { Alert, AlertDescription } from "@/components/ui/alert"
import { AlertCircle, Loader2 } from "lucide-react"

interface LocationState {
  message?: string;
}

export function LoginPage() {
  const { login, error: authError, isLoading } = useAuth()
  const navigate = useNavigate()
  const location = useLocation()
  const [isSubmitting, setIsSubmitting] = useState(false)
  const [formError, setFormError] = useState<string | null>(null)
  const [formSuccess, setFormSuccess] = useState<string | null>(
    (location.state as LocationState)?.message || null
  )
  const [formData, setFormData] = useState({
    email: "",
    password: ""
  })

  useEffect(() => {
    if (authError) {
      setFormError(authError);
      console.log("Auth error updated in LoginPage:", authError);
    }
  }, [authError]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target
    setFormData(prev => ({ ...prev, [name]: value }))
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setIsSubmitting(true)
    setFormError(null)
    setFormSuccess(null)
    console.log("Login form submitted with:", formData);

    try {
      const success = await login(formData.email, formData.password)
      console.log("Login result:", success);
      
      if (success) {
        console.log("Login successful, navigating to home");
        navigate("/")
      } else {
        console.log("Login returned false but no error thrown");
        setFormError("Đăng nhập không thành công. Vui lòng kiểm tra thông tin đăng nhập.");
      }
    } catch (err: any) {
      console.error("Login error in handleSubmit:", err);
      setFormError(err.message || "Đã xảy ra lỗi khi đăng nhập")
    } finally {
      setIsSubmitting(false)
    }
  }

  return (
    <div className="min-h-screen flex items-center justify-center bg-gradient-to-br from-purple-50 to-blue-50 p-4">
      <div className="w-full max-w-md">
        <Card className="shadow-lg border-t-4 border-t-purple-500">
          <CardHeader className="space-y-1">
            <CardTitle className="text-2xl text-center">Đăng nhập</CardTitle>
            <CardDescription className="text-center">
              Đăng nhập để học từ vựng với FlashLearn
            </CardDescription>
          </CardHeader>
          <CardContent>
            {formSuccess && (
              <Alert className="mb-4 bg-green-50 border-green-200">
                <AlertDescription className="text-green-700">{formSuccess}</AlertDescription>
              </Alert>
            )}
            
            {formError && (
              <Alert variant="destructive" className="mb-4">
                <AlertCircle className="h-4 w-4" />
                <AlertDescription>{formError}</AlertDescription>
              </Alert>
            )}
            
            <form onSubmit={handleSubmit}>
              <div className="grid gap-4">
                <div className="space-y-2">
                  <Label htmlFor="email">Email</Label>
                  <Input
                    id="email"
                    name="email"
                    type="email"
                    placeholder="name@example.com"
                    required
                    value={formData.email}
                    onChange={handleChange}
                  />
                </div>
                
                <div className="space-y-2">
                  <div className="flex items-center justify-between">
                    <Label htmlFor="password">Mật khẩu</Label>
                    <Link to="/auth/forgot-password" className="text-xs text-purple-600 hover:text-purple-700">
                      Quên mật khẩu?
                    </Link>
                  </div>
                  <Input
                    id="password"
                    name="password"
                    type="password"
                    required
                    value={formData.password}
                    onChange={handleChange}
                  />
                </div>
                
                <Button type="submit" className="w-full" disabled={isSubmitting || isLoading}>
                  {isSubmitting || isLoading ? (
                    <>
                      <Loader2 className="mr-2 h-4 w-4 animate-spin" />
                      Đang xử lý...
                    </>
                  ) : (
                    "Đăng nhập"
                  )}
                </Button>
              </div>
            </form>
          </CardContent>
          <CardFooter className="flex justify-center border-t p-4">
            <div className="text-sm text-center">
              Chưa có tài khoản?{" "}
              <Link to="/auth/register" className="underline text-purple-600 hover:text-purple-700">
                Đăng ký
              </Link>
            </div>
          </CardFooter>
        </Card>
      </div>
    </div>
  )
} 