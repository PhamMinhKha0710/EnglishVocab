using FluentValidation;

namespace EnglishVocab.Application.Features.Authentication.Commands
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} không được để trống.")
                .EmailAddress().WithMessage("{PropertyName} phải là một địa chỉ email hợp lệ.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("{PropertyName} không được để trống.")
                .MinimumLength(6).WithMessage("{PropertyName} phải có ít nhất 6 ký tự.");
        }
    }
} 