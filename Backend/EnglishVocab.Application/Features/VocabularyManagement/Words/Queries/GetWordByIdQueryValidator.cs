using FluentValidation;

namespace EnglishVocab.Application.Features.VocabularyManagement.Words.Queries
{
    public class GetWordByIdQueryValidator : AbstractValidator<GetWordByIdQuery>
    {
        public GetWordByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Word ID must be greater than 0");
        }
    }
} 