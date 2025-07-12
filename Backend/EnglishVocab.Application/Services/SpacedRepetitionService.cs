using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Constants.Constant;
using System;

namespace EnglishVocab.Application.Services
{
    public class SpacedRepetitionService : ISpacedRepetitionService
    {
        // Các hằng số cho thuật toán SM-2
        private const int MIN_INTERVAL = 1;  // Khoảng thời gian tối thiểu (1 ngày)
        private const int MIN_EASE_FACTOR = 130;  // Độ dễ tối thiểu (1.3)
        private const int DEFAULT_EASE_FACTOR = 250;  // Độ dễ mặc định (2.5)

        /// <summary>
        /// Tính toán ngày xem lại tiếp theo dựa trên thuật toán SM-2
        /// </summary>
        /// <param name="easeFactor">Độ dễ của từ (250 = 2.5)</param>
        /// <param name="intervalInDays">Khoảng thời gian hiện tại tính bằng ngày</param>
        /// <param name="isCorrect">Người dùng có nhớ từ này không</param>
        /// <param name="currentMasteryLevel">Cấp độ thành thạo hiện tại</param>
        /// <returns>Tuple chứa: Khoảng thời gian mới (ngày), Độ dễ mới, Cấp độ thành thạo mới</returns>
        public (int newIntervalInDays, int newEaseFactor, MasteryLevel newMasteryLevel) CalculateNextReview(
            int easeFactor, 
            int intervalInDays, 
            bool isCorrect, 
            MasteryLevel currentMasteryLevel)
        {
            // Chuyển đổi boolean isCorrect thành quality (0-5)
            int quality = isCorrect ? 4 : 1; // 4 = Good, 1 = Poor
            return CalculateNextReview(easeFactor, intervalInDays, quality, currentMasteryLevel);
        }

        /// <summary>
        /// Tính toán ngày xem lại tiếp theo dựa trên thuật toán SM-2
        /// </summary>
        /// <param name="easeFactor">Độ dễ của từ (250 = 2.5)</param>
        /// <param name="intervalInDays">Khoảng thời gian hiện tại tính bằng ngày</param>
        /// <param name="quality">Chất lượng phản hồi (0-5)</param>
        /// <param name="currentMasteryLevel">Cấp độ thành thạo hiện tại</param>
        /// <returns>Tuple chứa: Khoảng thời gian mới (ngày), Độ dễ mới, Cấp độ thành thạo mới</returns>
        public (int newIntervalInDays, int newEaseFactor, MasteryLevel newMasteryLevel) CalculateNextReview(
            int easeFactor, 
            int intervalInDays, 
            int quality, 
            MasteryLevel currentMasteryLevel)
        {
            // Đảm bảo quality nằm trong khoảng 0-5
            quality = Math.Max(0, Math.Min(5, quality));

            // Tính toán độ dễ mới dựa trên chất lượng phản hồi
            int newEaseFactor = easeFactor + (int)((0.1 - (5 - quality) * (0.08 + (5 - quality) * 0.02)) * 100);
            newEaseFactor = Math.Max(MIN_EASE_FACTOR, newEaseFactor);

            int newIntervalInDays;
            MasteryLevel newMasteryLevel = currentMasteryLevel;

            if (quality < 3)
            {
                // Nếu phản hồi kém, reset lại khoảng thời gian
                newIntervalInDays = MIN_INTERVAL;
                
                // Giảm cấp độ thành thạo nếu phản hồi kém
                if (currentMasteryLevel > MasteryLevel.New)
                {
                    newMasteryLevel = currentMasteryLevel - 1;
                }
            }
            else
            {
                // Tính toán khoảng thời gian mới dựa trên thuật toán SM-2
                if (intervalInDays == 0)
                {
                    newIntervalInDays = 1;
                }
                else if (intervalInDays == 1)
                {
                    newIntervalInDays = 6;
                }
                else
                {
                    newIntervalInDays = (int)Math.Round(intervalInDays * (easeFactor / 100.0));
                }

                // Tăng cấp độ thành thạo
                if (newMasteryLevel < MasteryLevel.Mastered)
                {
                    newMasteryLevel = newMasteryLevel + 1;
                }
            }

            // Giới hạn khoảng thời gian tối đa là 365 ngày
            newIntervalInDays = Math.Min(365, Math.Max(MIN_INTERVAL, newIntervalInDays));

            return (newIntervalInDays, newEaseFactor, newMasteryLevel);
        }

        /// <summary>
        /// Tính toán ngày xem lại tiếp theo
        /// </summary>
        /// <param name="lastReviewDate">Ngày xem lại gần nhất</param>
        /// <param name="intervalInDays">Khoảng thời gian tính bằng ngày</param>
        /// <returns>Ngày xem lại tiếp theo</returns>
        public DateTime CalculateNextReviewDate(DateTime lastReviewDate, int intervalInDays)
        {
            return lastReviewDate.AddDays(intervalInDays);
        }
    }
} 
 