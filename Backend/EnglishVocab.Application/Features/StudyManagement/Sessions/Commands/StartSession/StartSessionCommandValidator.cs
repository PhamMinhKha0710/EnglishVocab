using FluentValidation;

namespace EnglishVocab.Application.Features.StudySessions.Commands.StartSession
{
    public class StartSessionCommandValidator : AbstractValidator<StartSessionCommand>
    {
        public StartSessionCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required");
                
            RuleFor(x => x.WordSetId)
                .GreaterThan(0)
                .WithMessage("Word Set ID must be greater than 0");
                
            RuleFor(x => x.StudyMode)
                .NotEmpty()
                .WithMessage("Study mode is required");
        }
    }
} 