using System.Collections.Generic;

namespace EnglishVocab.Application.Common.Models
{
    public class DataTableResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        
        public int RecordsTotal { get; set; }

        public int RecordsFiltered { get; set; }

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