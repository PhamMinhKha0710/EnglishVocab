"use client"

import { useState, useEffect } from "react"
import { Link } from "react-router-dom"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardFooter, CardHeader, CardTitle } from "@/components/ui/card"
import { Progress } from "@/components/ui/progress"
import { Badge } from "@/components/ui/badge"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"
import { FlashCard3D } from "@/components/flashcards/flashcard-3d"
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
  Clock,
  Bookmark,
  Lightbulb,
  BarChart,
  Brain,
  List,
  ListChecks,
  Info,
  ExternalLink,
  Zap
} from "lucide-react"
import { vocabularyData } from "@/lib/vocabulary-data"

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
  const [isFlipping, setIsFlipping] = useState(false)
  const [studyMode, setStudyMode] = useState<'flashcard' | 'quiz' | 'typing'>('flashcard')

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
    setIsFlipping(true)
    setTimeout(() => {
      setIsFlipped(!isFlipped)
      setTimeout(() => {
        setIsFlipping(false)
      }, 150)
    }, 150)
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

  // Get difficulty color
  const getDifficultyColor = (difficulty: string) => {
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

  // Get difficulty label
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

  // Get study mode icon
  const getStudyModeIcon = (mode: string) => {
    switch (mode) {
      case 'flashcard':
        return <RotateCcw className="w-4 h-4 mr-2" />
      case 'quiz':
        return <ListChecks className="w-4 h-4 mr-2" />
      case 'typing':
        return <Zap className="w-4 h-4 mr-2" />
      default:
        return <RotateCcw className="w-4 h-4 mr-2" />
    }
  }

  if (filteredVocabulary.length === 0) {
    return (
      <div className="min-h-screen bg-gradient-to-br from-indigo-50 to-purple-50">
        <main className="p-6">
          <div className="max-w-md mx-auto pt-8 text-center">
            <h1 className="text-2xl font-bold text-gray-800 mb-4">Không có từ vựng</h1>
            <p className="text-gray-600 mb-6">Vui lòng chọn danh mục hoặc độ khó khác</p>
            <Link to="/">
              <Button>Quay về trang chủ</Button>
            </Link>
          </div>
        </main>
      </div>
    )
  }

  if (currentCardIndex >= filteredVocabulary.length) {
    return (
      <div className="min-h-screen bg-gradient-to-br from-indigo-50 to-purple-50">
        <main className="p-6">
          <div className="max-w-md mx-auto pt-8">
            <div className="text-center space-y-6">
              <div className="text-6xl">🎉</div>
              <h1 className="text-2xl font-bold text-gray-800">Hoàn thành session!</h1>
              
              <Card className="border-2 border-purple-200">
                <CardHeader className="bg-purple-50 pb-2">
                  <CardTitle className="flex items-center justify-center text-purple-800">
                    <Trophy className="w-5 h-5 mr-2 text-yellow-500" />
                    Kết quả học tập
                  </CardTitle>
                </CardHeader>
                <CardContent className="p-6 space-y-4">
                  <div className="flex justify-between items-center">
                    <span className="flex items-center text-gray-600">
                      <Clock className="w-4 h-4 mr-2" />
                      Thời gian:
                    </span>
                    <span className="font-semibold">{formatTime(sessionTime)}</span>
                  </div>
                  <div className="flex justify-between items-center">
                    <span className="flex items-center text-gray-600">
                      <CheckCircle className="w-4 h-4 mr-2 text-green-500" />
                      Đã biết:
                    </span>
                    <span className="font-semibold text-green-600">{knownCards.length}</span>
                  </div>
                  <div className="flex justify-between items-center">
                    <span className="flex items-center text-gray-600">
                      <Bookmark className="w-4 h-4 mr-2 text-orange-500" />
                      Cần ôn:
                    </span>
                    <span className="font-semibold text-orange-600">{unknownCards.length}</span>
                  </div>
                  <div className="flex justify-between items-center">
                    <span className="flex items-center text-gray-600">
                      <Trophy className="w-4 h-4 mr-2 text-yellow-500" />
                      Điểm kiếm được:
                    </span>
                    <span className="font-semibold text-purple-600">+{knownCards.length * 10}</span>
                  </div>
                  
                  <div className="mt-4 pt-4 border-t border-gray-100">
                    <div className="text-sm font-medium text-gray-700 mb-2">Tiến độ học tập:</div>
                    <div className="flex items-center gap-2">
                      <div className="flex-grow">
                        <Progress value={(knownCards.length / filteredVocabulary.length) * 100} 
                                 className="h-2 bg-gray-200" 
                                 indicatorClassName="bg-green-500" />
                      </div>
                      <div className="text-sm font-medium text-green-600">
                        {Math.round((knownCards.length / filteredVocabulary.length) * 100)}%
                      </div>
                    </div>
                  </div>
                </CardContent>
              </Card>
              
              <Card className="border border-blue-100">
                <CardHeader className="bg-blue-50 pb-2">
                  <CardTitle className="flex items-center justify-center text-blue-800 text-sm">
                    <Lightbulb className="w-4 h-4 mr-1 text-yellow-500" />
                    Mẹo học tập
                  </CardTitle>
                </CardHeader>
                <CardContent className="p-4 text-sm">
                  <p className="text-gray-600">Để học hiệu quả hơn, hãy thực hành các từ "Cần ôn" mỗi ngày và tạo câu ví dụ với các từ mới học.</p>
                </CardContent>
              </Card>
              
              <div className="space-y-3">
                <Button onClick={startSession} className="w-full bg-purple-600 hover:bg-purple-700">
                  <Play className="w-4 h-4 mr-2" />
                  Bắt đầu session mới
                </Button>
                <Link to="/" className="block">
                  <Button variant="outline" className="w-full">
                    <ArrowLeft className="w-4 h-4 mr-2" />
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
    <div className="min-h-screen bg-gradient-to-br from-indigo-50 to-purple-50 pb-6">
      <main className="container px-4 py-6 max-w-4xl mx-auto">
        {/* Header */}
        <div className="flex items-center justify-between mb-6">
          <Link to="/">
            <Button variant="ghost" size="sm">
              <ArrowLeft className="w-4 h-4 mr-2" />
              Quay lại
            </Button>
          </Link>
          <div className="flex items-center gap-4">
            <div className="flex items-center gap-2">
              <div className="bg-white p-1 px-2 rounded-md flex items-center shadow-sm">
                <Flame className="w-5 h-5 text-orange-500 mr-1" />
                <span className="font-semibold text-orange-600">{streak}</span>
              </div>
            </div>
            <div className="flex items-center gap-2">
              <div className="bg-white p-1 px-2 rounded-md flex items-center shadow-sm">
                <Trophy className="w-5 h-5 text-yellow-500 mr-1" />
                <span className="font-semibold text-yellow-600">{totalPoints}</span>
              </div>
            </div>
          </div>
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
          {/* Side panel */}
          <div className="lg:col-span-1">
            <div className="space-y-6">
              {/* Study Mode Selection */}
              <Card className="border-2 border-purple-100">
                <CardHeader className="pb-2">
                  <CardTitle className="text-lg font-bold text-purple-900 flex items-center">
                    <Brain className="w-5 h-5 text-purple-700 mr-2" />
                    Chế độ học
                  </CardTitle>
                </CardHeader>
                <CardContent className="pt-2">
                  <div className="space-y-2">
                    <div className="grid grid-cols-1 gap-2">
                      <Button
                        variant={studyMode === 'flashcard' ? 'default' : 'outline'} 
                        onClick={() => setStudyMode('flashcard')}
                        className={`justify-start ${studyMode === 'flashcard' ? 'bg-purple-600 hover:bg-purple-700' : ''}`}
                      >
                        <RotateCcw className="w-4 h-4 mr-2" />
                        Flashcard
                      </Button>
                      <Button
                        variant={studyMode === 'quiz' ? 'default' : 'outline'} 
                        onClick={() => setStudyMode('quiz')}
                        className={`justify-start ${studyMode === 'quiz' ? 'bg-purple-600 hover:bg-purple-700' : ''}`}
                        disabled={isSessionActive}
                      >
                        <ListChecks className="w-4 h-4 mr-2" />
                        Trắc nghiệm
                      </Button>
                      <Button
                        variant={studyMode === 'typing' ? 'default' : 'outline'} 
                        onClick={() => setStudyMode('typing')}
                        className={`justify-start ${studyMode === 'typing' ? 'bg-purple-600 hover:bg-purple-700' : ''}`}
                        disabled={isSessionActive}
                      >
                        <Zap className="w-4 h-4 mr-2" />
                        Gõ từ
                      </Button>
                    </div>
                  </div>
                </CardContent>
              </Card>
                
              {/* Filters */}
              {!isSessionActive && (
                <Card className="border-2 border-blue-100">
                  <CardHeader className="pb-2">
                    <CardTitle className="text-lg font-bold text-blue-900 flex items-center">
                      <List className="w-5 h-5 text-blue-700 mr-2" />
                      Bộ lọc từ vựng
                    </CardTitle>
                  </CardHeader>
                  <CardContent className="space-y-4 pt-2">
                    <div>
                      <label className="text-sm font-medium text-gray-700 mb-2 block">Độ khó</label>
                      <div className="flex gap-2 flex-wrap">
                        {["all", "beginner", "intermediate", "advanced"].map((level) => (
                          <Button
                            key={level}
                            variant={selectedDifficulty === level ? "default" : "outline"}
                            size="sm"
                            onClick={() => setSelectedDifficulty(level)}
                            className={selectedDifficulty === level ? "bg-blue-600 hover:bg-blue-700" : ""}
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
                          className={selectedCategory === "all" ? "bg-blue-600 hover:bg-blue-700" : ""}
                        >
                          Tất cả
                        </Button>
                        <Button
                          variant={selectedCategory === "business" ? "default" : "outline"}
                          size="sm"
                          onClick={() => setSelectedCategory("business")}
                          className={selectedCategory === "business" ? "bg-blue-600 hover:bg-blue-700" : ""}
                        >
                          Kinh doanh
                        </Button>
                        <Button
                          variant={selectedCategory === "technology" ? "default" : "outline"}
                          size="sm"
                          onClick={() => setSelectedCategory("technology")}
                          className={selectedCategory === "technology" ? "bg-blue-600 hover:bg-blue-700" : ""}
                        >
                          Công nghệ
                        </Button>
                      </div>
                    </div>
                    
                    {!isSessionActive && currentCardIndex === 0 && (
                      <Button onClick={startSession} className="w-full bg-green-600 hover:bg-green-700 mt-4">
                        <Play className="w-4 h-4 mr-2" />
                        Bắt đầu học
                      </Button>
                    )}
                  </CardContent>
                </Card>
              )}

              {/* Session Stats */}
              {isSessionActive && (
                <Card className="border-2 border-green-100">
                  <CardHeader className="pb-2">
                    <CardTitle className="text-lg font-bold text-green-900 flex items-center">
                      <BarChart className="w-5 h-5 text-green-700 mr-2" />
                      Thông tin session
                    </CardTitle>
                  </CardHeader>
                  <CardContent className="space-y-3 pt-2">
                    <div className="flex justify-between items-center">
                      <span className="text-gray-600 flex items-center">
                        <Clock className="w-4 h-4 mr-1 text-gray-500" />
                        Thời gian:
                      </span>
                      <span className="font-semibold">{formatTime(sessionTime)}</span>
                    </div>
                    <div className="flex justify-between items-center">
                      <span className="text-gray-600 flex items-center">
                        <CheckCircle className="w-4 h-4 mr-1 text-green-500" />
                        Đã biết:
                      </span>
                      <span className="font-semibold text-green-600">{knownCards.length}</span>
                    </div>
                    <div className="flex justify-between items-center">
                      <span className="text-gray-600 flex items-center">
                        <XCircle className="w-4 h-4 mr-1 text-orange-500" />
                        Cần ôn:
                      </span>
                      <span className="font-semibold text-orange-600">{unknownCards.length}</span>
                    </div>
                    <div className="flex justify-between items-center">
                      <span className="text-gray-600 flex items-center">
                        <Trophy className="w-4 h-4 mr-1 text-purple-500" />
                        Tiến độ:
                      </span>
                      <span className="font-semibold text-purple-600">
                        {currentCardIndex + 1}/{filteredVocabulary.length}
                      </span>
                    </div>
                  </CardContent>
                </Card>
              )}
              
              {/* Help Card */}
              <Card className="border border-gray-200 bg-white/50">
                <CardHeader className="pb-2">
                  <CardTitle className="text-sm font-bold text-gray-800 flex items-center">
                    <Info className="w-4 h-4 mr-1.5 text-blue-500" />
                    Hướng dẫn học
                  </CardTitle>
                </CardHeader>
                <CardContent className="pt-2 text-sm text-gray-600">
                  <ul className="space-y-1 list-disc pl-5">
                    <li>Nhấp vào thẻ để xem nghĩa</li>
                    <li>Chọn "<span className="text-green-600 font-medium">Đã biết</span>" nếu bạn đã thuộc từ này</li>
                    <li>Chọn "<span className="text-red-600 font-medium">Chưa biết</span>" nếu bạn cần ôn lại</li>
                  </ul>
                </CardContent>
              </Card>
            </div>
          </div>
          
          {/* Main content */}
          <div className="lg:col-span-2 space-y-6">
            {/* Welcome Card */}
            {!isSessionActive && currentCardIndex === 0 ? (
              <Card className="text-center border-2 border-purple-200">
                <CardHeader className="pb-2 bg-purple-50">
                  <CardTitle className="text-2xl font-bold text-purple-800">Sẵn sàng học?</CardTitle>
                </CardHeader>
                <CardContent className="p-8">
                  <div className="mb-6">
                    <Lightbulb className="h-16 w-16 text-yellow-500 mx-auto mb-4" />
                    <p className="text-gray-600 mb-6">Bắt đầu session học từ vựng với {filteredVocabulary.length} từ</p>
                    
                    <div className="grid grid-cols-3 gap-4 mb-6">
                      <div className="bg-purple-50 p-4 rounded-lg">
                        <div className="text-purple-800 font-bold text-lg">{filteredVocabulary.length}</div>
                        <div className="text-xs text-gray-600">Từ vựng</div>
                      </div>
                      <div className="bg-blue-50 p-4 rounded-lg">
                        <div className="text-blue-800 font-bold text-lg">5</div>
                        <div className="text-xs text-gray-600">Phút</div>
                      </div>
                      <div className="bg-green-50 p-4 rounded-lg">
                        <div className="text-green-800 font-bold text-lg">+{filteredVocabulary.length * 10}</div>
                        <div className="text-xs text-gray-600">Điểm tối đa</div>
                      </div>
                    </div>
                  </div>
                  
                  <Button onClick={startSession} size="lg" className="bg-purple-600 hover:bg-purple-700 w-full">
                    <Play className="w-5 h-5 mr-2" />
                    Bắt đầu học ngay
                  </Button>
                </CardContent>
              </Card>
            ) : (
              <>
                {/* Session Controls */}
                {isSessionActive && (
                  <Card className="border border-gray-200">
                    <CardContent className="p-4">
                      <div className="flex items-center justify-between">
                        <div className="flex-grow mr-4">
                          <div className="flex justify-between text-sm text-gray-600 mb-2">
                            <span className="flex items-center">
                              <BarChart className="w-3 h-3 mr-1" />
                              Tiến độ học tập
                            </span>
                            <span className="font-medium">
                              {currentCardIndex + 1} / {filteredVocabulary.length}
                            </span>
                          </div>
                          <Progress value={progress} className="h-2 bg-gray-200" indicatorClassName="bg-purple-600" />
                        </div>
                        
                        <div className="flex gap-2">
                          <Button variant="outline" size="sm" onClick={toggleSession} className="flex items-center">
                            {isSessionActive ? (
                              <>
                                <Pause className="w-4 h-4 mr-1" /> Tạm dừng
                              </>
                            ) : (
                              <>
                                <Play className="w-4 h-4 mr-1" /> Tiếp tục
                              </>
                            )}
                          </Button>
                          <Button variant="outline" size="sm" onClick={nextCard} className="flex items-center">
                            <SkipForward className="w-4 h-4 mr-1" /> Bỏ qua
                          </Button>
                        </div>
                      </div>
                    </CardContent>
                  </Card>
                )}

                {/* Flashcard */}
                {isSessionActive && studyMode === 'flashcard' && (
                  <FlashCard3D 
                    id={currentCard.id}
                    word={currentCard.word}
                    meaning={currentCard.meaning}
                    pronunciation={currentCard.pronunciation}
                    example={currentCard.example}
                    category={currentCard.category}
                    difficulty={currentCard.difficulty}
                    onKnown={handleKnown}
                    onUnknown={handleUnknown}
                    onSpeak={speakWord}
                  />
                )}
              </>
            )}
          </div>
        </div>
      </main>
    </div>
  )
}
