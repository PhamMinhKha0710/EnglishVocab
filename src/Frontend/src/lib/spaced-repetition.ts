// Spaced Repetition Algorithm - SM-2 Algorithm
export interface ReviewData {
  easeFactor: number
  interval: number
  repetitions: number
  nextReviewDate: Date
  lastReviewDate: Date
  quality: number // 0-5 rating
}

export class SpacedRepetition {
  static calculateNextReview(currentData: ReviewData, quality: number): ReviewData {
    const newData = { ...currentData }
    newData.quality = quality
    newData.lastReviewDate = new Date()

    if (quality >= 3) {
      // Correct response
      if (newData.repetitions === 0) {
        newData.interval = 1
      } else if (newData.repetitions === 1) {
        newData.interval = 6
      } else {
        newData.interval = Math.round(newData.interval * newData.easeFactor)
      }
      newData.repetitions += 1
    } else {
      // Incorrect response - reset
      newData.repetitions = 0
      newData.interval = 1
    }

    // Update ease factor
    newData.easeFactor = Math.max(1.3, newData.easeFactor + (0.1 - (5 - quality) * (0.08 + (5 - quality) * 0.02)))

    // Calculate next review date
    const nextDate = new Date()
    nextDate.setDate(nextDate.getDate() + newData.interval)
    newData.nextReviewDate = nextDate

    return newData
  }

  static getWordsForReview(vocabulary: any[]): any[] {
    const now = new Date()
    return vocabulary.filter((word) => {
      if (!word.reviewData) return true // New words
      return new Date(word.reviewData.nextReviewDate) <= now
    })
  }

  static getDifficultyMultiplier(difficulty: string): number {
    switch (difficulty) {
      case "beginner":
        return 0.8
      case "intermediate":
        return 1.0
      case "advanced":
        return 1.3
      default:
        return 1.0
    }
  }
}
