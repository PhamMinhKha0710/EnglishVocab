using EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetUserStatistics;
using FluentValidation;

namespace EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetUserStatistics
{
    public class GetUserStatisticsQueryValidator : AbstractValidator<GetUserStatisticsQuery>
    {
        public GetUserStatisticsQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required");
        }
    }
} 