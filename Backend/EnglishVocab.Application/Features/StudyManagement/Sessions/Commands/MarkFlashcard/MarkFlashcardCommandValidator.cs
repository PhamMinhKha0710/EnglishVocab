using FluentValidation;

namespace EnglishVocab.Application.Features.StudySessions.Commands.MarkFlashcard
{
    public class MarkFlashcardCommandValidator : AbstractValidator<MarkFlashcardCommand>
    {
        public MarkFlashcardCommandValidator()
        {
            RuleFor(x => x.SessionId)
                .GreaterThan(0)
                .WithMessage("Session ID must be greater than 0");
                
            RuleFor(x => x.WordId)
                .GreaterThan(0)
                .WithMessage("Word ID must be greater than 0");
        }
    }
} 