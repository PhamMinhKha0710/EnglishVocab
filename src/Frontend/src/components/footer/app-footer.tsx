"use client"

import Link from "next/link"
import { BookOpen } from "lucide-react"

export function AppFooter() {
  return (
    <footer className="bg-white border-t border-gray-200 py-6 md:ml-64">
      <div className="container mx-auto px-4">
        <div className="flex flex-col md:flex-row justify-between items-center">
          {/* Logo and copyright */}
          <div className="flex items-center gap-2 mb-4 md:mb-0">
            <div className="w-6 h-6 bg-gradient-to-br from-purple-500 to-blue-500 rounded-lg flex items-center justify-center">
              <BookOpen className="w-3 h-3 text-white" />
            </div>
            <span className="text-sm text-gray-600">
              © {new Date().getFullYear()} FlashLearn. Bản quyền thuộc về FlashLearn.
            </span>
          </div>

          {/* Links */}
          <div className="flex gap-6">
            <Link href="/terms" className="text-sm text-gray-600 hover:text-purple-600">
              Điều khoản
            </Link>
            <Link href="/privacy" className="text-sm text-gray-600 hover:text-purple-600">
              Chính sách bảo mật
            </Link>
            <Link href="/contact" className="text-sm text-gray-600 hover:text-purple-600">
              Liên hệ
            </Link>
          </div>
        </div>
      </div>
    </footer>
  )
} 