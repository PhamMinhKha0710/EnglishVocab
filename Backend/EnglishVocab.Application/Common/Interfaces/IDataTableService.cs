using EnglishVocab.Application.Common.Models;
using System.Linq.Expressions;


namespace EnglishVocab.Application.Common.Interfaces
{
    public interface IDataTableService
    {

        Task<DataTableResponse<T>> CreateResponseAsync<T>(
            IQueryable<T> query,
            DataTableRequest request,
            CancellationToken cancellationToken = default);
        Task<DataTableResponse<T>> CreateResponseAsync<T>(
            IEnumerable<T> data,
            DataTableRequest request,
            CancellationToken cancellationToken = default);

        PaginatedResponse<T> CreatePaginatedResponse<T>(
            IQueryable<T> query,
            PaginationParameters parameters);
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