"use client"

import { useState, useEffect } from "react"
import { Link } from "react-router-dom"
import { Layout } from "@/components/Layout/Layout"
import { FlashcardComponent } from "@/components/Flashcard/FlashcardComponent"
import { QuizMode } from "@/components/Quiz/QuizMode"
import { TypingMode } from "@/components/Quiz/TypingMode"
import { SearchBar } from "@/components/Search/SearchBar"
import { Button } from "@/components/ui/button"
import { Progress } from "@/components/ui/progress"
import { Badge } from "@/components/ui/badge"
import { Card, CardContent } from "@/components/ui/card"
import { useAppStore } from "@/store/useAppStore"
import { formatTime } from "@/lib/utils"
import { vocabularyCategories } from "@/data/vocabulary"
import {
  ArrowLeft,
  CheckCircle,
  XCircle,
  Flame,
  Trophy,
  Play,
  Pause,
  SkipForward,
  RotateCcw,
  BookOpen,
  Gamepad2,
  Keyboard,
  Search,
} from "lucide-react"

type StudyMode = "flashcard" | "quiz" | "typing"

export function StudyPage() {
  const {
    vocabulary,
    userProgress,
    currentSession,
    startStudySession,
    endStudySession,
    markWordAsLearned,
    markWordForReview,
    updateUserProgress,
    updateWordReview,
    getWordsForReview,
  } = useAppStore()

  const [studyMode, setStudyMode] = useState<StudyMode>("flashcard")
  const [currentCardIndex, setCurrentCardIndex] = useState(0)
  const [isFlipped, setIsFlipped] = useState(false)
  const [sessionTime, setSessionTime] = useState(0)
  const [selectedDifficulty, setSelectedDifficulty] = useState("all")
  const [selectedCategory, setSelectedCategory] = useState("all")
  const [knownCards, setKnownCards] = useState<number[]>([])
  const [unknownCards, setUnknownCards] = useState<number[]>([])
  const [showSearch, setShowSearch] = useState(false)
  const [useSpacedRepetition, setUseSpacedRepetition] = useState(false)

  // Get filtered vocabulary
  const getFilteredVocabulary = () => {
    if (useSpacedRepetition) {
      return getWordsForReview()
    }

    return vocabulary.filter((word) => {
      const difficultyMatch = selectedDifficulty === "all" || word.difficulty === selectedDifficulty
      const categoryMatch = selectedCategory === "all" || word.category === selectedCategory
      return difficultyMatch && categoryMatch
    })
  }

  const filteredVocabulary = getFilteredVocabulary()
  const currentCard = filteredVocabulary[currentCardIndex]
  const progress = filteredVocabulary.length > 0 ? ((currentCardIndex + 1) / filteredVocabulary.length) * 100 : 0

  // Timer for session
  useEffect(() => {
    let interval: NodeJS.Timeout
    if (currentSession.isActive) {
      interval = setInterval(() => {
        setSessionTime((prev) => prev + 1)
      }, 1000)
    }
    return () => clearInterval(interval)
  }, [currentSession.isActive])

  const handleFlip = () => {
    setIsFlipped(!isFlipped)
  }

  const handleKnown = () => {
    if (currentCard && !knownCards.includes(currentCard.id)) {
      setKnownCards([...knownCards, currentCard.id])
      markWordAsLearned(currentCard.id)

      // Update spaced repetition data
      if (useSpacedRepetition) {
        updateWordReview(currentCard.id, 4) // Good quality rating
      }
    }
    nextCard()
  }

  const handleUnknown = () => {
    if (currentCard && !unknownCards.includes(currentCard.id)) {
      setUnknownCards([...unknownCards, currentCard.id])
      markWordForReview(currentCard.id)

      // Update spaced repetition data
      if (useSpacedRepetition) {
        updateWordReview(currentCard.id, 1) // Poor quality rating
      }
    }
    nextCard()
  }

  const nextCard = () => {
    setIsFlipped(false)
    if (currentCardIndex < filteredVocabulary.length - 1) {
      setCurrentCardIndex((prev) => prev + 1)
    } else {
      handleEndSession()
    }
  }

  const handleStartSession = () => {
    startStudySession(selectedDifficulty, selectedCategory)
    setCurrentCardIndex(0)
    setIsFlipped(false)
    setSessionTime(0)
    setKnownCards([])
    setUnknownCards([])
  }

  const handleEndSession = () => {
    endStudySession()
    const newWordsToday = userProgress.wordsToday + knownCards.length
    if (newWordsToday >= userProgress.dailyGoal) {
      updateUserProgress({
        currentStreak: userProgress.currentStreak + 1,
        longestStreak: Math.max(userProgress.longestStreak, userProgress.currentStreak + 1),
      })
    }
  }

  const handleQuizComplete = (results: any) => {
    // Handle quiz completion
    const correctAnswers = results.correctAnswers
    const pointsEarned = correctAnswers * 15 // More points for quiz mode

    updateUserProgress({
      totalPoints: userProgress.totalPoints + pointsEarned,
      wordsToday: userProgress.wordsToday + correctAnswers,
      experience: userProgress.experience + pointsEarned,
    })

    // Update spaced repetition for quiz results
    results.results.forEach((result: any) => {
      const quality = result.correct ? 4 : 1
      updateWordReview(result.wordId, quality)
    })

    handleEndSession()
  }

  const handleTypingComplete = (results: any) => {
    // Handle typing completion
    const correctWords = results.correctWords
    const pointsEarned = correctWords * 20 // Most points for typing mode

    updateUserProgress({
      totalPoints: userProgress.totalPoints + pointsEarned,
      wordsToday: userProgress.wordsToday + correctWords,
      experience: userProgress.experience + pointsEarned,
    })

    // Update spaced repetition for typing results
    results.results.forEach((result: any) => {
      const quality = result.correct ? 5 : 1 // Typing gets highest quality when correct
      updateWordReview(result.wordId, quality)
    })

    handleEndSession()
  }

  const handleWordSelect = (word: any) => {
    const wordIndex = filteredVocabulary.findIndex((w) => w.id === word.id)
    if (wordIndex !== -1) {
      setCurrentCardIndex(wordIndex)
      setShowSearch(false)
    }
  }

  const speakWord = (text: string) => {
    if ("speechSynthesis" in window) {
      const utterance = new SpeechSynthesisUtterance(text)
      utterance.lang = "en-US"
      speechSynthesis.speak(utterance)
    }
  }

  // Session complete screen
  if (currentSession.isActive && currentCardIndex >= filteredVocabulary.length) {
    return (
      <Layout>
        <div className="max-w-md mx-auto pt-8">
          <div className="text-center space-y-6 animate-fade-in">
            <div className="text-6xl">🎉</div>
            <h1 className="text-2xl font-bold text-gray-800">Hoàn thành session!</h1>
            <Card>
              <CardContent className="p-6 space-y-4">
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
                  <span className="font-semibold text-purple-600">
                    +{knownCards.length * (studyMode === "typing" ? 20 : studyMode === "quiz" ? 15 : 10)}
                  </span>
                </div>
              </CardContent>
            </Card>
            <div className="space-y-3">
              <Button onClick={handleStartSession} className="w-full bg-purple-600 hover:bg-purple-700">
                Bắt đầu session mới
              </Button>
              <Link to="/" className="block">
                <Button variant="outline" className="w-full">
                  Quay về trang chủ
                </Button>
              </Link>
            </div>
          </div>
        </div>
      </Layout>
    )
  }

  // No vocabulary available
  if (filteredVocabulary.length === 0) {
    return (
      <Layout>
        <div className="max-w-md mx-auto pt-8 text-center">
          <h1 className="text-2xl font-bold text-gray-800 mb-4">
            {useSpacedRepetition ? "Không có từ cần ôn tập" : "Không có từ vựng"}
          </h1>
          <p className="text-gray-600 mb-6">
            {useSpacedRepetition
              ? "Tuyệt vời! Bạn đã ôn tập hết các từ cần thiết hôm nay."
              : "Vui lòng chọn danh mục hoặc độ khó khác"}
          </p>
          <Link to="/">
            <Button>Quay về trang chủ</Button>
          </Link>
        </div>
      </Layout>
    )
  }

  // Quiz Mode
  if (studyMode === "quiz" && currentSession.isActive) {
    return (
      <Layout>
        <QuizMode
          words={filteredVocabulary.slice(0, 10)}
          onComplete={handleQuizComplete}
          onExit={() => setStudyMode("flashcard")}
        />
      </Layout>
    )
  }

  // Typing Mode
  if (studyMode === "typing" && currentSession.isActive) {
    return (
      <Layout>
        <TypingMode
          words={filteredVocabulary.slice(0, 10)}
          onComplete={handleTypingComplete}
          onExit={() => setStudyMode("flashcard")}
        />
      </Layout>
    )
  }

  return (
    <Layout>
      <div className="max-w-md mx-auto">
        {/* Header */}
        <div className="flex items-center justify-between mb-6">
          <Link to="/">
            <Button variant="ghost" size="sm">
              <ArrowLeft className="w-4 h-4 mr-2" />
              Quay lại
            </Button>
          </Link>
          <div className="flex items-center gap-4">
            <Button variant="ghost" size="sm" onClick={() => setShowSearch(!showSearch)}>
              <Search className="w-4 h-4" />
            </Button>
            <div className="flex items-center gap-2">
              <Flame className="w-5 h-5 text-orange-500" />
              <span className="font-semibold text-orange-600">{userProgress.currentStreak}</span>
            </div>
            <div className="flex items-center gap-2">
              <Trophy className="w-5 h-5 text-yellow-500" />
              <span className="font-semibold text-yellow-600">{userProgress.totalPoints}</span>
            </div>
          </div>
        </div>

        {/* Search Bar */}
        {showSearch && (
          <div className="mb-6 animate-fade-in">
            <SearchBar vocabulary={vocabulary} onWordSelect={handleWordSelect} className="mb-4" />
          </div>
        )}

        {/* Study Mode Selection */}
        {!currentSession.isActive && (
          <div className="mb-6 space-y-4 animate-fade-in">
            <div>
              <label className="text-sm font-medium text-gray-700 mb-2 block">Chế độ học</label>
              <div className="grid grid-cols-3 gap-2">
                <Button
                  variant={studyMode === "flashcard" ? "default" : "outline"}
                  size="sm"
                  onClick={() => setStudyMode("flashcard")}
                  className="flex flex-col items-center gap-1 h-auto py-3"
                >
                  <BookOpen className="w-4 h-4" />
                  <span className="text-xs">Flashcard</span>
                </Button>
                <Button
                  variant={studyMode === "quiz" ? "default" : "outline"}
                  size="sm"
                  onClick={() => setStudyMode("quiz")}
                  className="flex flex-col items-center gap-1 h-auto py-3"
                >
                  <Gamepad2 className="w-4 h-4" />
                  <span className="text-xs">Quiz</span>
                </Button>
                <Button
                  variant={studyMode === "typing" ? "default" : "outline"}
                  size="sm"
                  onClick={() => setStudyMode("typing")}
                  className="flex flex-col items-center gap-1 h-auto py-3"
                >
                  <Keyboard className="w-4 h-4" />
                  <span className="text-xs">Typing</span>
                </Button>
              </div>
            </div>

            {/* Spaced Repetition Toggle */}
            <div className="flex items-center justify-between p-3 bg-blue-50 rounded-lg">
              <div>
                <h4 className="font-medium text-blue-800">Spaced Repetition</h4>
                <p className="text-xs text-blue-600">Ôn tập thông minh theo khoa học</p>
              </div>
              <Button
                variant={useSpacedRepetition ? "default" : "outline"}
                size="sm"
                onClick={() => setUseSpacedRepetition(!useSpacedRepetition)}
              >
                {useSpacedRepetition ? "Đang bật" : "Tắt"}
              </Button>
            </div>

            {/* Filters (only show if not using spaced repetition) */}
            {!useSpacedRepetition && (
              <>
                <div>
                  <label className="text-sm font-medium text-gray-700 mb-2 block">Độ khó</label>
                  <div className="flex gap-2 flex-wrap">
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

                <div>
                  <label className="text-sm font-medium text-gray-700 mb-2 block">Danh mục</label>
                  <div className="flex gap-2 flex-wrap">
                    <Button
                      variant={selectedCategory === "all" ? "default" : "outline"}
                      size="sm"
                      onClick={() => setSelectedCategory("all")}
                    >
                      Tất cả
                    </Button>
                    {vocabularyCategories.slice(0, 4).map((category) => (
                      <Button
                        key={category}
                        variant={selectedCategory === category ? "default" : "outline"}
                        size="sm"
                        onClick={() => setSelectedCategory(category)}
                      >
                        {category}
                      </Button>
                    ))}
                  </div>
                </div>
              </>
            )}
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
        {!currentSession.isActive ? (
          <div className="text-center mb-8 animate-fade-in">
            <h1 className="text-2xl font-bold text-gray-800 mb-2">Sẵn sàng học?</h1>
            <p className="text-gray-600 mb-6">
              {studyMode === "flashcard" && `Session học từ vựng với ${filteredVocabulary.length} từ`}
              {studyMode === "quiz" && "Quiz 10 câu hỏi với timer"}
              {studyMode === "typing" && "Luyện typing với 10 từ"}
              {useSpacedRepetition && " (Spaced Repetition)"}
            </p>
            <Button onClick={handleStartSession} size="lg" className="bg-purple-600 hover:bg-purple-700">
              <Play className="w-4 h-4 mr-2" />
              Bắt đầu học
            </Button>
          </div>
        ) : (
          <div className="flex justify-between items-center mb-6">
            <Button variant="outline" size="sm" className="flex items-center gap-2">
              <Pause className="w-4 h-4" />
              {formatTime(sessionTime)}
            </Button>
            <Badge variant="secondary" className="flex items-center gap-1">
              <CheckCircle className="w-3 h-3 text-green-500" />
              {knownCards.length} đã biết
            </Badge>
          </div>
        )}

        {/* Flashcard (only in flashcard mode) */}
        {currentCard && currentSession.isActive && studyMode === "flashcard" && (
          <div className="mb-8">
            <FlashcardComponent
              word={currentCard}
              isFlipped={isFlipped}
              onFlip={handleFlip}
              onSpeak={speakWord}
              className="animate-fade-in"
            />
          </div>
        )}

        {/* Action Buttons (only in flashcard mode) */}
        {isFlipped && currentSession.isActive && studyMode === "flashcard" && (
          <div className="grid grid-cols-2 gap-4 mb-6 animate-fade-in">
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

        {/* Quick Actions (only in flashcard mode) */}
        {currentSession.isActive && studyMode === "flashcard" && (
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
        )}

        {/* Learning Tips */}
        <div className="mt-8 bg-white/50 rounded-lg p-4 text-center">
          <p className="text-sm text-gray-600">
            💡 <strong>Mẹo học:</strong>
            {studyMode === "flashcard" && " Spaced Repetition giúp nhớ lâu hơn!"}
            {studyMode === "quiz" && " Quiz mode giúp kiểm tra kiến thức!"}
            {studyMode === "typing" && " Typing mode cải thiện tốc độ và chính xác!"}
          </p>
        </div>
      </div>
    </Layout>
  )
}
