using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Common.Services
{
    public class PaginationService : IPaginationService
    {
        public async Task<DataTableResponse<T>> CreatePaginatedListAsync<T>(
            IQueryable<T> source, 
            int pageNumber, 
            int pageSize, 
            string sortBy = null, 
            bool ascending = true)
        {
            // Ensure valid pagination parameters
            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Max(1, Math.Min(100, pageSize));

            // Get total count for pagination metadata
            var totalCount = await source.CountAsync();

            // Apply sorting if specified
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                var propertyInfo = typeof(T).GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null)
                {
                    source = ascending 
                        ? source.OrderBy(sortBy) 
                        : source.OrderBy(sortBy + " descending");
                }
            }

            // Apply pagination
            var items = await source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new DataTableResponse<T>
            {
                Data = items,
                RecordsTotal = totalCount,
                RecordsFiltered = totalCount
            };
        }

        public DataTableResponse<T> CreatePaginatedList<T>(
            IEnumerable<T> source, 
            int pageNumber, 
            int pageSize, 
            string sortBy = null, 
            bool ascending = true)
        {
            // Ensure valid pagination parameters
            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Max(1, Math.Min(100, pageSize));

            // Get total count
            var totalCount = source.Count();

            // Apply sorting if specified
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                var propertyInfo = typeof(T).GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null)
                {
                    source = ascending
                        ? source.AsQueryable().OrderBy(sortBy)
                        : source.AsQueryable().OrderBy(sortBy + " descending");
                }
            }

            // Apply pagination
            var items = source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new DataTableResponse<T>
            {
                Data = items,
                RecordsTotal = totalCount,
                RecordsFiltered = totalCount
            };
        }
    }
} 