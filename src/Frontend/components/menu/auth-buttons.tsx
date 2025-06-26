"use client"

import Link from "next/link"
import { Button } from "@/components/ui/button"
import { LogIn, UserPlus } from "lucide-react"

export function AuthButtons() {
  return (
    <div className="mt-8 space-y-3">
      <Link href="/auth/login" className="w-full block">
        <Button variant="outline" className="w-full justify-start">
          <LogIn className="w-4 h-4 mr-3" />
          Đăng nhập
        </Button>
      </Link>
      <Link href="/auth/register" className="w-full block">
        <Button className="w-full justify-start bg-purple-600 hover:bg-purple-700">
          <UserPlus className="w-4 h-4 mr-3" />
          Đăng ký
        </Button>
      </Link>
    </div>
  )
} 