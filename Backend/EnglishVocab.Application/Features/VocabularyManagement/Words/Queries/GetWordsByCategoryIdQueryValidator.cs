using FluentValidation;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.Queries
{
    public class GetWordsByCategoryIdQueryValidator : AbstractValidator<GetWordsByCategoryIdQuery>
    {
        public GetWordsByCategoryIdQueryValidator()
        {
            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .WithMessage("CategoryId must be greater than 0.");
        }
    }
} 