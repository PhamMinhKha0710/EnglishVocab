import { type ClassValue, clsx } from "clsx"
import { twMerge } from "tailwind-merge"

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}

export function formatDate(date: Date | string): string {
  if (!date) return ""
  const d = typeof date === "string" ? new Date(date) : date
  return d.toLocaleDateString("vi-VN", {
    weekday: "long",
    year: "numeric", 
    month: "long", 
    day: "numeric"
  })
}

export function formatTime(seconds: number): string {
  const minutes = Math.floor(seconds / 60)
  const remainingSeconds = seconds % 60
  return `${minutes}:${remainingSeconds.toString().padStart(2, '0')}`
}

export function getGreeting(): string {
  const hour = new Date().getHours()
  if (hour < 12) return "Chào buổi sáng"
  if (hour < 18) return "Chào buổi chiều"
  return "Chào buổi tối"
}

export function calculateLevel(experience: number): number {
  // Mỗi level cần nhiều kinh nghiệm hơn level trước
  const baseXP = 100
  const multiplier = 1.5
  
  let level = 1
  let xpForNextLevel = baseXP
  let totalXpNeeded = xpForNextLevel
  
  while (experience >= totalXpNeeded) {
    level++
    xpForNextLevel = Math.round(baseXP * Math.pow(multiplier, level - 1))
    totalXpNeeded += xpForNextLevel
  }
  
  return level
}

export function calculateProgress(currentXP: number, level: number): number {
  const baseXP = 100
  const multiplier = 1.5
  
  // Tính tổng XP cần cho đến level hiện tại
  let totalXPForCurrentLevel = 0
  for (let i = 1; i < level; i++) {
    totalXPForCurrentLevel += Math.round(baseXP * Math.pow(multiplier, i - 1))
  }
  
  // Tính XP cần cho level tiếp theo
  const xpForNextLevel = Math.round(baseXP * Math.pow(multiplier, level - 1))
  
  // Tính XP hiện có trong level này
  const currentLevelXP = currentXP - totalXPForCurrentLevel
  
  // Tính phần trăm
  return Math.min(100, Math.max(0, Math.floor((currentLevelXP / xpForNextLevel) * 100)))
}
