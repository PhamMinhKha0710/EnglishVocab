using EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetRecentlyStudied;
using FluentValidation;

namespace EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetRecentlyStudied
{
    public class GetRecentlyStudiedQueryValidator : AbstractValidator<GetRecentlyStudiedQuery>
    {
        public GetRecentlyStudiedQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required");
                
            RuleFor(x => x.Count)
                .GreaterThan(0)
                .LessThanOrEqualTo(100)
                .WithMessage("Count must be between 1 and 100");
        }
    }
} 