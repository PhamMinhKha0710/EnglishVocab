using FluentValidation;

namespace EnglishVocab.Application.Features.WordSets.Queries.GetWordSetById
{
    public class GetWordSetByIdQueryValidator : AbstractValidator<GetWordSetByIdQuery>
    {
        public GetWordSetByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Word Set ID must be greater than 0");
        }
    }
} 