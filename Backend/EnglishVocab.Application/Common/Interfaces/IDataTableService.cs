using EnglishVocab.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Common.Interfaces
{
    public interface IDataTableService
    {
        /// <summary>
        /// Tạo DataTableResponse từ IQueryable với phân trang và tìm kiếm
        /// </summary>
        Task<DataTableResponse<T>> CreateResponseAsync<T>(
            IQueryable<T> query,
            DataTableRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Tạo DataTableResponse từ IEnumerable với phân trang và tìm kiếm
        /// </summary>
        Task<DataTableResponse<T>> CreateResponseAsync<T>(
            IEnumerable<T> data,
            DataTableRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Áp dụng phân trang cho IQueryable
        /// </summary>
        IQueryable<T> ApplyPaging<T>(
            IQueryable<T> query,
            DataTableRequest request);

        /// <summary>
        /// Áp dụng sắp xếp cho IQueryable
        /// </summary>
        IQueryable<T> ApplySorting<T>(
            IQueryable<T> query,
            DataTableRequest request);

        /// <summary>
        /// Áp dụng tìm kiếm toàn cục cho IQueryable
        /// </summary>
        IQueryable<T> ApplySearch<T>(
            IQueryable<T> query,
            DataTableRequest request,
            params Expression<Func<T, string>>[] searchProperties);

        /// <summary>
        /// Áp dụng tìm kiếm theo cột cụ thể cho IQueryable
        /// </summary>
        IQueryable<T> ApplyColumnFilters<T>(
            IQueryable<T> query,
            DataTableRequest request,
            Dictionary<string, Expression<Func<T, string>>> filterProperties);
    }
} 