using FluentValidation;

namespace EnglishVocab.Application.Features.Auth.Commands
{
    public class RevokeTokenCommandValidator : AbstractValidator<RevokeTokenCommand>
    {
        public RevokeTokenCommandValidator()
        {
            RuleFor(p => p.UserId)
                .NotEmpty().WithMessage("{PropertyName} không được để trống.");
        }
    }
} 