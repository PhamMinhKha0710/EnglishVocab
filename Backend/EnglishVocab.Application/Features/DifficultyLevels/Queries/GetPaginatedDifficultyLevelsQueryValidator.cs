using FluentValidation;

namespace EnglishVocab.Application.Features.DifficultyLevels.Queries
{
    public class GetPaginatedDifficultyLevelsQueryValidator : AbstractValidator<GetPaginatedDifficultyLevelsQuery>
    {
        public GetPaginatedDifficultyLevelsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(50)
                .WithMessage("Page size must be between 1 and 50.");
                
            RuleFor(x => x.SortBy)
                .Must(sortBy => sortBy == "Value" || sortBy == "Name" || sortBy == "WordCount")
                .WithMessage("Sort by must be one of: Value, Name, WordCount.");
        }
    }
} 