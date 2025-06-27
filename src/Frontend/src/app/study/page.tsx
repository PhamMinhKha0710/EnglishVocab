"use client"

import { useState, useEffect } from "react"
import { Navigation } from "@/components/navigation"
import { Button } from "@/components/ui/button"
import { Card, CardContent } from "@/components/ui/card"
import { Progress } from "@/components/ui/progress"
import { Badge } from "@/components/ui/badge"
import {
  RotateCcw,
  CheckCircle,
  XCircle,
  Flame,
  Trophy,
  Play,
  Pause,
  SkipForward,
  Volume2,
  ArrowLeft,
} from "lucide-react"
import { vocabularyData } from "@/lib/vocabulary-data"
import Link from "next/link"

export default function StudyPage() {
  const [currentCardIndex, setCurrentCardIndex] = useState(0)
  const [isFlipped, setIsFlipped] = useState(false)
  const [streak, setStreak] = useState(7)
  const [totalPoints, setTotalPoints] = useState(1250)
  const [isSessionActive, setIsSessionActive] = useState(false)
  const [sessionTime, setSessionTime] = useState(0)
  const [knownCards, setKnownCards] = useState<number[]>([])
  const [unknownCards, setUnknownCards] = useState<number[]>([])
  const [selectedDifficulty, setSelectedDifficulty] = useState<string>("all")
  const [selectedCategory, setSelectedCategory] = useState<string>("all")

  const filteredVocabulary = vocabularyData.filter((word) => {
    const difficultyMatch = selectedDifficulty === "all" || word.difficulty === selectedDifficulty
    const categoryMatch = selectedCategory === "all" || word.category === selectedCategory
    return difficultyMatch && categoryMatch
  })

  const currentCard = filteredVocabulary[currentCardIndex]
  const progress = filteredVocabulary.length > 0 ? ((currentCardIndex + 1) / filteredVocabulary.length) * 100 : 0

  // Timer for session
  useEffect(() => {
    let interval: NodeJS.Timeout
    if (isSessionActive) {
      interval = setInterval(() => {
        setSessionTime((prev) => prev + 1)
      }, 1000)
    }
    return () => clearInterval(interval)
  }, [isSessionActive])

  const formatTime = (seconds: number) => {
    const mins = Math.floor(seconds / 60)
    const secs = seconds % 60
    return `${mins}:${secs.toString().padStart(2, "0")}`
  }

  const handleFlip = () => {
    setIsFlipped(!isFlipped)
  }

  const handleKnown = () => {
    if (!knownCards.includes(currentCard.id)) {
      setKnownCards([...knownCards, currentCard.id])
      setTotalPoints((prev) => prev + 10)
    }
    nextCard()
  }

  const handleUnknown = () => {
    if (!unknownCards.includes(currentCard.id)) {
      setUnknownCards([...unknownCards, currentCard.id])
    }
    nextCard()
  }

  const nextCard = () => {
    setIsFlipped(false)
    if (currentCardIndex < filteredVocabulary.length - 1) {
      setCurrentCardIndex((prev) => prev + 1)
    } else {
      // Session complete
      setIsSessionActive(false)
      setStreak((prev) => prev + 1)
    }
  }

  const startSession = () => {
    setIsSessionActive(true)
    setCurrentCardIndex(0)
    setIsFlipped(false)
    setSessionTime(0)
    setKnownCards([])
    setUnknownCards([])
  }

  const toggleSession = () => {
    setIsSessionActive(!isSessionActive)
  }

  const speakWord = (text: string) => {
    if ("speechSynthesis" in window) {
      const utterance = new SpeechSynthesisUtterance(text)
      utterance.lang = "en-US"
      speechSynthesis.speak(utterance)
    }
  }

  if (filteredVocabulary.length === 0) {
    return (
      <div className="min-h-screen bg-gradient-to-br from-purple-50 to-blue-50">
        <Navigation />
        <main className="md:ml-64 p-6">
          <div className="max-w-md mx-auto pt-8 text-center">
            <h1 className="text-2xl font-bold text-gray-800 mb-4">Không có từ vựng</h1>
            <p className="text-gray-600 mb-6">Vui lòng chọn danh mục hoặc độ khó khác</p>
            <Link href="/">
              <Button>Quay về trang chủ</Button>
            </Link>
          </div>
        </main>
      </div>
    )
  }

  if (currentCardIndex >= filteredVocabulary.length) {
    return (
      <div className="min-h-screen bg-gradient-to-br from-purple-50 to-blue-50">
        <Navigation />
        <main className="md:ml-64 p-6">
          <div className="max-w-md mx-auto pt-8">
            <div className="text-center space-y-6">
              <div className="text-6xl">🎉</div>
              <h1 className="text-2xl font-bold text-gray-800">Hoàn thành session!</h1>
              <div className="bg-white rounded-2xl p-6 shadow-lg space-y-4">
                <div className="flex justify-between items-center">
                  <span className="text-gray-600">Thời gian:</span>
                  <span className="font-semibold">{formatTime(sessionTime)}</span>
                </div>
                <div className="flex justify-between items-center">
                  <span className="text-gray-600">Đã biết:</span>
                  <span className="font-semibold text-green-600">{knownCards.length}</span>
                </div>
                <div className="flex justify-between items-center">
                  <span className="text-gray-600">Cần ôn:</span>
                  <span className="font-semibold text-orange-600">{unknownCards.length}</span>
                </div>
                <div className="flex justify-between items-center">
                  <span className="text-gray-600">Điểm kiếm được:</span>
                  <span className="font-semibold text-purple-600">+{knownCards.length * 10}</span>
                </div>
              </div>
              <div className="space-y-3">
                <Button onClick={startSession} className="w-full bg-purple-600 hover:bg-purple-700">
                  Bắt đầu session mới
                </Button>
                <Link href="/" className="block">
                  <Button variant="outline" className="w-full">
                    Quay về trang chủ
                  </Button>
                </Link>
              </div>
            </div>
          </div>
        </main>
      </div>
    )
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-purple-50 to-blue-50">
      <Navigation />

      <main className="md:ml-64 p-6">
        <div className="max-w-md mx-auto">
          {/* Header */}
          <div className="flex items-center justify-between mb-6">
            <Link href="/">
              <Button variant="ghost" size="sm">
                <ArrowLeft className="w-4 h-4 mr-2" />
                Quay lại
              </Button>
            </Link>
            <div className="flex items-center gap-4">
              <div className="flex items-center gap-2">
                <Flame className="w-5 h-5 text-orange-500" />
                <span className="font-semibold text-orange-600">{streak}</span>
              </div>
              <div className="flex items-center gap-2">
                <Trophy className="w-5 h-5 text-yellow-500" />
                <span className="font-semibold text-yellow-600">{totalPoints}</span>
              </div>
            </div>
          </div>

          {/* Filters */}
          {!isSessionActive && (
            <div className="mb-6 space-y-4">
              <div>
                <label className="text-sm font-medium text-gray-700 mb-2 block">Độ khó</label>
                <div className="flex gap-2">
                  {["all", "beginner", "intermediate", "advanced"].map((level) => (
                    <Button
                      key={level}
                      variant={selectedDifficulty === level ? "default" : "outline"}
                      size="sm"
                      onClick={() => setSelectedDifficulty(level)}
                    >
                      {level === "all"
                        ? "Tất cả"
                        : level === "beginner"
                          ? "Cơ bản"
                          : level === "intermediate"
                            ? "Trung bình"
                            : "Nâng cao"}
                    </Button>
                  ))}
                </div>
              </div>
            </div>
          )}

          {/* Progress */}
          <div className="mb-6">
            <div className="flex justify-between text-sm text-gray-600 mb-2">
              <span>Tiến độ</span>
              <span>
                {currentCardIndex + 1} / {filteredVocabulary.length}
              </span>
            </div>
            <Progress value={progress} className="h-2" />
          </div>

          {/* Session Controls */}
          {!isSessionActive && currentCardIndex === 0 ? (
            <div className="text-center mb-8">
              <h1 className="text-2xl font-bold text-gray-800 mb-2">Sẵn sàng học?</h1>
              <p className="text-gray-600 mb-6">Session học từ vựng 5 phút</p>
              <Button onClick={startSession} size="lg" className="bg-purple-600 hover:bg-purple-700">
                <Play className="w-4 h-4 mr-2" />
                Bắt đầu học
              </Button>
            </div>
          ) : (
            <div className="flex justify-between items-center mb-6">
              <Button variant="outline" size="sm" onClick={toggleSession} className="flex items-center gap-2">
                {isSessionActive ? <Pause className="w-4 h-4" /> : <Play className="w-4 h-4" />}
                {formatTime(sessionTime)}
              </Button>
              <Badge variant="secondary" className="flex items-center gap-1">
                <CheckCircle className="w-3 h-3 text-green-500" />
                {knownCards.length} đã biết
              </Badge>
            </div>
          )}

          {/* Flashcard */}
          <div className="mb-8">
            <Card className="h-80 cursor-pointer transition-all duration-300 hover:shadow-lg" onClick={handleFlip}>
              <CardContent className="h-full flex flex-col justify-center items-center p-6 text-center">
                {!isFlipped ? (
                  <div className="space-y-4">
                    <div className="flex items-center justify-center gap-2 mb-4">
                      <Badge variant="outline" className="text-xs">
                        {currentCard.difficulty === "beginner"
                          ? "Cơ bản"
                          : currentCard.difficulty === "intermediate"
                            ? "Trung bình"
                            : "Nâng cao"}
                      </Badge>
                      <Badge variant="outline" className="text-xs">
                        {currentCard.category}
                      </Badge>
                    </div>
                    <h2 className="text-3xl font-bold text-gray-800">{currentCard.word}</h2>
                    <p className="text-sm text-gray-500">{currentCard.pronunciation}</p>
                    <Button
                      variant="ghost"
                      size="sm"
                      onClick={(e) => {
                        e.stopPropagation()
                        speakWord(currentCard.word)
                      }}
                      className="flex items-center gap-2"
                    >
                      <Volume2 className="w-4 h-4" />
                      Phát âm
                    </Button>
                    <p className="text-gray-500">Nhấn để xem nghĩa</p>
                  </div>
                ) : (
                  <div className="space-y-4">
                    <h3 className="text-xl font-semibold text-gray-700">{currentCard.word}</h3>
                    <p className="text-lg text-gray-800 leading-relaxed">{currentCard.meaning}</p>
                    <div className="bg-gray-50 rounded-lg p-3 mt-4">
                      <p className="text-sm text-gray-600 italic">"{currentCard.example}"</p>
                    </div>
                  </div>
                )}
              </CardContent>
            </Card>
          </div>

          {/* Action Buttons */}
          {isFlipped && (
            <div className="grid grid-cols-2 gap-4 mb-6">
              <Button
                variant="outline"
                onClick={handleUnknown}
                className="h-12 border-orange-200 text-orange-600 hover:bg-orange-50"
              >
                <XCircle className="w-4 h-4 mr-2" />
                Cần ôn lại
              </Button>
              <Button onClick={handleKnown} className="h-12 bg-green-600 hover:bg-green-700">
                <CheckCircle className="w-4 h-4 mr-2" />
                Đã biết
              </Button>
            </div>
          )}

          {/* Quick Actions */}
          <div className="flex justify-center gap-4">
            <Button variant="ghost" size="sm" onClick={handleFlip} className="flex items-center gap-2">
              <RotateCcw className="w-4 h-4" />
              Lật thẻ
            </Button>
            <Button variant="ghost" size="sm" onClick={nextCard} className="flex items-center gap-2">
              <SkipForward className="w-4 h-4" />
              Bỏ qua
            </Button>
          </div>

          {/* Learning Tips */}
          <div className="mt-8 bg-white/50 rounded-lg p-4 text-center">
            <p className="text-sm text-gray-600">
              💡 <strong>Mẹo học:</strong> Chỉ 5 phút mỗi ngày = 1,800+ từ mỗi năm!
            </p>
          </div>
        </div>
      </main>
    </div>
  )
}
