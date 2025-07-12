using MediatR;
using System.ComponentModel.DataAnnotations;
using EnglishVocab.Application.Common.Interfaces;

namespace EnglishVocab.Application.Features.Authentication.Commands
{
    public class LoginCommand : IRequest<AuthResponse>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
} 