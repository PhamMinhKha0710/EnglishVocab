using FluentValidation;

namespace EnglishVocab.Application.Features.Words.Commands
{
    public class DeleteWordCommandValidator : AbstractValidator<DeleteWordCommand>
    {
        public DeleteWordCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Word ID must be greater than 0");
        }
    }
} 