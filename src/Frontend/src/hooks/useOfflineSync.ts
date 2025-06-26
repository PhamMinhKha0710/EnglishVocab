"use client"

import { useState, useEffect } from "react"
import { useAppStore } from "@/store/useAppStore"

interface SyncStatus {
  isOnline: boolean
  lastSync: Date | null
  pendingChanges: number
  syncing: boolean
}

export function useOfflineSync() {
  const [syncStatus, setSyncStatus] = useState<SyncStatus>({
    isOnline: navigator.onLine,
    lastSync: null,
    pendingChanges: 0,
    syncing: false,
  })

  const { userProgress, studySessions, vocabulary } = useAppStore()

  // Monitor online status
  useEffect(() => {
    const handleOnline = () => {
      setSyncStatus((prev) => ({ ...prev, isOnline: true }))
      // Trigger sync when coming back online
      syncData()
    }

    const handleOffline = () => {
      setSyncStatus((prev) => ({ ...prev, isOnline: false }))
    }

    window.addEventListener("online", handleOnline)
    window.addEventListener("offline", handleOffline)

    return () => {
      window.removeEventListener("online", handleOnline)
      window.removeEventListener("offline", handleOffline)
    }
  }, [])

  // Store data locally when offline
  useEffect(() => {
    if (!syncStatus.isOnline) {
      // Store pending changes in localStorage
      const pendingData = {
        userProgress,
        studySessions,
        vocabulary,
        timestamp: Date.now(),
      }
      localStorage.setItem("pendingSync", JSON.stringify(pendingData))

      setSyncStatus((prev) => ({
        ...prev,
        pendingChanges: prev.pendingChanges + 1,
      }))
    }
  }, [userProgress, studySessions, vocabulary, syncStatus.isOnline])

  const syncData = async () => {
    if (!syncStatus.isOnline) return

    setSyncStatus((prev) => ({ ...prev, syncing: true }))

    try {
      // Get pending changes
      const pendingData = localStorage.getItem("pendingSync")

      if (pendingData) {
        const data = JSON.parse(pendingData)

        // Here you would sync with your backend
        // For now, we'll just simulate the sync
        await new Promise((resolve) => setTimeout(resolve, 1000))

        // Clear pending changes
        localStorage.removeItem("pendingSync")

        setSyncStatus((prev) => ({
          ...prev,
          lastSync: new Date(),
          pendingChanges: 0,
          syncing: false,
        }))
      }
    } catch (error) {
      console.error("Sync failed:", error)
      setSyncStatus((prev) => ({ ...prev, syncing: false }))
    }
  }

  const forcSync = () => {
    if (syncStatus.isOnline) {
      syncData()
    }
  }

  return {
    syncStatus,
    syncData: forcSync,
  }
}
