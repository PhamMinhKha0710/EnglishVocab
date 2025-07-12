using FluentValidation;

namespace EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetProgressByWord
{
    public class GetProgressByWordQueryValidator : AbstractValidator<GetProgressByWordQuery>
    {
        public GetProgressByWordQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required");
                
            RuleFor(x => x.WordId)
                .GreaterThan(0)
                .WithMessage("Word ID must be greater than 0");
        }
    }
} 