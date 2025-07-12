using FluentValidation;

namespace EnglishVocab.Application.Features.WordSets.Queries.GetUserWordSets
{
    public class GetUserWordSetsQueryValidator : AbstractValidator<GetUserWordSetsQuery>
    {
        public GetUserWordSetsQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required");
        }
    }
} 