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
        /// Số trang cần lấy (ưu tiên sử dụng PageNumber và PageSize nếu được cung cấp)
        /// </summary>
        public int? PageNumber { get; set; } = 1;
        
        /// <summary>
        /// Kích thước trang (ưu tiên sử dụng PageNumber và PageSize nếu được cung cấp)
        /// </summary>
        public int? PageSize { get; set; } = 10;

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
        public string Search { get; set; } 

        /// <summary>
        /// Tìm kiếm theo từng cột cụ thể
        /// </summary>
        public Dictionary<string, string> Filters { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Tính toán số trang dựa trên Start và Length hoặc sử dụng PageNumber nếu được cung cấp
        /// </summary>
        public int GetPageNumber()
        {
            if (PageNumber.HasValue && PageNumber.Value > 0)
            {
                return PageNumber.Value;
            }
            
            return Start / Length + 1;
        }

        /// <summary>
        /// Lấy kích thước trang từ Length hoặc PageSize nếu được cung cấp
        /// </summary>
        public int GetPageSize()
        {
            if (PageSize.HasValue && PageSize.Value > 0)
            {
                return PageSize.Value;
            }
            
            return Length;
        }
        
        /// <summary>
        /// Đảm bảo Start và Length được tính toán đúng từ PageNumber và PageSize
        /// </summary>
        public void NormalizeRequest()
        {
            if (PageNumber.HasValue && PageSize.HasValue && PageNumber.Value > 0 && PageSize.Value > 0)
            {
                Start = (PageNumber.Value - 1) * PageSize.Value;
                Length = PageSize.Value;
            }
        }
        
        /// <summary>
        /// Tạo DataTableRequest từ tham số phân trang phổ biến
        /// </summary>
        public static DataTableRequest FromPagination(int pageNumber, int pageSize, string sortBy = "Id", bool ascending = true)
        {
            return new DataTableRequest
            {
                Start = (pageNumber - 1) * pageSize,
                Length = pageSize,
                PageNumber = pageNumber,
                PageSize = pageSize,
                OrderBy = sortBy,
                Order = ascending ? "asc" : "desc"
            };
        }
        
        /// <summary>
        /// Chuyển đổi sang DataTableRequest từ tham số pageNumber, pageSize
        /// </summary>
        public void ConvertFromPagination(int pageNumber, int pageSize, string sortBy = null, bool? ascending = null)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Start = (pageNumber - 1) * pageSize;
            Length = pageSize;
            
            if (!string.IsNullOrEmpty(sortBy))
                OrderBy = sortBy;
                
            if (ascending.HasValue)
                Order = ascending.Value ? "asc" : "desc";
        }
        
        /// <summary>
        /// Kiểm tra xem yêu cầu này có phải là yêu cầu phân trang hay không
        /// </summary>
        /// <returns>True nếu Length > 0, ngược lại là False</returns>
        public bool IsPagingRequest()
        {
            return Length > 0;
        }
    }
} 