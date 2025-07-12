using EnglishVocab.Application.Common.Models;
using MediatR;

namespace EnglishVocab.Application.Features.Authentication.Commands
{
    public class RefreshTokenCommand : IRequest<TokenDto>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
} 