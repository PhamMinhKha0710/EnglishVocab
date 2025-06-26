"use client"

import type React from "react"

import { useState, useEffect, useRef } from "react"
import { Card, CardContent } from "@/components/ui/card"
import { Badge } from "@/components/ui/badge"
import type { VocabularyWord } from "@/types"
import { Search, X, BookOpen } from "lucide-react"
import { cn } from "@/lib/utils"

interface SearchBarProps {
  vocabulary: VocabularyWord[]
  onWordSelect: (word: VocabularyWord) => void
  placeholder?: string
  className?: string
}

export function SearchBar({
  vocabulary,
  onWordSelect,
  placeholder = "Tìm kiếm từ vựng...",
  className,
}: SearchBarProps) {
  const [query, setQuery] = useState("")
  const [isOpen, setIsOpen] = useState(false)
  const [results, setResults] = useState<VocabularyWord[]>([])
  const [selectedIndex, setSelectedIndex] = useState(-1)
  const [filters, setFilters] = useState({
    difficulty: "all",
    category: "all",
    learned: "all",
  })

  const inputRef = useRef<HTMLInputElement>(null)
  const resultsRef = useRef<HTMLDivElement>(null)

  // Search logic
  useEffect(() => {
    if (query.length < 2) {
      setResults([])
      setIsOpen(false)
      return
    }

    const searchResults = vocabulary
      .filter((word) => {
        const matchesQuery =
          word.word.toLowerCase().includes(query.toLowerCase()) ||
          word.meaning.toLowerCase().includes(query.toLowerCase()) ||
          word.example.toLowerCase().includes(query.toLowerCase())

        const matchesDifficulty = filters.difficulty === "all" || word.difficulty === filters.difficulty
        const matchesCategory = filters.category === "all" || word.category === filters.category
        const matchesLearned =
          filters.learned === "all" ||
          (filters.learned === "learned" && word.learned) ||
          (filters.learned === "unlearned" && !word.learned)

        return matchesQuery && matchesDifficulty && matchesCategory && matchesLearned
      })
      .slice(0, 10) // Limit to 10 results

    setResults(searchResults)
    setIsOpen(searchResults.length > 0)
    setSelectedIndex(-1)
  }, [query, vocabulary, filters])

  // Keyboard navigation
  const handleKeyDown = (e: React.KeyboardEvent) => {
    if (!isOpen) return

    switch (e.key) {
      case "ArrowDown":
        e.preventDefault()
        setSelectedIndex((prev) => Math.min(prev + 1, results.length - 1))
        break
      case "ArrowUp":
        e.preventDefault()
        setSelectedIndex((prev) => Math.max(prev - 1, -1))
        break
      case "Enter":
        e.preventDefault()
        if (selectedIndex >= 0 && results[selectedIndex]) {
          handleWordSelect(results[selectedIndex])
        }
        break
      case "Escape":
        setIsOpen(false)
        setSelectedIndex(-1)
        inputRef.current?.blur()
        break
    }
  }

  const handleWordSelect = (word: VocabularyWord) => {
    onWordSelect(word)
    setQuery("")
    setIsOpen(false)
    setSelectedIndex(-1)
    inputRef.current?.blur()
  }

  const clearSearch = () => {
    setQuery("")
    setResults([])
    setIsOpen(false)
    setSelectedIndex(-1)
    inputRef.current?.focus()
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
    <div className={cn("relative", className)}>
      {/* Search Input */}
      <div className="relative">
        <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
          <Search className="h-4 w-4 text-gray-400" />
        </div>
        <input
          ref={inputRef}
          type="text"
          value={query}
          onChange={(e) => setQuery(e.target.value)}
          onKeyDown={handleKeyDown}
          onFocus={() => query.length >= 2 && setIsOpen(true)}
          placeholder={placeholder}
          className="w-full pl-10 pr-10 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-purple-500 focus:border-transparent"
        />
        {query && (
          <button onClick={clearSearch} className="absolute inset-y-0 right-0 pr-3 flex items-center">
            <X className="h-4 w-4 text-gray-400 hover:text-gray-600" />
          </button>
        )}
      </div>

      {/* Quick Filters */}
      <div className="flex gap-2 mt-2 flex-wrap">
        <select
          value={filters.difficulty}
          onChange={(e) => setFilters((prev) => ({ ...prev, difficulty: e.target.value }))}
          className="text-xs border rounded px-2 py-1"
        >
          <option value="all">Tất cả độ khó</option>
          <option value="beginner">Cơ bản</option>
          <option value="intermediate">Trung bình</option>
          <option value="advanced">Nâng cao</option>
        </select>

        <select
          value={filters.learned}
          onChange={(e) => setFilters((prev) => ({ ...prev, learned: e.target.value }))}
          className="text-xs border rounded px-2 py-1"
        >
          <option value="all">Tất cả</option>
          <option value="learned">Đã học</option>
          <option value="unlearned">Chưa học</option>
        </select>
      </div>

      {/* Search Results */}
      {isOpen && (
        <Card className="absolute top-full left-0 right-0 mt-1 z-50 max-h-96 overflow-y-auto shadow-lg">
          <CardContent className="p-0">
            {results.map((word, index) => (
              <div
                key={word.id}
                onClick={() => handleWordSelect(word)}
                className={cn(
                  "p-3 cursor-pointer border-b border-gray-100 last:border-b-0 hover:bg-gray-50 transition-colors",
                  selectedIndex === index && "bg-purple-50",
                )}
              >
                <div className="flex items-start justify-between gap-3">
                  <div className="flex-1 min-w-0">
                    <div className="flex items-center gap-2 mb-1">
                      <h4 className="font-semibold text-gray-800">{word.word}</h4>
                      <Badge variant="outline" className={getDifficultyColor(word.difficulty)}>
                        {word.difficulty}
                      </Badge>
                      {word.learned && (
                        <Badge variant="outline" className="bg-green-100 text-green-800">
                          ✓
                        </Badge>
                      )}
                    </div>
                    <p className="text-sm text-gray-600 mb-1">{word.meaning}</p>
                    <p className="text-xs text-gray-500 italic">"{word.example}"</p>
                  </div>
                  <div className="flex-shrink-0">
                    <Badge variant="secondary" className="text-xs">
                      {word.category}
                    </Badge>
                  </div>
                </div>
              </div>
            ))}

            {results.length === 0 && query.length >= 2 && (
              <div className="p-4 text-center text-gray-500">
                <BookOpen className="w-8 h-8 mx-auto mb-2 opacity-50" />
                <p>Không tìm thấy từ vựng nào</p>
                <p className="text-sm">Thử thay đổi từ khóa hoặc bộ lọc</p>
              </div>
            )}
          </CardContent>
        </Card>
      )}

      {/* Overlay to close search */}
      {isOpen && <div className="fixed inset-0 z-40" onClick={() => setIsOpen(false)} />}
    </div>
  )
}
