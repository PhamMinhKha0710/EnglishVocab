using FluentValidation;

namespace EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetDueForReview
{
    public class GetDueForReviewQueryValidator : AbstractValidator<GetDueForReviewQuery>
    {
        public GetDueForReviewQueryValidator()
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