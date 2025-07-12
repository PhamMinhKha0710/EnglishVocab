using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.Authentication.Commands
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenDto>
    {
        private readonly IAuthService _authService;

        public RefreshTokenCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<TokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.RefreshTokenAsync(request.AccessToken, request.RefreshToken);
            return new TokenDto
            {
                Token = result.Token,
                Expiration = result.Expiration,
                Claims = result.Claims
            };
        }
    }
} 