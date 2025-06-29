"use client"

import { useState } from "react"
import { Button } from "@/components/ui/button"
import { Card } from "@/components/ui/card"
import { Volume2, ArrowRight, Check, X } from "lucide-react"

interface FlashcardProps {
  word: string
  translation: string
  pronunciation?: string
  example?: string
  onNext: () => void
  onResult: (result: 'correct' | 'incorrect') => void
}

export function Flashcard({ word, translation, pronunciation, example, onNext, onResult }: FlashcardProps) {
  const [isFlipped, setIsFlipped] = useState(false)

  const handleFlip = () => {
    setIsFlipped(!isFlipped)
  }

  const handleNext = () => {
    setIsFlipped(false)
    onNext()
  }

  const handleResult = (result: 'correct' | 'incorrect') => {
    onResult(result)
    handleNext()
  }

  const playAudio = () => {
    // In a real app, this would play the pronunciation audio
    console.log("Playing audio for:", word)
  }

  return (
    <div className="w-full max-w-md mx-auto">
      <div 
        className={`relative w-full h-64 cursor-pointer perspective-1000 transition-transform duration-500 ${isFlipped ? 'rotate-y-180' : ''}`}
        onClick={handleFlip}
      >
        {/* Front side */}
        <Card className={`absolute w-full h-full backface-hidden ${isFlipped ? 'hidden' : 'block'} p-6 flex flex-col items-center justify-center`}>
          <h2 className="text-3xl font-bold text-center mb-4">{word}</h2>
          {pronunciation && (
            <div className="flex items-center gap-2 text-gray-500 mb-4">
              <span>/{pronunciation}/</span>
              <Button variant="ghost" size="icon" onClick={(e) => { e.stopPropagation(); playAudio(); }}>
                <Volume2 className="h-4 w-4" />
              </Button>
            </div>
          )}
          {example && <p className="text-sm text-gray-600 italic text-center mt-4">"{example}"</p>}
          <div className="absolute bottom-4 right-4 text-gray-400 text-xs">
            Nhấn để lật thẻ
          </div>
        </Card>

        {/* Back side */}
        <Card className={`absolute w-full h-full backface-hidden rotate-y-180 ${isFlipped ? 'block' : 'hidden'} p-6 flex flex-col items-center justify-center`}>
          <h3 className="text-xl text-gray-600 mb-2">Nghĩa:</h3>
          <h2 className="text-2xl font-bold text-center">{translation}</h2>
          <div className="absolute bottom-4 right-4 text-gray-400 text-xs">
            Nhấn để lật thẻ
          </div>
        </Card>
      </div>

      {/* Controls */}
      <div className="flex justify-between mt-6">
        <Button 
          variant="outline" 
          className="flex-1 mr-2 border-red-300 hover:bg-red-50 text-red-600"
          onClick={() => handleResult('incorrect')}
        >
          <X className="w-4 h-4 mr-2" />
          Chưa thuộc
        </Button>
        <Button 
          variant="outline"
          className="flex-1 ml-2 border-green-300 hover:bg-green-50 text-green-600"
          onClick={() => handleResult('correct')}
        >
          <Check className="w-4 h-4 mr-2" />
          Đã thuộc
        </Button>
      </div>
      
      <Button 
        variant="ghost" 
        className="w-full mt-2"
        onClick={handleNext}
      >
        <ArrowRight className="w-4 h-4 mr-2" />
        Từ tiếp theo
      </Button>
    </div>
  )
} 