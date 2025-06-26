"use client"

import type React from "react"

import { useState, useEffect, useRef } from "react"
import { Card, CardContent } from "@/components/ui/card"
import { Button } from "@/components/ui/button"
import { Progress } from "@/components/ui/progress"
import { Badge } from "@/components/ui/badge"
import type { VocabularyWord } from "@/types"
import { CheckCircle, XCircle, Clock, Keyboard } from "lucide-react"
import { cn } from "@/lib/utils"

interface TypingModeProps {
  words: VocabularyWord[]
  onComplete: (results: TypingResult) => void
  onExit: () => void
}

interface TypingResult {
  totalWords: number
  correctWords: number
  averageWPM: number
  accuracy: number
  timeSpent: number
  results: Array<{
    wordId: number
    word: string
    userInput: string
    correct: boolean
    timeSpent: number
    wpm: number
  }>
}

export function TypingMode({ words, onComplete, onExit }: TypingModeProps) {
  const [currentWordIndex, setCurrentWordIndex] = useState(0)
  const [userInput, setUserInput] = useState("")
  const [isComplete, setIsComplete] = useState(false)
  const [startTime] = useState(Date.now())
  const [wordStartTime, setWordStartTime] = useState(Date.now())
  const [results, setResults] = useState<TypingResult["results"]>([])
  const [showFeedback, setShowFeedback] = useState(false)
  const [currentResult, setCurrentResult] = useState<{ correct: boolean; wpm: number } | null>(null)

  const inputRef = useRef<HTMLInputElement>(null)
  const currentWord = words[currentWordIndex]
  const progress = words.length > 0 ? ((currentWordIndex + 1) / words.length) * 100 : 0

  useEffect(() => {
    inputRef.current?.focus()
  }, [currentWordIndex])

  const calculateWPM = (word: string, timeMs: number) => {
    const minutes = timeMs / (1000 * 60)
    const characters = word.length
    return Math.round(characters / 5 / minutes) // Standard WPM calculation
  }

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setUserInput(e.target.value)
  }

  const handleKeyPress = (e: React.KeyboardEvent<HTMLInputElement>) => {
    if (e.key === "Enter") {
      handleSubmitWord()
    }
  }

  const handleSubmitWord = () => {
    if (!currentWord) return

    const timeSpent = Date.now() - wordStartTime
    const isCorrect = userInput.toLowerCase().trim() === currentWord.word.toLowerCase()
    const wpm = calculateWPM(currentWord.word, timeSpent)

    const result = {
      wordId: currentWord.id,
      word: currentWord.word,
      userInput: userInput.trim(),
      correct: isCorrect,
      timeSpent,
      wpm,
    }

    setResults((prev) => [...prev, result])
    setCurrentResult({ correct: isCorrect, wpm })
    setShowFeedback(true)

    // Show feedback for 1 second then move to next word
    setTimeout(() => {
      setShowFeedback(false)
      setCurrentResult(null)

      if (currentWordIndex < words.length - 1) {
        setCurrentWordIndex((prev) => prev + 1)
        setUserInput("")
        setWordStartTime(Date.now())
      } else {
        // Complete typing test
        const finalResults = [...results, result]
        const totalTime = Date.now() - startTime
        const correctCount = finalResults.filter((r) => r.correct).length
        const totalCharacters = finalResults.reduce((sum, r) => sum + r.word.length, 0)
        const averageWPM = Math.round(totalCharacters / 5 / (totalTime / (1000 * 60)))
        const accuracy = (correctCount / finalResults.length) * 100

        const typingResult: TypingResult = {
          totalWords: finalResults.length,
          correctWords: correctCount,
          averageWPM,
          accuracy,
          timeSpent: totalTime,
          results: finalResults,
        }

        setIsComplete(true)
        onComplete(typingResult)
      }
    }, 1500)
  }

  const getInputStatus = () => {
    if (!userInput) return "default"

    const currentInput = userInput.toLowerCase()
    const targetWord = currentWord.word.toLowerCase()

    if (targetWord.startsWith(currentInput)) {
      return currentInput === targetWord ? "correct" : "typing"
    } else {
      return "error"
    }
  }

  const inputStatus = getInputStatus()

  if (isComplete) {
    return (
      <div className="max-w-2xl mx-auto text-center space-y-6">
        <div className="text-6xl mb-4">⌨️</div>
        <h2 className="text-2xl font-bold">Hoàn thành bài typing!</h2>
        <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
          <div className="bg-blue-50 p-4 rounded-lg">
            <div className="text-2xl font-bold text-blue-600">{results.filter((r) => r.correct).length}</div>
            <div className="text-sm text-gray-600">Đúng</div>
          </div>
          <div className="bg-green-50 p-4 rounded-lg">
            <div className="text-2xl font-bold text-green-600">
              {Math.round((results.filter((r) => r.correct).length / results.length) * 100)}%
            </div>
            <div className="text-sm text-gray-600">Độ chính xác</div>
          </div>
          <div className="bg-purple-50 p-4 rounded-lg">
            <div className="text-2xl font-bold text-purple-600">
              {Math.round(results.reduce((sum, r) => sum + r.wpm, 0) / results.length)}
            </div>
            <div className="text-sm text-gray-600">WPM trung bình</div>
          </div>
          <div className="bg-orange-50 p-4 rounded-lg">
            <div className="text-2xl font-bold text-orange-600">{Math.round((Date.now() - startTime) / 1000)}s</div>
            <div className="text-sm text-gray-600">Thời gian</div>
          </div>
        </div>
      </div>
    )
  }

  return (
    <div className="max-w-2xl mx-auto space-y-6">
      {/* Header */}
      <div className="flex items-center justify-between">
        <div className="flex items-center gap-4">
          <Badge variant="outline">
            <Keyboard className="w-3 h-3 mr-1" />
            Từ {currentWordIndex + 1}/{words.length}
          </Badge>
          <div className="flex items-center gap-2">
            <Clock className="w-4 h-4 text-blue-500" />
            <span className="font-mono text-blue-600">{Math.round((Date.now() - startTime) / 1000)}s</span>
          </div>
        </div>
        <Button variant="ghost" size="sm" onClick={onExit}>
          Thoát
        </Button>
      </div>

      {/* Progress */}
      <div className="space-y-2">
        <div className="flex justify-between text-sm text-gray-600">
          <span>Tiến độ</span>
          <span>{Math.round(progress)}%</span>
        </div>
        <Progress value={progress} className="h-2" />
      </div>

      {/* Typing Area */}
      <Card className="animate-fade-in">
        <CardContent className="p-8">
          <div className="text-center space-y-6">
            {/* Word Info */}
            <div className="space-y-2">
              <Badge variant="secondary">{currentWord.category}</Badge>
              <h2 className="text-lg text-gray-600">Gõ từ này:</h2>
              <div className="text-4xl font-bold text-gray-800 mb-2">{currentWord.meaning}</div>
              <p className="text-gray-500 italic">"{currentWord.example}"</p>
            </div>

            {/* Input Field */}
            <div className="space-y-4">
              <div className="relative">
                <input
                  ref={inputRef}
                  type="text"
                  value={userInput}
                  onChange={handleInputChange}
                  onKeyPress={handleKeyPress}
                  placeholder="Gõ từ tiếng Anh..."
                  disabled={showFeedback}
                  className={cn(
                    "w-full text-2xl text-center p-4 border-2 rounded-lg font-mono transition-all",
                    inputStatus === "correct" && "border-green-500 bg-green-50",
                    inputStatus === "error" && "border-red-500 bg-red-50",
                    inputStatus === "typing" && "border-blue-500 bg-blue-50",
                    inputStatus === "default" && "border-gray-300",
                    showFeedback && "opacity-50",
                  )}
                />

                {/* Target word hint */}
                <div className="absolute inset-0 flex items-center justify-center pointer-events-none">
                  <div className="text-2xl font-mono text-gray-300 relative">
                    {currentWord.word.split("").map((char, index) => (
                      <span
                        key={index}
                        className={cn(
                          index < userInput.length
                            ? userInput[index]?.toLowerCase() === char.toLowerCase()
                              ? "text-green-500"
                              : "text-red-500"
                            : "text-gray-300",
                        )}
                      >
                        {index < userInput.length ? userInput[index] : char}
                      </span>
                    ))}
                  </div>
                </div>
              </div>

              {/* Submit Button */}
              <Button
                onClick={handleSubmitWord}
                disabled={!userInput.trim() || showFeedback}
                size="lg"
                className="w-full"
              >
                Xác nhận (Enter)
              </Button>
            </div>

            {/* Feedback */}
            {showFeedback && currentResult && (
              <div
                className={cn(
                  "p-4 rounded-lg animate-fade-in",
                  currentResult.correct ? "bg-green-50 text-green-800" : "bg-red-50 text-red-800",
                )}
              >
                <div className="flex items-center justify-center gap-2 mb-2">
                  {currentResult.correct ? <CheckCircle className="w-5 h-5" /> : <XCircle className="w-5 h-5" />}
                  <span className="font-semibold">{currentResult.correct ? "Chính xác!" : "Sai rồi!"}</span>
                </div>
                <div className="text-sm">
                  {!currentResult.correct && (
                    <p>
                      Đáp án đúng: <strong>{currentWord.word}</strong>
                    </p>
                  )}
                  <p>
                    Tốc độ: <strong>{currentResult.wpm} WPM</strong>
                  </p>
                </div>
              </div>
            )}

            {/* Instructions */}
            <div className="text-sm text-gray-500">💡 Gõ từ tiếng Anh tương ứng với nghĩa tiếng Việt ở trên</div>
          </div>
        </CardContent>
      </Card>
    </div>
  )
}
