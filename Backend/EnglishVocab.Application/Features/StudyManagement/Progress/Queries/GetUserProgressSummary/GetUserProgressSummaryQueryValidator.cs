using EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetUserProgressSummary;
using FluentValidation;

namespace EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetUserProgressSummary
{
    public class GetUserProgressSummaryQueryValidator : AbstractValidator<GetUserProgressSummaryQuery>
    {
        public GetUserProgressSummaryQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required");
        }
    }
} 