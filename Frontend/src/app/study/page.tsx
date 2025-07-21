"use client"

import { useState, useEffect, useCallback } from "react"
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
  Zap,
  Loader2,
  Star
} from "lucide-react"
import { vocabularyData } from "@/lib/vocabulary-data"
import { apiService, Category, DifficultyLevel } from "@/services/api-service"
import { useAuth } from "@/contexts/auth-context"

// Tạo cache cho API data
const API_CACHE = {
  categories: null as Category[] | null,
  difficultyLevels: null as DifficultyLevel[] | null,
  lastFetchTime: {
    categories: 0,
    difficultyLevels: 0
  },
  CACHE_DURATION: 5 * 60 * 1000 // 5 phút
};

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
  const [categories, setCategories] = useState<Category[]>([])
  const [loadingCategories, setLoadingCategories] = useState<boolean>(false)
  const [categoryError, setCategoryError] = useState<string | null>(null)
  const [difficultyLevels, setDifficultyLevels] = useState<DifficultyLevel[]>([])
  const [loadingDifficultyLevels, setLoadingDifficultyLevels] = useState<boolean>(false)
  const [difficultyLevelError, setDifficultyLevelError] = useState<string | null>(null)
  const { user } = useAuth()
  const [dataLoaded, setDataLoaded] = useState(false)

  // Hàm fetch data với caching để tránh gọi API quá nhiều lần
  const fetchDataWithCache = useCallback(async () => {
    // Chỉ fetch khi có user và token
    if (!user?.accessToken || dataLoaded) return;

    const currentTime = Date.now();
    const fetchPromises = [];
    let shouldUpdateState = false;

    // Fetch categories nếu cần
    if (
      !API_CACHE.categories || 
      (currentTime - API_CACHE.lastFetchTime.categories > API_CACHE.CACHE_DURATION)
    ) {
      setLoadingCategories(true);
      const categoriesPromise = apiService.getCategories(user.accessToken)
        .then(data => {
          API_CACHE.categories = data;
          API_CACHE.lastFetchTime.categories = currentTime;
          setCategories(data);
          shouldUpdateState = true;
        })
        .catch(error => {
          setCategoryError(error.message);
        })
        .finally(() => {
          setLoadingCategories(false);
        });
      fetchPromises.push(categoriesPromise);
    } else if (API_CACHE.categories) {
      // Sử dụng cache nếu có
      setCategories(API_CACHE.categories);
    }

    // Fetch difficulty levels nếu cần
    if (
      !API_CACHE.difficultyLevels || 
      (currentTime - API_CACHE.lastFetchTime.difficultyLevels > API_CACHE.CACHE_DURATION)
    ) {
      setLoadingDifficultyLevels(true);
      const difficultyPromise = apiService.getDifficultyLevels(user.accessToken)
        .then(data => {
          // Thêm log để debug dữ liệu
          console.log("Dữ liệu độ khó từ API:", data);
          if (data && data.length > 0) {
            console.log("Cấu trúc độ khó:", Object.keys(data[0]));
            console.log("Mẫu dữ liệu độ khó:", JSON.stringify(data[0]));
          }
          
          API_CACHE.difficultyLevels = data;
          API_CACHE.lastFetchTime.difficultyLevels = currentTime;
          setDifficultyLevels(data);
          shouldUpdateState = true;
        })
        .catch(error => {
          setDifficultyLevelError(error.message);
        })
        .finally(() => {
          setLoadingDifficultyLevels(false);
        });
      fetchPromises.push(difficultyPromise);
    } else if (API_CACHE.difficultyLevels) {
      // Sử dụng cache nếu có
      setDifficultyLevels(API_CACHE.difficultyLevels);
    }

    // Đợi cả hai promise hoàn thành
    if (fetchPromises.length > 0) {
      await Promise.all(fetchPromises);
      setDataLoaded(true);
    }
  }, [user, dataLoaded]);

  // Gọi fetch data một lần duy nhất khi component mount hoặc user thay đổi
  useEffect(() => {
    fetchDataWithCache();
  }, [fetchDataWithCache]);

  // Chuyển đổi category sang đúng định dạng cho so sánh
  const getCategoryValue = (categoryName: string) => {
    return categoryName.trim().toLowerCase();
  }

  // Hàm kiểm tra xem một từ vựng có thuộc danh mục được chọn không
  const matchesSelectedCategory = (wordCategory: string) => {
    if (selectedCategory === "all") return true;
    return getCategoryValue(wordCategory) === selectedCategory;
  };

  const filteredVocabulary = vocabularyData.filter((word) => {
    const difficultyMatch = selectedDifficulty === "all" || word.difficulty === selectedDifficulty
    const categoryMatch = selectedCategory === "all" || matchesSelectedCategory(word.category)
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
                        {/* Trạng thái đang tải */}
                        {loadingDifficultyLevels && (
                          <div className="flex items-center justify-center w-full py-2 text-blue-600">
                            <Loader2 className="w-4 h-4 mr-2 animate-spin" />
                            Đang tải cấp độ khó...
                          </div>
                        )}
                        
                        {/* Hiển thị lỗi nếu có */}
                        {difficultyLevelError && (
                          <div className="text-red-500 text-sm w-full py-2">
                            Lỗi: {difficultyLevelError}. Đang sử dụng cấp độ khó mặc định.
                          </div>
                        )}
                        
                        {/* Nút "Tất cả" luôn được hiển thị */}
                        <Button
                          variant={selectedDifficulty === "all" ? "default" : "outline"}
                          size="sm"
                          onClick={() => setSelectedDifficulty("all")}
                          className={selectedDifficulty === "all" ? "bg-blue-600 hover:bg-blue-700" : ""}
                        >
                          Tất cả
                        </Button>
                        
                        {/* Cấp độ khó từ API */}
                        {difficultyLevels && difficultyLevels.length > 0 ? (
                          difficultyLevels.map((difficulty) => {
                            // Lấy tên và giá trị của cấp độ khó từ API
                            const difficultyName = difficulty.name?.toLowerCase() || 
                              difficulty.Name?.toLowerCase() || 
                              String(difficulty.id).toLowerCase() || 
                              "unknown";
                            
                            // Hiển thị tên độ khó và giữ nguyên tên hiển thị
                            return (
                              <Button
                                key={difficulty.id}
                                variant={selectedDifficulty === difficultyName ? "default" : "outline"}
                                size="sm"
                                onClick={() => setSelectedDifficulty(difficultyName)}
                                className={selectedDifficulty === difficultyName ? "bg-blue-600 hover:bg-blue-700" : ""}
                              >
                                {difficulty.name || difficulty.Name || getDifficultyLabel(difficultyName)}
                              </Button>
                            );
                          })
                        ) : !loadingDifficultyLevels && !difficultyLevelError ? (
                          // Cấp độ khó mặc định nếu API không trả về dữ liệu
                          <>
                            <Button
                              variant={selectedDifficulty === "beginner" ? "default" : "outline"}
                              size="sm"
                              onClick={() => setSelectedDifficulty("beginner")}
                              className={selectedDifficulty === "beginner" ? "bg-blue-600 hover:bg-blue-700" : ""}
                            >
                              Cơ bản
                            </Button>
                            <Button
                              variant={selectedDifficulty === "intermediate" ? "default" : "outline"}
                              size="sm"
                              onClick={() => setSelectedDifficulty("intermediate")}
                              className={selectedDifficulty === "intermediate" ? "bg-blue-600 hover:bg-blue-700" : ""}
                            >
                              Trung bình
                            </Button>
                            <Button
                              variant={selectedDifficulty === "advanced" ? "default" : "outline"}
                              size="sm"
                              onClick={() => setSelectedDifficulty("advanced")}
                              className={selectedDifficulty === "advanced" ? "bg-blue-600 hover:bg-blue-700" : ""}
                            >
                              Nâng cao
                            </Button>
                          </>
                        ) : null}
                      </div>
                    </div>
                    <div>
                      <label className="text-sm font-medium text-gray-700 mb-2 block">Danh mục</label>
                      <div className="flex gap-2 flex-wrap">
                        {/* Trạng thái đang tải */}
                        {loadingCategories && (
                          <div className="flex items-center justify-center w-full py-2 text-blue-600">
                            <Loader2 className="w-4 h-4 mr-2 animate-spin" />
                            Đang tải danh mục...
                          </div>
                        )}
                        
                        {/* Hiển thị lỗi nếu có */}
                        {categoryError && (
                          <div className="text-red-500 text-sm w-full py-2">
                            Lỗi: {categoryError}. Đang sử dụng danh mục mặc định.
                          </div>
                        )}
                        
                        {/* Nút "Tất cả" luôn được hiển thị */}
                        <Button
                          variant={selectedCategory === "all" ? "default" : "outline"}
                          size="sm"
                          onClick={() => setSelectedCategory("all")}
                          className={selectedCategory === "all" ? "bg-blue-600 hover:bg-blue-700" : ""}
                        >
                          Tất cả
                        </Button>
                        
                        {/* Danh mục từ API */}
                        {categories && categories.length > 0 ? (
                          categories.map((category) => {
                            // Xác định dữ liệu từ category, kiểm tra các trường hợp khác nhau của API
                            const categoryId = category.id || '';
                            // Kiểm tra các trường hợp khác nhau của cấu trúc dữ liệu
                            const categoryName = 
                              typeof category.name === 'string' ? category.name : 
                              typeof category.Name === 'string' ? category.Name :
                              category.description || 
                              String(categoryId) || 
                              'Danh mục không tên';
                            
                            // Chuyển đổi an toàn sang lowercase cho việc so sánh
                            const categoryValue = getCategoryValue(categoryName);
                            
                            return (
                              <Button
                                key={categoryId}
                                variant={selectedCategory === categoryValue ? "default" : "outline"}
                                size="sm"
                                onClick={() => setSelectedCategory(categoryValue)}
                                className={selectedCategory === categoryValue ? "bg-blue-600 hover:bg-blue-700" : ""}
                              >
                                {categoryName}
                              </Button>
                            );
                          })
                        ) : !loadingCategories && !categoryError ? (
                          // Danh mục mặc định nếu API không trả về dữ liệu
                          <>
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
                          </>
                        ) : null}
                      </div>
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

              {/* Session information */}
              {isSessionActive && (
                <Card className="border-2 border-purple-100">
                  <CardHeader className="pb-2">
                    <CardTitle className="text-lg font-bold text-purple-900 flex items-center">
                      <BarChart className="w-5 h-5 text-purple-700 mr-2" />
                      Session hiện tại
                    </CardTitle>
                  </CardHeader>
                  <CardContent className="pt-2 space-y-4">
                    <div className="flex justify-between items-center">
                      <span className="text-gray-600 flex items-center">
                        <Clock className="w-4 h-4 mr-1 text-purple-500" />
                        Thời gian:
                      </span>
                      <span className="font-semibold text-purple-600">{formatTime(sessionTime)}</span>
                    </div>
                    
                    <div className="flex justify-between items-center">
                      <span className="text-gray-600 flex items-center">
                        <CheckCircle className="w-4 h-4 mr-1 text-green-500" />
                        Đã thuộc:
                      </span>
                      <span className="font-semibold text-green-600">{knownCards.length}</span>
                    </div>
                    
                    <div className="flex justify-between items-center">
                      <span className="text-gray-600 flex items-center">
                        <XCircle className="w-4 h-4 mr-1 text-red-500" />
                        Cần ôn lại:
                      </span>
                      <span className="font-semibold text-red-600">{unknownCards.length}</span>
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
            </div>
          </div>
          
          {/* Main content */}
          <div className="lg:col-span-2">
            {!isSessionActive ? (
              <div className="flex flex-col gap-6">
                <Card className="border-2 border-purple-100 overflow-hidden">
                  <div className="h-2 bg-gradient-to-r from-purple-500 to-blue-500 w-full"></div>
                  <CardHeader className="pb-2">
                    <CardTitle className="text-2xl font-bold text-center text-purple-900">
                      Sẵn sàng học?
                    </CardTitle>
                    <p className="text-center text-gray-600">
                      Bắt đầu session học từ vựng với {filteredVocabulary.length} từ
                    </p>
                  </CardHeader>
                  <CardContent className="pt-4 text-center space-y-6">
                    <div className="mb-6 flex justify-center">
                      <div className="bg-yellow-100 p-6 rounded-full">
                        <Lightbulb className="w-20 h-20 text-yellow-400" />
                      </div>
                    </div>
                    
                    <div className="grid grid-cols-3 gap-4 mb-6">
                      <div className="bg-purple-50 p-4 rounded-lg text-center border border-purple-100 shadow-sm hover:shadow-md transition-all">
                        <div className="text-2xl font-bold text-purple-600">{filteredVocabulary.length}</div>
                        <div className="text-sm text-purple-500">Từ vựng</div>
                      </div>
                      <div className="bg-blue-50 p-4 rounded-lg text-center border border-blue-100 shadow-sm hover:shadow-md transition-all">
                        <div className="text-2xl font-bold text-blue-600">5</div>
                        <div className="text-sm text-blue-500">Phút</div>
                      </div>
                      <div className="bg-green-50 p-4 rounded-lg text-center border border-green-100 shadow-sm hover:shadow-md transition-all">
                        <div className="text-2xl font-bold text-green-600">+{filteredVocabulary.length * 10}</div>
                        <div className="text-sm text-green-500">Điểm tối đa</div>
                      </div>
                    </div>
                    
                    <div className="space-y-3">
                      <Button 
                        onClick={startSession} 
                        className="w-full bg-purple-600 hover:bg-purple-700 py-6 text-lg shadow-md"
                      >
                        <Play className="w-5 h-5 mr-2" />
                        Bắt đầu học ngay
                      </Button>
                      
                      <Link to="/" className="block mt-4">
                        <Button variant="outline" className="w-full">
                          <ArrowLeft className="w-4 h-4 mr-2" />
                          Quay về trang chủ
                        </Button>
                      </Link>
                    </div>
                  </CardContent>
                </Card>

                <Card className="border border-gray-200 shadow-sm">
                  <CardHeader className="pb-2">
                    <CardTitle className="text-lg font-medium flex items-center">
                      <Info className="w-5 h-5 text-blue-500 mr-2" />
                      Thông tin học tập
                    </CardTitle>
                  </CardHeader>
                  <CardContent className="pt-2">
                    <div className="space-y-3 text-sm text-gray-600">
                      <div className="flex items-start gap-2">
                        <div className="mt-0.5">
                          <CheckCircle className="w-4 h-4 text-green-500" />
                        </div>
                        <div>
                          <p className="font-medium text-gray-700">Flashcard</p>
                          <p>Ôn tập hiệu quả với thẻ ghi nhớ thông minh, xem từ và nghĩa</p>
                        </div>
                      </div>
                      
                      <div className="flex items-start gap-2">
                        <div className="mt-0.5">
                          <CheckCircle className="w-4 h-4 text-green-500" />
                        </div>
                        <div>
                          <p className="font-medium text-gray-700">Trắc nghiệm</p>
                          <p>Làm bài trắc nghiệm để kiểm tra kiến thức và củng cố từ vựng</p>
                        </div>
                      </div>
                      
                      <div className="flex items-start gap-2">
                        <div className="mt-0.5">
                          <CheckCircle className="w-4 h-4 text-green-500" />
                        </div>
                        <div>
                          <p className="font-medium text-gray-700">Gõ từ</p>
                          <p>Luyện tập gõ từ để nhớ chính tả và củng cố trí nhớ</p>
                        </div>
                      </div>
                      
                      <div className="flex items-start gap-2">
                        <div className="mt-0.5">
                          <Star className="w-4 h-4 text-yellow-500" />
                        </div>
                        <div>
                          <p className="font-medium text-gray-700">Phần thưởng</p>
                          <p>Nhận {filteredVocabulary.length * 10} điểm khi hoàn thành tất cả các từ</p>
                        </div>
                      </div>
                    </div>
                  </CardContent>
                </Card>
              </div>
            ) : (
              <>
                {/* Study session progress */}
                <div className="mb-6">
                  <div className="flex justify-between mb-2">
                    <div className="flex items-center">
                      <Badge variant="outline" className={getDifficultyColor(currentCard.difficulty)}>
                        {getDifficultyLabel(currentCard.difficulty)}
                      </Badge>
                    </div>
                    <div className="flex items-center">
                      <Badge variant="outline" className="bg-blue-100 text-blue-700 border-blue-300">
                        {currentCard.category}
                      </Badge>
                    </div>
                  </div>
                  <Progress value={progress} className="h-2" />
                  <div className="flex justify-between mt-1 text-sm text-gray-500">
                    <span>{currentCardIndex + 1} / {filteredVocabulary.length}</span>
                    <span>{Math.round(progress)}%</span>
                          </div>
                        </div>
                        
                {/* Session controls */}
                <div className="flex justify-between mb-6">
                  <Button variant="outline" size="sm" onClick={toggleSession}>
                            {isSessionActive ? (
                              <>
                        <Pause className="w-4 h-4 mr-2" />
                        Tạm dừng
                              </>
                            ) : (
                              <>
                        <Play className="w-4 h-4 mr-2" />
                        Tiếp tục
                              </>
                            )}
                          </Button>
                  <Button variant="outline" size="sm" onClick={nextCard}>
                    <SkipForward className="w-4 h-4 mr-2" />
                    Bỏ qua
                          </Button>
                        </div>

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
