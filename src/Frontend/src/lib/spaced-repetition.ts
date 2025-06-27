// Spaced Repetition Algorithm - SM-2 Algorithm
import type { VocabularyWord, ReviewData } from "@/types";

export class SpacedRepetition {
  // SM-2 algorithm for spaced repetition
  
  // Constants for SM-2 algorithm
  private static readonly MIN_EASE_FACTOR = 1.3;
  
  /**
   * Calculate the next review date based on the SM-2 algorithm
   */
  static calculateNextReview(reviewData: ReviewData, quality: number): ReviewData {
    // Quality: 0-5 (0=worst, 5=best)
    const { easeFactor, interval, repetitions } = reviewData;
    
    // Clone the review data
    const newReviewData = { ...reviewData };
    
    // Update the quality
    newReviewData.quality = quality;
    
    // Update last review date
    newReviewData.lastReviewDate = new Date();
    
    // If quality is less than 3, reset repetitions
    if (quality < 3) {
      newReviewData.repetitions = 0;
      newReviewData.interval = 1;
    } else {
      // Update repetitions
      newReviewData.repetitions = repetitions + 1;
      
      // Calculate new interval
      if (repetitions === 0) {
        newReviewData.interval = 1;
      } else if (repetitions === 1) {
        newReviewData.interval = 6;
      } else {
        newReviewData.interval = Math.round(interval * easeFactor);
      }
      
      // Update ease factor
      newReviewData.easeFactor = Math.max(
        this.MIN_EASE_FACTOR,
        easeFactor + (0.1 - (5 - quality) * (0.08 + (5 - quality) * 0.02))
      );
    }
    
    // Calculate next review date
    const nextReviewDate = new Date();
    nextReviewDate.setDate(nextReviewDate.getDate() + newReviewData.interval);
    newReviewData.nextReviewDate = nextReviewDate;
    
    return newReviewData;
  }
  
  /**
   * Get words that are due for review
   */
  static getWordsForReview(words: VocabularyWord[]): VocabularyWord[] {
    const today = new Date();
    
    return words.filter(word => {
      if (!word.learned) {
        return false; // Skip words that haven't been learned yet
      }
      
      if (!word.reviewData || !word.reviewData.nextReviewDate) {
        return true; // Review words without review data
      }
      
      // Review if the next review date is today or earlier
      return word.reviewData.nextReviewDate <= today;
    });
  }
  
  /**
   * Initialize review data for a word
   */
  static initializeReviewData(): ReviewData {
    const today = new Date();
    return {
      easeFactor: 2.5,
      interval: 1,
      repetitions: 0,
      lastReviewDate: today,
      nextReviewDate: today,
      quality: 0
    };
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
