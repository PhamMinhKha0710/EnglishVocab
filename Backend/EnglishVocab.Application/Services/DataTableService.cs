using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Services
{
    public class DataTableService : IDataTableService
    {
        public async Task<DataTableResponse<T>> CreateResponseAsync<T>(
            IQueryable<T> query,
            DataTableRequest request,
            CancellationToken cancellationToken = default)
        {
            // Đếm tổng số bản ghi trước khi áp dụng bất kỳ bộ lọc nào
            int totalCount = await query.CountAsync(cancellationToken);

            // Áp dụng sắp xếp
            query = ApplySorting(query, request);

            // Đếm số bản ghi sau khi lọc
            int filteredCount = await query.CountAsync(cancellationToken);

            // Áp dụng phân trang
            query = ApplyPaging(query, request);

            // Lấy dữ liệu
            var data = await query.ToListAsync(cancellationToken);

            // Tạo response
            return new DataTableResponse<T>
            {
                Data = data,
                RecordsTotal = totalCount,
                RecordsFiltered = filteredCount
            };
        }

        public async Task<DataTableResponse<T>> CreateResponseAsync<T>(
            IEnumerable<T> data,
            DataTableRequest request,
            CancellationToken cancellationToken = default)
        {
            // Chuyển đổi IEnumerable thành IQueryable
            var query = data.AsQueryable();
            
            // Sử dụng phương thức đã có để xử lý
            return await CreateResponseAsync(query, request, cancellationToken);
        }

        public IQueryable<T> ApplyPaging<T>(
            IQueryable<T> query,
            DataTableRequest request)
        {
            return query
                .Skip(request.Start)
                .Take(request.Length);
        }

        public IQueryable<T> ApplySorting<T>(
            IQueryable<T> query,
            DataTableRequest request)
        {
            if (string.IsNullOrEmpty(request.OrderBy))
                return query;

            // Lấy thông tin thuộc tính cần sắp xếp
            var propertyInfo = GetPropertyInfo<T>(request.OrderBy);
            if (propertyInfo == null)
                return query;

            // Tạo expression để sắp xếp
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyInfo);
            var lambda = Expression.Lambda(property, parameter);

            // Áp dụng sắp xếp
            var methodName = request.Order.ToLower() == "asc" ? "OrderBy" : "OrderByDescending";
            var resultExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { typeof(T), propertyInfo.PropertyType },
                query.Expression,
                Expression.Quote(lambda));

            return query.Provider.CreateQuery<T>(resultExpression);
        }

        public IQueryable<T> ApplySearch<T>(
            IQueryable<T> query,
            DataTableRequest request,
            params Expression<Func<T, string>>[] searchProperties)
        {
            if (string.IsNullOrEmpty(request.Search) || searchProperties.Length == 0)
                return query;

            // Tạo biểu thức tìm kiếm cho mỗi thuộc tính
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression combinedExpression = null;

            foreach (var searchProperty in searchProperties)
            {
                // Lấy body của expression
                var memberExpression = searchProperty.Body;

                // Tạo biểu thức so sánh: property.Contains(searchTerm)
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var searchTermExpression = Expression.Constant(request.Search);
                var containsExpression = Expression.Call(memberExpression, containsMethod, searchTermExpression);

                // Xử lý null
                var nullCheckExpression = Expression.NotEqual(memberExpression, Expression.Constant(null, typeof(string)));
                var safeContainsExpression = Expression.AndAlso(nullCheckExpression, containsExpression);

                // Kết hợp với các biểu thức trước đó
                if (combinedExpression == null)
                    combinedExpression = safeContainsExpression;
                else
                    combinedExpression = Expression.OrElse(combinedExpression, safeContainsExpression);
            }

            // Tạo lambda expression từ combined expression
            var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);

            // Áp dụng filter
            return query.Where(lambda);
        }

        public IQueryable<T> ApplyColumnFilters<T>(
            IQueryable<T> query,
            DataTableRequest request,
            Dictionary<string, Expression<Func<T, string>>> filterProperties)
        {
            if (request.Filters == null || request.Filters.Count == 0 || filterProperties.Count == 0)
                return query;

            foreach (var filter in request.Filters)
            {
                if (string.IsNullOrEmpty(filter.Value) || !filterProperties.ContainsKey(filter.Key))
                    continue;

                var propertyExpression = filterProperties[filter.Key];
                var parameter = Expression.Parameter(typeof(T), "x");
                var memberExpression = propertyExpression.Body;

                // Tạo biểu thức so sánh: property.Contains(filterValue)
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var filterValueExpression = Expression.Constant(filter.Value);
                var containsExpression = Expression.Call(memberExpression, containsMethod, filterValueExpression);

                // Xử lý null
                var nullCheckExpression = Expression.NotEqual(memberExpression, Expression.Constant(null, typeof(string)));
                var safeContainsExpression = Expression.AndAlso(nullCheckExpression, containsExpression);

                // Tạo lambda expression
                var lambda = Expression.Lambda<Func<T, bool>>(safeContainsExpression, parameter);

                // Áp dụng filter
                query = query.Where(lambda);
            }

            return query;
        }

        private PropertyInfo GetPropertyInfo<T>(string propertyName)
        {
            // Hỗ trợ thuộc tính lồng nhau (nested properties) như "User.Name"
            var propertyNames = propertyName.Split('.');
            var type = typeof(T);
            PropertyInfo propertyInfo = null;

            foreach (var name in propertyNames)
            {
                propertyInfo = type.GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo == null)
                    return null;

                type = propertyInfo.PropertyType;
            }

            return propertyInfo;
        }
    }
} 