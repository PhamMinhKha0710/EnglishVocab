"use client"

import { useEffect } from "react"
import { useAppStore } from "@/store/useAppStore"

export function useNotification() {
  const { settings } = useAppStore()

  useEffect(() => {
    if (!settings.notificationsEnabled) return

    // Request notification permission
    if ("Notification" in window && Notification.permission === "default") {
      Notification.requestPermission()
    }

    // Set up daily reminder
    if (settings.studyReminder.enabled) {
      const [hours, minutes] = settings.studyReminder.time.split(":").map(Number)
      const now = new Date()
      const reminderTime = new Date()
      reminderTime.setHours(hours, minutes, 0, 0)

      // If reminder time has passed today, set for tomorrow
      if (reminderTime <= now) {
        reminderTime.setDate(reminderTime.getDate() + 1)
      }

      const timeUntilReminder = reminderTime.getTime() - now.getTime()

      const timeoutId = setTimeout(() => {
        if (Notification.permission === "granted") {
          new Notification("FlashLearn - Nhắc nhở học tập", {
            body: "Đã đến giờ học từ vựng! Hãy duy trì streak của bạn.",
            icon: "/favicon.ico",
            badge: "/favicon.ico",
          })
        }
      }, timeUntilReminder)

      return () => clearTimeout(timeoutId)
    }
  }, [settings.notificationsEnabled, settings.studyReminder])

  const showNotification = (title: string, body: string) => {
    if (settings.notificationsEnabled && Notification.permission === "granted") {
      new Notification(title, {
        body,
        icon: "/favicon.ico",
        badge: "/favicon.ico",
      })
    }
  }

  return { showNotification }
}
