using FluentValidation;

namespace EnglishVocab.Application.Features.VocabularyManagement.WordSets.Queries.GetAllWordSets
{
    public class GetAllWordSetsQueryValidator : AbstractValidator<GetAllWordSetsQuery>
    {
        public GetAllWordSetsQueryValidator()
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