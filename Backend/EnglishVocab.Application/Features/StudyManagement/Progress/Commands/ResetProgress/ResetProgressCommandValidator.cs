using FluentValidation;

namespace EnglishVocab.Application.Features.UserProgress.Commands
{
    public class ResetProgressCommandValidator : AbstractValidator<ResetProgressCommand>
    {
        public ResetProgressCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required");
                
            RuleFor(x => x.WordSetId)
                .GreaterThan(0)
                .When(x => x.WordSetId.HasValue)
                .WithMessage("Word Set ID must be greater than 0 when provided");
        }
    }
} 