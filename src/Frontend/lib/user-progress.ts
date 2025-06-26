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
  badges: string[]
}

export interface StudySession {
  id: string
  date: Date
  duration: number
  wordsStudied: number
  correctAnswers: number
  pointsEarned: number
}

export const defaultUserProgress: UserProgress = {
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

export const calculateLevel = (experience: number): number => {
  return Math.floor(experience / 100) + 1
}

export const getExperienceForNextLevel = (currentExp: number): number => {
  const currentLevel = calculateLevel(currentExp)
  return currentLevel * 100 - currentExp
}
