using MediatR;
using System.ComponentModel.DataAnnotations;
using EnglishVocab.Application.Common.Interfaces;

namespace EnglishVocab.Application.Features.Auth.Commands
{
    public class RefreshTokenCommand : IRequest<AuthResponse>
    {
        [Required]
        public string AccessToken { get; set; }
        
        [Required]
        public string RefreshToken { get; set; }
    }
} 