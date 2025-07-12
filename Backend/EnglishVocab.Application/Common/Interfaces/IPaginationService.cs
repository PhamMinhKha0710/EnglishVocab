using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishVocab.Application.Common.Models;

namespace EnglishVocab.Application.Common.Interfaces
{
    public interface IPaginationService
    {
        Task<DataTableResponse<T>> CreatePaginatedListAsync<T>(
            IQueryable<T> source, 
            int pageNumber, 
            int pageSize, 
            string sortBy = null, 
            bool ascending = true);
        DataTableResponse<T> CreatePaginatedList<T>(
            IEnumerable<T> source, 
            int pageNumber, 
            int pageSize, 
            string sortBy = null, 
            bool ascending = true);
    }
} 