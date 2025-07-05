"use client"

import { useState } from "react"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { ArrowRight, Check, X } from "lucide-react"

interface Option {
  id: string
  text: string
}

interface MultipleChoiceProps {
  question: string
  options: Option[]
  correctOptionId: string
  onNext: () => void
  onResult: (isCorrect: boolean) => void
}

export function MultipleChoice({ question, options, correctOptionId, onNext, onResult }: MultipleChoiceProps) {
  const [selectedOptionId, setSelectedOptionId] = useState<string | null>(null)
  const [hasSubmitted, setHasSubmitted] = useState(false)
  
  const handleOptionSelect = (optionId: string) => {
    if (!hasSubmitted) {
      setSelectedOptionId(optionId)
    }
  }
  
  const handleSubmit = () => {
    if (selectedOptionId) {
      setHasSubmitted(true)
      const isCorrect = selectedOptionId === correctOptionId
      onResult(isCorrect)
    }
  }
  
  const handleNext = () => {
    setSelectedOptionId(null)
    setHasSubmitted(false)
    onNext()
  }
  
  const isCorrect = selectedOptionId === correctOptionId
  
  return (
    <Card className="w-full max-w-md mx-auto">
      <CardHeader>
        <CardTitle className="text-xl text-center">{question}</CardTitle>
      </CardHeader>
      <CardContent className="space-y-4">
        {options.map((option) => {
          const isSelected = selectedOptionId === option.id
          const isCorrectOption = option.id === correctOptionId
          
          let optionClassName = "p-4 border rounded-md cursor-pointer transition-colors"
          
          if (hasSubmitted) {
            if (isCorrectOption) {
              optionClassName += " bg-green-50 border-green-300"
            } else if (isSelected) {
              optionClassName += " bg-red-50 border-red-300"
            }
          } else if (isSelected) {
            optionClassName += " bg-blue-50 border-blue-300"
          } else {
            optionClassName += " hover:bg-gray-50"
          }
          
          return (
            <div
              key={option.id}
              className={optionClassName}
              onClick={() => handleOptionSelect(option.id)}
            >
              <div className="flex justify-between items-center">
                <span>{option.text}</span>
                {hasSubmitted && (
                  <span>
                    {isCorrectOption && <Check className="h-5 w-5 text-green-600" />}
                    {isSelected && !isCorrectOption && <X className="h-5 w-5 text-red-600" />}
                  </span>
                )}
              </div>
            </div>
          )
        })}
        
        <div className="flex justify-between mt-6">
          {!hasSubmitted ? (
            <Button 
              className="w-full bg-purple-600 hover:bg-purple-700"
              onClick={handleSubmit}
              disabled={!selectedOptionId}
            >
              Kiểm tra
            </Button>
          ) : (
            <Button 
              className="w-full"
              onClick={handleNext}
            >
              <ArrowRight className="w-4 h-4 mr-2" />
              Câu hỏi tiếp theo
            </Button>
          )}
        </div>
      </CardContent>
    </Card>
  )
} 