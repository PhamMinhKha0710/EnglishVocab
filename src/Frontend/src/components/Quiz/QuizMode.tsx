"use client"

import { useState, useEffect } from "react"
import { Card, CardContent } from "@/components/ui/card"
import { Button } from "@/components/ui/button"
import { Progress } from "@/components/ui/progress"
import { Badge } from "@/components/ui/badge"
import type { VocabularyWord } from "@/types"
import { CheckCircle, XCircle, Clock } from "lucide-react"
import { cn } from "@/lib/utils"

interface QuizModeProps {
  words: VocabularyWord[]
  onComplete: (results: QuizResult) => void
  onExit: () => void
}

interface QuizResult {
  totalQuestions: number
  correctAnswers: number
  timeSpent: number
  results: Array<{
    wordId: number
    correct: boolean
    timeSpent: number
    selectedAnswer?: string
  }>
}

interface QuizQuestion {
  word: VocabularyWord
  options: string[]
  correctAnswer: string
  type: "meaning" | "word" | "example"
}

export function QuizMode({ words, onComplete, onExit }: QuizModeProps) {
  const [questions, setQuestions] = useState<QuizQuestion[]>([])
  const [currentQuestionIndex, setCurrentQuestionIndex] = useState(0)
  const [selectedAnswer, setSelectedAnswer] = useState<string>("")
  const [showResult, setShowResult] = useState(false)
  const [startTime] = useState(Date.now())
  const [questionStartTime, setQuestionStartTime] = useState(Date.now())
  const [results, setResults] = useState<QuizResult["results"]>([])
  const [timeLeft, setTimeLeft] = useState(30) // 30 seconds per question

  // Generate quiz questions
  useEffect(() => {
    const generateQuestions = () => {
      const quizQuestions: QuizQuestion[] = words.slice(0, 10).map((word) => {
        const questionTypes: Array<"meaning" | "word" | "example"> = ["meaning", "word", "example"]
        const type = questionTypes[Math.floor(Math.random() * questionTypes.length)]

        // Get wrong answers from other words
        const otherWords = words.filter((w) => w.id !== word.id)
        const wrongAnswers = otherWords
          .sort(() => Math.random() - 0.5)
          .slice(0, 3)
          .map((w) => (type === "meaning" ? w.meaning : w.word))

        let correctAnswer: string
        let options: string[]

        switch (type) {
          case "meaning":
            correctAnswer = word.meaning
            options = [correctAnswer, ...wrongAnswers].sort(() => Math.random() - 0.5)
            break
          case "word":
            correctAnswer = word.word
            options = [correctAnswer, ...wrongAnswers].sort(() => Math.random() - 0.5)
            break
          case "example":
            correctAnswer = word.word
            const exampleWithBlank = word.example.replace(new RegExp(word.word, "gi"), "____")
            options = [correctAnswer, ...wrongAnswers].sort(() => Math.random() - 0.5)
            break
          default:
            correctAnswer = word.meaning
            options = [correctAnswer, ...wrongAnswers].sort(() => Math.random() - 0.5)
        }

        return {
          word,
          options,
          correctAnswer,
          type,
        }
      })

      setQuestions(quizQuestions)
    }

    generateQuestions()
  }, [words])

  // Timer countdown
  useEffect(() => {
    if (timeLeft > 0 && !showResult) {
      const timer = setTimeout(() => setTimeLeft(timeLeft - 1), 1000)
      return () => clearTimeout(timer)
    } else if (timeLeft === 0) {
      handleNextQuestion()
    }
  }, [timeLeft, showResult])

  const currentQuestion = questions[currentQuestionIndex]
  const progress = questions.length > 0 ? ((currentQuestionIndex + 1) / questions.length) * 100 : 0

  const handleAnswerSelect = (answer: string) => {
    setSelectedAnswer(answer)
  }

  const handleNextQuestion = () => {
    if (!currentQuestion) return

    const isCorrect = selectedAnswer === currentQuestion.correctAnswer || timeLeft === 0
    const questionTime = Date.now() - questionStartTime

    const newResult = {
      wordId: currentQuestion.word.id,
      correct: isCorrect,
      timeSpent: questionTime,
      selectedAnswer: selectedAnswer || "No answer",
    }

    setResults((prev) => [...prev, newResult])

    if (currentQuestionIndex < questions.length - 1) {
      setCurrentQuestionIndex((prev) => prev + 1)
      setSelectedAnswer("")
      setShowResult(false)
      setQuestionStartTime(Date.now())
      setTimeLeft(30)
    } else {
      // Quiz complete
      const finalResults: QuizResult = {
        totalQuestions: questions.length,
        correctAnswers: [...results, newResult].filter((r) => r.correct).length,
        timeSpent: Date.now() - startTime,
        results: [...results, newResult],
      }
      onComplete(finalResults)
    }
  }

  const handleConfirmAnswer = () => {
    setShowResult(true)
    setTimeout(handleNextQuestion, 1500) // Show result for 1.5 seconds
  }

  if (!currentQuestion) {
    return (
      <div className="flex items-center justify-center h-64">
        <div className="text-center">
          <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-purple-600 mx-auto mb-4"></div>
          <p>Đang tạo câu hỏi...</p>
        </div>
      </div>
    )
  }

  const isCorrect = selectedAnswer === currentQuestion.correctAnswer
  const hasAnswered = selectedAnswer !== ""

  return (
    <div className="max-w-2xl mx-auto space-y-6">
      {/* Header */}
      <div className="flex items-center justify-between">
        <div className="flex items-center gap-4">
          <Badge variant="outline">
            Câu {currentQuestionIndex + 1}/{questions.length}
          </Badge>
          <div className="flex items-center gap-2">
            <Clock className="w-4 h-4 text-orange-500" />
            <span className={cn("font-mono font-bold", timeLeft <= 10 ? "text-red-500" : "text-orange-500")}>
              {timeLeft}s
            </span>
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

      {/* Question */}
      <Card className="animate-fade-in">
        <CardContent className="p-8">
          <div className="text-center space-y-6">
            {/* Question Type Badge */}
            <Badge variant="secondary" className="mb-4">
              {currentQuestion.type === "meaning"
                ? "Chọn nghĩa đúng"
                : currentQuestion.type === "word"
                  ? "Chọn từ đúng"
                  : "Điền từ vào chỗ trống"}
            </Badge>

            {/* Question Content */}
            <div className="space-y-4">
              {currentQuestion.type === "meaning" && (
                <div>
                  <h2 className="text-3xl font-bold text-gray-800 mb-2">{currentQuestion.word.word}</h2>
                  <p className="text-gray-500">{currentQuestion.word.pronunciation}</p>
                  <p className="text-lg text-gray-600 mt-4">Nghĩa của từ này là gì?</p>
                </div>
              )}

              {currentQuestion.type === "word" && (
                <div>
                  <p className="text-lg text-gray-600 mb-4">Từ nào có nghĩa là:</p>
                  <h2 className="text-2xl font-bold text-gray-800">{currentQuestion.word.meaning}</h2>
                </div>
              )}

              {currentQuestion.type === "example" && (
                <div>
                  <p className="text-lg text-gray-600 mb-4">Điền từ vào chỗ trống:</p>
                  <h2 className="text-xl text-gray-800 italic">
                    "{currentQuestion.word.example.replace(new RegExp(currentQuestion.word.word, "gi"), "____")}"
                  </h2>
                </div>
              )}
            </div>

            {/* Options */}
            <div className="grid grid-cols-1 gap-3 mt-8">
              {currentQuestion.options.map((option, index) => (
                <Button
                  key={index}
                  variant={
                    showResult
                      ? option === currentQuestion.correctAnswer
                        ? "default"
                        : selectedAnswer === option
                          ? "destructive"
                          : "outline"
                      : selectedAnswer === option
                        ? "default"
                        : "outline"
                  }
                  size="lg"
                  onClick={() => !showResult && handleAnswerSelect(option)}
                  disabled={showResult}
                  className={cn(
                    "h-auto p-4 text-left justify-start transition-all",
                    showResult && option === currentQuestion.correctAnswer && "bg-green-500 hover:bg-green-600",
                    showResult &&
                      selectedAnswer === option &&
                      option !== currentQuestion.correctAnswer &&
                      "bg-red-500 hover:bg-red-600",
                  )}
                >
                  <div className="flex items-center gap-3">
                    <div className="w-6 h-6 rounded-full border-2 flex items-center justify-center text-xs font-bold">
                      {String.fromCharCode(65 + index)}
                    </div>
                    <span className="flex-1">{option}</span>
                    {showResult && option === currentQuestion.correctAnswer && <CheckCircle className="w-5 h-5" />}
                    {showResult && selectedAnswer === option && option !== currentQuestion.correctAnswer && (
                      <XCircle className="w-5 h-5" />
                    )}
                  </div>
                </Button>
              ))}
            </div>

            {/* Confirm Button */}
            {hasAnswered && !showResult && (
              <Button onClick={handleConfirmAnswer} size="lg" className="w-full mt-6 animate-fade-in">
                Xác nhận đáp án
              </Button>
            )}

            {/* Result Message */}
            {showResult && (
              <div
                className={cn(
                  "p-4 rounded-lg animate-fade-in",
                  isCorrect ? "bg-green-50 text-green-800" : "bg-red-50 text-red-800",
                )}
              >
                <div className="flex items-center justify-center gap-2">
                  {isCorrect ? <CheckCircle className="w-5 h-5" /> : <XCircle className="w-5 h-5" />}
                  <span className="font-semibold">{isCorrect ? "Chính xác!" : "Sai rồi!"}</span>
                </div>
                {!isCorrect && (
                  <p className="text-sm mt-2">
                    Đáp án đúng: <strong>{currentQuestion.correctAnswer}</strong>
                  </p>
                )}
              </div>
            )}
          </div>
        </CardContent>
      </Card>
    </div>
  )
}
