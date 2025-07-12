using FluentValidation;

namespace EnglishVocab.Application.Features.StudySessions.Queries.GetUserSessions
{
    public class GetUserSessionsQueryValidator : AbstractValidator<GetUserSessionsQuery>
    {
        public GetUserSessionsQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required");
        }
    }
} 