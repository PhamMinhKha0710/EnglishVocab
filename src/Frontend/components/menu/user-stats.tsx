"use client"

import { Badge } from "@/components/ui/badge"

export function UserStats() {
  return (
    <div className="mt-8 p-4 bg-gray-50 rounded-lg">
      <h3 className="text-sm font-semibold text-gray-700 mb-2">Hôm nay</h3>
      <div className="space-y-2">
        <div className="flex justify-between items-center">
          <span className="text-xs text-gray-600">Từ đã học</span>
          <Badge variant="secondary" className="text-xs">
            5/10
          </Badge>
        </div>
        <div className="flex justify-between items-center">
          <span className="text-xs text-gray-600">Chuỗi ngày</span>
          <Badge variant="secondary" className="text-xs">
            7 ngày
          </Badge>
        </div>
      </div>
    </div>
  )
} 