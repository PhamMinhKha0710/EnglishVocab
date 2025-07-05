using FluentValidation;
using System.Text.RegularExpressions;

namespace EnglishVocab.Application.Features.Auth.Commands
{
    /// <summary>
    /// Validator cho command cập nhật hồ sơ người dùng
    /// </summary>
    public class UpdateUserProfileCommandValidator : AbstractValidator<UpdateUserProfileCommand>
    {
        public UpdateUserProfileCommandValidator()
        {
            RuleFor(v => v.UserId)
                .GreaterThan(0)
                .WithMessage("ID người dùng không hợp lệ");
                
            RuleFor(v => v.FirstName)
                .NotEmpty()
                .WithMessage("Tên người dùng là bắt buộc")
                .MaximumLength(50)
                .WithMessage("Tên người dùng không được vượt quá 50 ký tự");
                
            RuleFor(v => v.LastName)
                .NotEmpty()
                .WithMessage("Họ người dùng là bắt buộc")
                .MaximumLength(50)
                .WithMessage("Họ người dùng không được vượt quá 50 ký tự");
                
            RuleFor(v => v.Email)
                .NotEmpty()
                .WithMessage("Email là bắt buộc")
                .EmailAddress()
                .WithMessage("Email không hợp lệ")
                .MaximumLength(100)
                .WithMessage("Email không được vượt quá 100 ký tự");
                
            RuleFor(v => v.Username)
                .NotEmpty()
                .WithMessage("Tên đăng nhập là bắt buộc")
                .MaximumLength(50)
                .WithMessage("Tên đăng nhập không được vượt quá 50 ký tự")
                .Matches(new Regex("^[a-zA-Z0-9._@+-]+$"))
                .WithMessage("Tên đăng nhập chỉ được chứa chữ cái, số và các ký tự đặc biệt: ._@+-");
        }
    }
} 