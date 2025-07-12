using FluentValidation;

namespace EnglishVocab.Application.Features.StudyManagement.Sessions.Queries.GetSessionStatistics
{
    public class GetSessionStatisticsQueryValidator : AbstractValidator<GetSessionStatisticsQuery>
    {
        public GetSessionStatisticsQueryValidator()
        {
            RuleFor(x => x.SessionId)
                .GreaterThan(0)
                .WithMessage("Session ID must be greater than 0");
        }
    }
} 