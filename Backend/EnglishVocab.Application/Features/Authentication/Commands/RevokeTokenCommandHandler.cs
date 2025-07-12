using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.Auth.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.Auth.CommandHandlers
{
    public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, bool>
    {
        private readonly IAuthService _authService;
        private readonly ILogger<RevokeTokenCommandHandler> _logger;

        public RevokeTokenCommandHandler(
            IAuthService authService,
            ILogger<RevokeTokenCommandHandler> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        public async Task<bool> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _authService.RevokeToken(request.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while revoking token for user {UserId}", request.UserId);
                return false;
            }
        }
    }
} 