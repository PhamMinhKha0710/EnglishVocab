using FluentValidation;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.Queries
{
    public class SearchWordsQueryValidator : AbstractValidator<SearchWordsQuery>
    {
        public SearchWordsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page number must be at least 1");
                
            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(100)
                .WithMessage("Page size must be between 1 and 100");
        }
    }
} 