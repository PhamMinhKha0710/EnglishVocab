"use client"

import { useState, useRef, useEffect } from "react"
import { Button } from "@/components/ui/button"
import { Slider } from "@/components/ui/slider"
import { Volume2, Play, Pause, SkipBack, SkipForward } from "lucide-react"

interface AudioPlayerProps {
  src: string
  autoPlay?: boolean
  onEnded?: () => void
}

export function AudioPlayer({ src, autoPlay = false, onEnded }: AudioPlayerProps) {
  const [isPlaying, setIsPlaying] = useState(false)
  const [duration, setDuration] = useState(0)
  const [currentTime, setCurrentTime] = useState(0)
  const [volume, setVolume] = useState(0.7)
  const audioRef = useRef<HTMLAudioElement | null>(null)

  useEffect(() => {
    const audio = audioRef.current
    if (!audio) return

    const setAudioData = () => {
      setDuration(audio.duration)
      setCurrentTime(audio.currentTime)
    }

    const setAudioTime = () => setCurrentTime(audio.currentTime)
    
    // Events
    audio.addEventListener('loadeddata', setAudioData)
    audio.addEventListener('timeupdate', setAudioTime)
    audio.addEventListener('ended', handleEnd)

    // Cleanup
    return () => {
      audio.removeEventListener('loadeddata', setAudioData)
      audio.removeEventListener('timeupdate', setAudioTime)
      audio.removeEventListener('ended', handleEnd)
    }
  }, [])

  useEffect(() => {
    if (audioRef.current) {
      audioRef.current.volume = volume
    }
  }, [volume])

  useEffect(() => {
    if (audioRef.current) {
      audioRef.current.src = src
      if (autoPlay) {
        audioRef.current.play().catch(e => console.error("Auto-play failed:", e))
        setIsPlaying(true)
      }
    }
  }, [src, autoPlay])

  const handlePlayPause = () => {
    const audio = audioRef.current
    if (!audio) return

    if (isPlaying) {
      audio.pause()
    } else {
      audio.play().catch(e => console.error("Play failed:", e))
    }
    
    setIsPlaying(!isPlaying)
  }

  const handleTimeChange = (value: number[]) => {
    const audio = audioRef.current
    if (!audio) return

    const newTime = value[0]
    audio.currentTime = newTime
    setCurrentTime(newTime)
  }

  const handleVolumeChange = (value: number[]) => {
    setVolume(value[0])
  }

  const handleEnd = () => {
    setIsPlaying(false)
    setCurrentTime(0)
    if (onEnded) onEnded()
  }

  const handleRestart = () => {
    const audio = audioRef.current
    if (!audio) return
    
    audio.currentTime = 0
    setCurrentTime(0)
    if (!isPlaying) {
      audio.play().catch(e => console.error("Play failed:", e))
      setIsPlaying(true)
    }
  }

  const formatTime = (time: number) => {
    if (isNaN(time)) return "0:00"
    
    const minutes = Math.floor(time / 60)
    const seconds = Math.floor(time % 60)
    return `${minutes}:${seconds.toString().padStart(2, '0')}`
  }

  return (
    <div className="bg-white p-4 rounded-lg shadow-sm border">
      <audio ref={audioRef} src={src} />
      
      <div className="flex items-center justify-between mb-2">
        <span className="text-xs text-gray-500">{formatTime(currentTime)}</span>
        <span className="text-xs text-gray-500">{formatTime(duration)}</span>
      </div>
      
      <Slider
        value={[currentTime]}
        min={0}
        max={duration || 1}
        step={0.01}
        onValueChange={handleTimeChange}
        className="mb-4"
      />
      
      <div className="flex items-center justify-between">
        <div className="flex items-center gap-2">
          <Button variant="ghost" size="icon" onClick={handleRestart}>
            <SkipBack className="h-4 w-4" />
          </Button>
          
          <Button 
            variant="outline" 
            size="icon" 
            onClick={handlePlayPause}
            className="h-10 w-10 rounded-full"
          >
            {isPlaying ? (
              <Pause className="h-5 w-5" />
            ) : (
              <Play className="h-5 w-5 ml-0.5" />
            )}
          </Button>
          
          <Button variant="ghost" size="icon" disabled>
            <SkipForward className="h-4 w-4" />
          </Button>
        </div>
        
        <div className="flex items-center gap-2 w-24">
          <Volume2 className="h-4 w-4 text-gray-500" />
          <Slider
            value={[volume]}
            min={0}
            max={1}
            step={0.01}
            onValueChange={handleVolumeChange}
          />
        </div>
      </div>
    </div>
  )
} 