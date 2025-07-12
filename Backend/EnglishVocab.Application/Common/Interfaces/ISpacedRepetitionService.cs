using EnglishVocab.Constants.Constant;
using System;

namespace EnglishVocab.Application.Common.Interfaces
{
    public interface ISpacedRepetitionService
    {
        (int newIntervalInDays, int newEaseFactor, MasteryLevel newMasteryLevel) CalculateNextReview(
            int easeFactor, 
            int intervalInDays, 
            bool isCorrect, 
            MasteryLevel currentMasteryLevel);
        (int newIntervalInDays, int newEaseFactor, MasteryLevel newMasteryLevel) CalculateNextReview(
            int easeFactor, 
            int intervalInDays, 
            int quality, 
            MasteryLevel currentMasteryLevel);
        DateTime CalculateNextReviewDate(DateTime lastReviewDate, int intervalInDays);
    }
} 
 