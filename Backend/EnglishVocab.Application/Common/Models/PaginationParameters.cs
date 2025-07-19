namespace EnglishVocab.Application.Common.Models
{
    public class PaginationParameters
    {
        private int _pageNumber = 1;
        private int _pageSize = 10;
        
        public int PageNumber 
        { 
            get => _pageNumber; 
            set => _pageNumber = value < 1 ? 1 : value; 
        }
        
        public int PageSize 
        { 
            get => _pageSize; 
            set => _pageSize = value < 1 ? 10 : (value > 100 ? 100 : value); 
        }
        
        public string Search { get; set; }
        public string OrderBy { get; set; } = "Id";
        public string OrderDirection { get; set; } = "asc";
        
        public bool IsPagingRequest()
        {
            return PageNumber > 0 && PageSize > 0;
        }
        
        public void NormalizeRequest()
        {
            PageNumber = PageNumber < 1 ? 1 : PageNumber;
            PageSize = PageSize < 1 ? 10 : (PageSize > 100 ? 100 : PageSize);
            OrderDirection = string.IsNullOrWhiteSpace(OrderDirection) ? "asc" : OrderDirection.ToLower();
        }
    }
} 