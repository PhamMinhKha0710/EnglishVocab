using FluentValidation;

namespace EnglishVocab.Application.Features.StudySessions.Commands.EndSession
{
    public class EndSessionCommandValidator : AbstractValidator<EndSessionCommand>
    {
        public EndSessionCommandValidator()
        {
            RuleFor(x => x.SessionId)
                .GreaterThan(0)
                .WithMessage("Session ID must be greater than 0");
        }
    }
} 