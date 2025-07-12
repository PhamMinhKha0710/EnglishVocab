using System.Collections.Generic;

namespace EnglishVocab.Application.Common.Models
{
    public class DataTableRequest
    {
        /// <summary>
        /// Vị trí bắt đầu lấy dữ liệu
        /// </summary>
        public int Start { get; set; } = 0;

        /// <summary>
        /// Số lượng bản ghi cần lấy
        /// </summary>
        public int Length { get; set; } = 10;

        /// <summary>
        /// Thứ tự sắp xếp (asc/desc)
        /// </summary>
        public string Order { get; set; } = "asc";

        /// <summary>
        /// Tên cột để sắp xếp
        /// </summary>
        public string OrderBy { get; set; } = "Id";

        /// <summary>
        /// Từ khóa tìm kiếm toàn cục
        /// </summary>
        public string Search { get; set; } = string.Empty;

        /// <summary>
        /// Tìm kiếm theo từng cột cụ thể
        /// </summary>
        public Dictionary<string, string> Filters { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Tính toán số trang dựa trên Start và Length
        /// </summary>
        public int GetPageNumber()
        {
            return Start / Length + 1;
        }

        /// <summary>
        /// Lấy kích thước trang
        /// </summary>
        public int GetPageSize()
        {
            return Length;
        }
    }
} 