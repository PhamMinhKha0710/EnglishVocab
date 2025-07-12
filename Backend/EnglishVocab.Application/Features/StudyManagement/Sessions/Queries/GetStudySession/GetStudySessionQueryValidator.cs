using FluentValidation;

namespace EnglishVocab.Application.Features.StudySessions.Queries.GetStudySession
{
    public class GetStudySessionQueryValidator : AbstractValidator<GetStudySessionQuery>
    {
        public GetStudySessionQueryValidator()
        {
            RuleFor(x => x.SessionId)
                .GreaterThan(0)
                .WithMessage("Session ID must be greater than 0");
        }
    }
} 