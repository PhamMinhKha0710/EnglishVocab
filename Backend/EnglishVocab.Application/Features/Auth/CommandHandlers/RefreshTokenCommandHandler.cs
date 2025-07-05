using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Features.Auth.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Features.Auth.CommandHandlers
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        private readonly IAuthService _authService;
        private readonly ILogger<RefreshTokenCommandHandler> _logger;

        public RefreshTokenCommandHandler(
            IAuthService authService,
            ILogger<RefreshTokenCommandHandler> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _authService.RefreshToken(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while refreshing token");
                return new AuthResponse
                {
                    IsAuthenticated = false,
                    Message = "Đã xảy ra lỗi khi làm mới token: " + ex.Message
                };
            }
        }
    }
} 