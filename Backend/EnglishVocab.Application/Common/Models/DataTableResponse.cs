using System.Collections.Generic;

namespace EnglishVocab.Application.Common.Models
{
    public class DataTableResponse<T>
    {
        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// Tổng số bản ghi (không phân trang)
        /// </summary>
        public int RecordsTotal { get; set; }

        /// <summary>
        /// Tổng số bản ghi sau khi lọc
        /// </summary>
        public int RecordsFiltered { get; set; }

        /// <summary>
        /// Tạo response trống
        /// </summary>
        public static DataTableResponse<T> Empty()
        {
            return new DataTableResponse<T>
            {
                Data = new List<T>(),
                RecordsTotal = 0,
                RecordsFiltered = 0
            };
        }
    }
} 