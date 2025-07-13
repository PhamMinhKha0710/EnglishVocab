using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.DifficultyLevels.DTOs;
using FluentValidation;
using MediatR;
using System.Collections.Generic;

namespace EnglishVocab.Application.Features.DifficultyLevels.Queries
{
    public class GetDifficultyLevelsQuery : IRequest<object>
    {
        /// <summary>
        /// Xác định có sử dụng phân trang hay không
        /// </summary>
        public bool UsePagination { get; set; } = false;

        /// <summary>
        /// Tham số phân trang (nếu UsePagination = true)
        /// </summary>
        public DataTableRequest PaginationParams { get; set; } = new DataTableRequest();
    }
    
    public class GetDifficultyLevelsQueryValidator : AbstractValidator<GetDifficultyLevelsQuery>
    {
        public GetDifficultyLevelsQueryValidator()
        {
            When(x => x.UsePagination, () => 
            {
                RuleFor(x => x.PaginationParams.Start)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Start must be greater than or equal to 0.");

                RuleFor(x => x.PaginationParams.Length)
                    .GreaterThan(0)
                    .LessThanOrEqualTo(100)
                    .WithMessage("Length must be between 1 and 100.");
                    
                RuleFor(x => x.PaginationParams.OrderBy)
                    .Must(sortBy => sortBy == "Value" || sortBy == "Name" || sortBy == "Id")
                    .WithMessage("OrderBy must be one of: Value, Name, Id.");
            });
        }
    }
} 