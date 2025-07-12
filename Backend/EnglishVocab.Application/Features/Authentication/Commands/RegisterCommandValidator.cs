using FluentValidation;

namespace EnglishVocab.Application.Features.Authentication.Commands
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} không được để trống.")
                .EmailAddress().WithMessage("{PropertyName} phải là một địa chỉ email hợp lệ.");

            RuleFor(p => p.Username)
                .NotEmpty().WithMessage("{PropertyName} không được để trống.")
                .MinimumLength(3).WithMessage("{PropertyName} phải có ít nhất 3 ký tự.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("{PropertyName} không được để trống.")
                .MinimumLength(6).WithMessage("{PropertyName} phải có ít nhất 6 ký tự.");

            RuleFor(p => p.ConfirmPassword)
                .NotEmpty().WithMessage("{PropertyName} không được để trống.")
                .Equal(p => p.Password).WithMessage("Mật khẩu xác nhận không khớp với mật khẩu.");

            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("{PropertyName} không được để trống.")
                .MaximumLength(50).WithMessage("{PropertyName} không được vượt quá 50 ký tự.");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("{PropertyName} không được để trống.")
                .MaximumLength(50).WithMessage("{PropertyName} không được vượt quá 50 ký tự.");
        }
    }
} 