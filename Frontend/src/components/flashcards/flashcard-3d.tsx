"use client"

import { useState } from "react"
import { Button } from "@/components/ui/button"
import { Badge } from "@/components/ui/badge"
import { Volume2, Info } from "lucide-react"
import { cn } from "@/lib/utils"
import "./flashcard-styles.css"

interface FlashCard3DProps {
  id: number
  word: string
  meaning: string
  pronunciation?: string
  example?: string
  category?: string
  difficulty?: "beginner" | "intermediate" | "advanced"
  onKnown?: () => void
  onUnknown?: () => void
  onSpeak?: (text: string) => void
}

// Utility function to get difficulty color
const getDifficultyColor = (difficulty: string | undefined) => {
  switch (difficulty) {
    case "beginner":
      return "bg-green-100 text-green-700 border-green-300"
    case "intermediate":
      return "bg-yellow-100 text-yellow-700 border-yellow-300"
    case "advanced":
      return "bg-red-100 text-red-700 border-red-300"
    default:
      return "bg-gray-100 text-gray-700 border-gray-300"
  }
}

// Utility function to get difficulty label
const getDifficultyLabel = (difficulty: string | undefined) => {
  switch (difficulty) {
    case "beginner":
      return "Cơ bản"
    case "intermediate":
      return "Trung bình"
    case "advanced":
      return "Nâng cao"
    default:
      return difficulty || ""
  }
}

export function FlashCard3D({ 
  id, 
  word, 
  meaning, 
  pronunciation, 
  example, 
  category, 
  difficulty,
  onKnown,
  onUnknown,
  onSpeak
}: FlashCard3DProps) {
  const [flipped, setFlipped] = useState(false)
  const [isFlipping, setIsFlipping] = useState(false)

  const handleFlip = () => {
    if (isFlipping) return; // Prevent multiple flips during animation
    
    // Add click animation to container element
    const container = document.querySelector('.flashcard-3d-container');
    if (container) {
      container.classList.add('card-click');
      setTimeout(() => {
        container.classList.remove('card-click');
      }, 300);
    }
    
    setIsFlipping(true);
    setFlipped(!flipped);
    
    // Reset flipping state when animation completes
    setTimeout(() => {
      setIsFlipping(false);
    }, 700); // Match duration to CSS transition (700ms)
  }
  
  const handleSpeak = (e: React.MouseEvent) => {
    e.stopPropagation()
    if (onSpeak) onSpeak(word)
  }

  const handleKnown = () => {
    if (onKnown) onKnown()
  }

  const handleUnknown = () => {
    if (onUnknown) onUnknown()
  }

  return (
    <div className="w-full">
      {/* 3D Card Container */}
      <div className="w-full h-[380px] mb-4 flashcard-3d-container rounded-xl shadow-xl">
        {/* Card Inner Container - This is what rotates */}
        <div 
          className={cn(
            "relative w-full h-full cursor-pointer flashcard-3d-inner",
            "flip-transition",
            flipped ? "rotate-y-180" : ""
          )}
          onClick={handleFlip}
        >
          {/* Front Card Face */}
          <div className={cn(
            "absolute inset-0 w-full h-full",
            "backface-hidden rounded-xl flashcard-3d-front",
            "border-2 border-purple-200 bg-white",
            "flex flex-col shadow-lg"
          )}>
            <div className="bg-purple-50 px-6 py-4 rounded-t-xl flex justify-between items-center">
              {difficulty && (
                <Badge className={getDifficultyColor(difficulty)}>
                  {getDifficultyLabel(difficulty)}
                </Badge>
              )}
              {category && <Badge variant="outline">{category}</Badge>}
            </div>
            
            <div className="flex-grow flex flex-col items-center justify-center p-6">
              <h2 className="text-4xl font-bold mb-4 text-purple-800">{word}</h2>
              {pronunciation && (
                <p className="text-gray-500 font-mono mb-6">{pronunciation}</p>
              )}
              
              <Button
                variant="outline"
                onClick={handleSpeak}
                className="mb-4 border-purple-200 hover:bg-purple-50"
              >
                <Volume2 className="w-4 h-4 mr-2" />
                Phát âm
              </Button>
            </div>
            
            <div className="bg-purple-50 px-6 py-3 rounded-b-xl text-center text-gray-600 text-sm flex items-center justify-center">
              <Info className="w-4 h-4 mr-2 text-purple-600" />
              Nhấn vào thẻ để xem nghĩa
            </div>
          </div>

          {/* Back Card Face */}
          <div className={cn(
            "absolute inset-0 w-full h-full",
            "backface-hidden rounded-xl rotate-y-180 flashcard-3d-back",
            "border-2 border-purple-200 bg-white",
            "flex flex-col shadow-lg"
          )}>
            <div className="bg-blue-50 px-6 py-4 rounded-t-xl flex items-center">
              <h3 className="text-lg font-medium text-blue-800">{word}</h3>
              {category && <Badge variant="outline" className="ml-2">{category}</Badge>}
            </div>
            
            <div className="flex-grow flex flex-col p-6">
              <div className="mb-6">
                <h2 className="text-base font-semibold text-purple-800 mb-2">Nghĩa:</h2>
                <div className="bg-purple-50 p-4 rounded-xl">
                  <p className="text-lg">{meaning}</p>
                </div>
              </div>
              
              {example && (
                <div>
                  <h2 className="text-base font-semibold text-blue-800 mb-2">Ví dụ:</h2>
                  <div className="bg-blue-50 p-4 rounded-xl">
                    <p className="text-md italic">"{example}"</p>
                  </div>
                </div>
              )}
            </div>
            
            <div className="bg-blue-50 px-6 py-3 rounded-b-xl text-center text-gray-600 text-sm flex items-center justify-center">
              <Info className="w-4 h-4 mr-2 text-blue-600" />
              Nhấn vào thẻ để quay lại
            </div>
          </div>
        </div>
      </div>

      {/* Action Buttons */}
      <div className="grid grid-cols-2 gap-4">
        <Button 
          className="bg-red-500 hover:bg-red-600 py-5 text-lg font-semibold shadow-md"
          onClick={handleUnknown}
        >
          Chưa biết
        </Button>
        <Button 
          className="bg-green-500 hover:bg-green-600 py-5 text-lg font-semibold shadow-md"
          onClick={handleKnown}
        >
          Đã biết
        </Button>
      </div>
    </div>
  )
} 