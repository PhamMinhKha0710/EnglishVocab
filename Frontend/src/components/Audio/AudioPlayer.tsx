"use client"

import { useState, useRef, useEffect } from "react"
import { Button } from "@/components/ui/button"
import { Volume2, VolumeX, RotateCcw } from "lucide-react"
import { useAppStore } from "@/store/useAppStore"

interface AudioPlayerProps {
  text: string
  language?: string
  className?: string
}

export function AudioPlayer({ text, language = "en-US", className }: AudioPlayerProps) {
  const { settings } = useAppStore()
  const [isPlaying, setIsPlaying] = useState(false)
  const [isSupported, setIsSupported] = useState(false)
  const utteranceRef = useRef<SpeechSynthesisUtterance | null>(null)

  useEffect(() => {
    setIsSupported("speechSynthesis" in window)
  }, [])

  const speak = () => {
    if (!isSupported || !settings.soundEnabled) return

    // Stop any current speech
    speechSynthesis.cancel()

    const utterance = new SpeechSynthesisUtterance(text)
    utterance.lang = language
    utterance.rate = 0.8 // Slightly slower for learning
    utterance.pitch = 1
    utterance.volume = 1

    utterance.onstart = () => setIsPlaying(true)
    utterance.onend = () => setIsPlaying(false)
    utterance.onerror = () => setIsPlaying(false)

    utteranceRef.current = utterance
    speechSynthesis.speak(utterance)
  }

  const stop = () => {
    speechSynthesis.cancel()
    setIsPlaying(false)
  }

  if (!isSupported) {
    return (
      <Button variant="ghost" size="sm" disabled className={className}>
        <VolumeX className="w-4 h-4 mr-2" />
        Không hỗ trợ
      </Button>
    )
  }

  return (
    <div className={`flex items-center gap-2 ${className}`}>
      <Button
        variant="ghost"
        size="sm"
        onClick={isPlaying ? stop : speak}
        disabled={!settings.soundEnabled}
        className="flex items-center gap-2"
      >
        <Volume2 className="w-4 h-4" />
        {isPlaying ? "Đang phát..." : "Phát âm"}
      </Button>

      {isPlaying && (
        <Button variant="ghost" size="sm" onClick={stop}>
          <RotateCcw className="w-4 h-4" />
        </Button>
      )}
    </div>
  )
}
