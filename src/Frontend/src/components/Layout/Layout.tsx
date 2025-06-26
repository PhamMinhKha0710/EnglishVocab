import type React from "react"
import { Sidebar } from "./Sidebar"

interface LayoutProps {
  children: React.ReactNode
}

export function Layout({ children }: LayoutProps) {
  return (
    <div className="min-h-screen bg-gradient-to-br from-purple-50 to-blue-50">
      <Sidebar />
      <main className="md:ml-64 p-6">{children}</main>
    </div>
  )
}
