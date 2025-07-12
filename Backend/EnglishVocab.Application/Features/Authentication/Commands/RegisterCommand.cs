using MediatR;
using System.ComponentModel.DataAnnotations;
using EnglishVocab.Application.Common.Interfaces;

namespace EnglishVocab.Application.Features.Authentication.Commands
{
    public class RegisterCommand : IRequest<AuthResponse>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp với mật khẩu.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
} 