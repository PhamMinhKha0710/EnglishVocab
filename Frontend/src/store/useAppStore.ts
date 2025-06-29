import { create } from "zustand"
import { persist } from "zustand/middleware"
import type { VocabularyWord, UserProgress, StudySession, AppSettings } from "@/types"
import { vocabularyData } from "@/data/vocabulary"
// Add spaced repetition imports
import { SpacedRepetition } from "@/lib/spaced-repetition"

interface AppState {
  // Vocabulary
  vocabulary: VocabularyWord[]

  // User Progress
  userProgress: UserProgress

  // Study Sessions
  studySessions: StudySession[]

  // Settings
  settings: AppSettings

  // Current Study State
  currentSession: {
    isActive: boolean
    startTime?: Date
    wordsStudied: number
    correctAnswers: number
    currentWordIndex: number
    selectedDifficulty: string
    selectedCategory: string
  }

  // Actions
  updateUserProgress: (progress: Partial<UserProgress>) => void
  addStudySession: (session: StudySession) => void
  updateVocabularyWord: (wordId: number, updates: Partial<VocabularyWord>) => void
  updateSettings: (settings: Partial<AppSettings>) => void
  startStudySession: (difficulty?: string, category?: string) => void
  endStudySession: () => void
  markWordAsLearned: (wordId: number) => void
  markWordForReview: (wordId: number) => void
  resetProgress: () => void
  // Add new actions for spaced repetition
  getWordsForReview: () => VocabularyWord[]
  updateWordReview: (wordId: number, quality: number) => void
}

const defaultUserProgress: UserProgress = {
  totalWords: 0,
  learnedWords: 0,
  currentStreak: 0,
  longestStreak: 0,
  totalPoints: 0,
  dailyGoal: 10,
  wordsToday: 0,
  level: 1,
  experience: 0,
  badges: [],
}

const defaultSettings: AppSettings = {
  theme: "light",
  language: "vi",
  soundEnabled: true,
  notificationsEnabled: true,
  dailyGoal: 10,
  studyReminder: {
    enabled: false,
    time: "19:00",
  },
}

export const useAppStore = create<AppState>()(
  persist(
    (set, get) => ({
      // Update vocabulary state to include extended vocabulary
      vocabulary: vocabularyData,
      userProgress: defaultUserProgress,
      studySessions: [],
      settings: defaultSettings,
      currentSession: {
        isActive: false,
        wordsStudied: 0,
        correctAnswers: 0,
        currentWordIndex: 0,
        selectedDifficulty: "all",
        selectedCategory: "all",
      },

      updateUserProgress: (progress) =>
        set((state) => ({
          userProgress: { ...state.userProgress, ...progress },
        })),

      addStudySession: (session) =>
        set((state) => ({
          studySessions: [session, ...state.studySessions],
        })),

      updateVocabularyWord: (wordId, updates) =>
        set((state) => ({
          vocabulary: state.vocabulary.map((word) => (word.id === wordId ? { ...word, ...updates } : word)),
        })),

      updateSettings: (settings) =>
        set((state) => ({
          settings: { ...state.settings, ...settings },
        })),

      startStudySession: (difficulty = "all", category = "all") =>
        set((state) => ({
          currentSession: {
            ...state.currentSession,
            isActive: true,
            startTime: new Date(),
            wordsStudied: 0,
            correctAnswers: 0,
            currentWordIndex: 0,
            selectedDifficulty: difficulty,
            selectedCategory: category,
          },
        })),

      endStudySession: () =>
        set((state) => {
          const session = state.currentSession
          if (session.startTime) {
            const duration = Math.floor((Date.now() - session.startTime.getTime()) / 1000)
            const pointsEarned = session.correctAnswers * 10

            // Add study session
            const newSession: StudySession = {
              id: Date.now().toString(),
              date: new Date(),
              duration,
              wordsStudied: session.wordsStudied,
              correctAnswers: session.correctAnswers,
              pointsEarned,
              wordsLearned: [],
              wordsReviewed: [],
            }

            // Update user progress
            const newProgress = {
              ...state.userProgress,
              totalPoints: state.userProgress.totalPoints + pointsEarned,
              wordsToday: state.userProgress.wordsToday + session.correctAnswers,
              experience: state.userProgress.experience + pointsEarned,
            }

            return {
              currentSession: {
                isActive: false,
                wordsStudied: 0,
                correctAnswers: 0,
                currentWordIndex: 0,
                selectedDifficulty: "all",
                selectedCategory: "all",
              },
              studySessions: [newSession, ...state.studySessions],
              userProgress: newProgress,
            }
          }
          return state
        }),

      markWordAsLearned: (wordId) =>
        set((state) => ({
          vocabulary: state.vocabulary.map((word) =>
            word.id === wordId
              ? { ...word, learned: true, lastReviewed: new Date(), reviewCount: word.reviewCount + 1 }
              : word,
          ),
          currentSession: {
            ...state.currentSession,
            correctAnswers: state.currentSession.correctAnswers + 1,
            wordsStudied: state.currentSession.wordsStudied + 1,
          },
        })),

      markWordForReview: (wordId) =>
        set((state) => ({
          vocabulary: state.vocabulary.map((word) =>
            word.id === wordId ? { ...word, lastReviewed: new Date(), reviewCount: word.reviewCount + 1 } : word,
          ),
          currentSession: {
            ...state.currentSession,
            wordsStudied: state.currentSession.wordsStudied + 1,
          },
        })),

      resetProgress: () =>
        set({
          userProgress: defaultUserProgress,
          studySessions: [],
          vocabulary: vocabularyData.map((word) => ({ ...word, learned: false, reviewCount: 0 })),
        }),
      getWordsForReview: () => {
        const { vocabulary } = get()
        return SpacedRepetition.getWordsForReview(vocabulary)
      },

      updateWordReview: (wordId: number, quality: number) =>
        set((state) => ({
          vocabulary: state.vocabulary.map((word) => {
            if (word.id === wordId) {
              const currentReviewData = word.reviewData || {
                easeFactor: 2.5,
                interval: 1,
                repetitions: 0,
                nextReviewDate: new Date(),
                lastReviewDate: new Date(),
                quality: 0,
              }

              const newReviewData = SpacedRepetition.calculateNextReview(currentReviewData, quality)
              return { ...word, reviewData: newReviewData }
            }
            return word
          }),
        })),
    }),
    {
      name: "flashlearn-storage",
    },
  ),
)
