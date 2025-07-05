using FluentValidation;

namespace EnglishVocab.Application.Features.Auth.Commands
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(p => p.AccessToken)
                .NotEmpty().WithMessage("{PropertyName} không được để trống.");

            RuleFor(p => p.RefreshToken)
                .NotEmpty().WithMessage("{PropertyName} không được để trống.");
        }
    }
} 