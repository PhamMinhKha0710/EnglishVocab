export interface VocabularyWord {
  id: number
  word: string
  meaning: string
  example: string
  pronunciation: string
  difficulty: "beginner" | "intermediate" | "advanced"
  category: string
  learned: boolean
  reviewCount: number
  lastReviewed?: Date
  nextReview?: Date
  reviewData?: ReviewData
}

export interface ReviewData {
  easeFactor: number
  interval: number
  repetitions: number
  nextReviewDate: Date
  lastReviewDate: Date
  quality: number
}

export interface UserProgress {
  totalWords: number
  learnedWords: number
  currentStreak: number
  longestStreak: number
  totalPoints: number
  dailyGoal: number
  wordsToday: number
  level: number
  experience: number
  badges: Badge[]
}

export interface StudySession {
  id: string
  date: Date
  duration: number
  wordsStudied: number
  correctAnswers: number
  pointsEarned: number
  wordsLearned: string[]
  wordsReviewed: string[]
}

export interface Badge {
  id: number
  title: string
  description: string
  earned: boolean
  earnedDate?: Date
  icon: string
}

export interface AppSettings {
  theme: "light" | "dark" | "system"
  language: string
  soundEnabled: boolean
  notificationsEnabled: boolean
  dailyGoal: number
  studyReminder: {
    enabled: boolean
    time: string
  }
}

export type DifficultyLevel = "beginner" | "intermediate" | "advanced"
export type StudyMode = "flashcard" | "quiz" | "typing"

// Category interface
export interface CategoryType {
  id: number
  name: string
  description: string
  wordCount: number
}

// Quiz interfaces
export interface QuizResult {
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

// Typing interfaces
export interface TypingResult {
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
