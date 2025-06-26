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
  createdAt: Date
  reviewData?: ReviewData // Add spaced repetition data
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
  lastStudyDate?: Date
}

export interface StudySession {
  id: string
  date: Date
  duration: number
  wordsStudied: number
  correctAnswers: number
  pointsEarned: number
  wordsLearned: VocabularyWord[]
  wordsReviewed: VocabularyWord[]
  mode: "flashcard" | "quiz" | "typing" // Add study mode
}

export interface Badge {
  id: string
  name: string
  description: string
  icon: string
  unlockedAt: Date
  category: "streak" | "learning" | "achievement" | "special"
}

export interface AppSettings {
  theme: "light" | "dark" | "system"
  language: "vi" | "en"
  soundEnabled: boolean
  notificationsEnabled: boolean
  dailyGoal: number
  studyReminder: {
    enabled: boolean
    time: string
  }
  spacedRepetitionEnabled: boolean // Add spaced repetition setting
}

export type DifficultyLevel = "beginner" | "intermediate" | "advanced"
export type StudyMode = "flashcard" | "quiz" | "typing"

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
