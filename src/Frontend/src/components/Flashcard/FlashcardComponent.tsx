"use client"
import { Card, CardContent } from "@/components/ui/card"
import { Button } from "@/components/ui/button"
import { Badge } from "@/components/ui/badge"
import type { VocabularyWord } from "@/types"
import { Volume2, RotateCcw } from "lucide-react"
import { cn } from "@/lib/utils"

interface FlashcardProps {
  word: VocabularyWord
  isFlipped: boolean
  onFlip: () => void
  onSpeak: (text: string) => void
  className?: string
}

export function FlashcardComponent({ word, isFlipped, onFlip, onSpeak, className }: FlashcardProps) {
  const getDifficultyLabel = (difficulty: string) => {
    switch (difficulty) {
      case "beginner":
        return "Cơ bản"
      case "intermediate":
        return "Trung bình"
      case "advanced":
        return "Nâng cao"
      default:
        return difficulty
    }
  }

  const getDifficultyColor = (difficulty: string) => {
    switch (difficulty) {
      case "beginner":
        return "bg-green-100 text-green-800"
      case "intermediate":
        return "bg-yellow-100 text-yellow-800"
      case "advanced":
        return "bg-red-100 text-red-800"
      default:
        return "bg-gray-100 text-gray-800"
    }
  }

  return (
    <div className={cn("relative h-80", className)}>
      <Card className="h-full cursor-pointer transition-all duration-300 hover:shadow-lg" onClick={onFlip}>
        <CardContent className="h-full flex flex-col justify-center items-center p-6 text-center">
          {!isFlipped ? (
            <div className="space-y-4 animate-fade-in">
              <div className="flex items-center justify-center gap-2 mb-4">
                <Badge variant="outline" className={cn("text-xs", getDifficultyColor(word.difficulty))}>
                  {getDifficultyLabel(word.difficulty)}
                </Badge>
                <Badge variant="outline" className="text-xs">
                  {word.category}
                </Badge>
              </div>

              <h2 className="text-3xl font-bold text-gray-800">{word.word}</h2>

              <p className="text-sm text-gray-500 font-mono">{word.pronunciation}</p>

              <Button
                variant="ghost"
                size="sm"
                onClick={(e) => {
                  e.stopPropagation()
                  onSpeak(word.word)
                }}
                className="flex items-center gap-2 hover:bg-purple-50"
              >
                <Volume2 className="w-4 h-4" />
                Phát âm
              </Button>

              <p className="text-gray-500 text-sm">Nhấn để xem nghĩa</p>
            </div>
          ) : (
            <div className="space-y-4 animate-fade-in">
              <h3 className="text-xl font-semibold text-gray-700">{word.word}</h3>

              <p className="text-lg text-gray-800 leading-relaxed">{word.meaning}</p>

              <div className="bg-gray-50 rounded-lg p-4 mt-4">
                <p className="text-sm text-gray-600 italic">"{word.example}"</p>
              </div>

              <Button
                variant="ghost"
                size="sm"
                onClick={(e) => {
                  e.stopPropagation()
                  onFlip()
                }}
                className="flex items-center gap-2 mt-4"
              >
                <RotateCcw className="w-4 h-4" />
                Lật lại
              </Button>
            </div>
          )}
        </CardContent>
      </Card>
    </div>
  )
}
